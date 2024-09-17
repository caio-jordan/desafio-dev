using Polly;
using Polly.Extensions.Http;
using Polly.Retry;
using Polly.Timeout;
using Polly.Wrap;
using System.Diagnostics.CodeAnalysis;
using System.Net.Http.Headers;
using System.Security.Authentication;
using System.Text.Json.Serialization;
using desafio_dev.API.Core.Services.Interface;
using desafio_dev.API.Core.Services;
using Hangfire;
using desafio_dev.API.Repository;
using desafio_dev.API.Model;
using Hangfire.MemoryStorage;

namespace desafio_dev.API.Core.IoC;

[ExcludeFromCodeCoverage]
public static class ServiceCollectionExtensions
{
    public static void AddInitServices(this IServiceCollection services)
    {        
        services.AddTransient<IHttpService, HttpService>();
        services.AddTransient<IService, Service>();
        services.AddScoped<IWeatherRepository, WeatherRepository>();


        //Configuracao do hangfire, como task background
        services.AddHangfire(configuration => configuration
                .UseRecommendedSerializerSettings().UseMemoryStorage()
                );

        GlobalJobFilters.Filters.Add(new AutomaticRetryAttribute
        {
            Attempts = 3,
            DelaysInSeconds = new int[] { 300 }
        });        
        services.AddHangfireServer();

        services.AddRouting(options => options.LowercaseUrls = true);
        services.AddHttpCustomServices();
        services.AddHttpClient("httpClient", c =>
        {
            // Total time for requests
            c.Timeout = TimeSpan.FromSeconds(30);
            c.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        })
        .ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler
        {
            SslProtocols = SslProtocols.None,
            ClientCertificateOptions = ClientCertificateOption.Manual,
            ServerCertificateCustomValidationCallback =
                (httpRequestMessage, cert, cetChain, policyErrors) => true
        });
        


    }

    private static void AddHttpCustomServices(this IServiceCollection services)
    {
        services.AddPrevisaoAtualService();
        services.AddPrevisaoEstendidaService();
        services.AddControllers()
            .AddControllersAsServices()
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
            });
    }

    private static void AddPrevisaoAtualService(this IServiceCollection services)
    {
        services.AddHttpClient(nameof(WeatherModel), c =>
        {
            c.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            c.BaseAddress = new Uri("http://api.weatherapi.com/v1/");
        })
            .ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler
            {
                SslProtocols = SslProtocols.Tls12,
                ClientCertificateOptions = ClientCertificateOption.Manual,
                ServerCertificateCustomValidationCallback =
                 (httpRequestMessage, cert, cetChain, policyErrors) => true
            })
         .AddPolicyHandler(CreatePolicy(services));
    }

    private static void AddPrevisaoEstendidaService(this IServiceCollection services)
    {
        services.AddHttpClient(nameof(WeatherForecastModel), c =>
        {
            c.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            c.BaseAddress = new Uri("http://api.weatherapi.com/v1/");
        })
            .ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler
            {
                SslProtocols = SslProtocols.Tls12,
                ClientCertificateOptions = ClientCertificateOption.Manual,
                ServerCertificateCustomValidationCallback =
                 (httpRequestMessage, cert, cetChain, policyErrors) => true
            })
         .AddPolicyHandler(CreatePolicy(services));
    }    
    public static void StartHangFire(this IServiceCollection services)
    {        
        var serviceProvider = services.BuildServiceProvider();        

        var weatherRepository = serviceProvider.GetService<IWeatherRepository>();
        if (weatherRepository is not null)
            BackgroundJob.Schedule(
                 () => weatherRepository.DeleteCacheAsync(), TimeSpan.FromHours(1));
    }

    //Aplicando politica de Retry
    private static AsyncPolicyWrap<HttpResponseMessage> CreatePolicy(IServiceCollection services)
    {
        var maxRetryAttempts = 3;
        var pauseBetweenFailures = TimeSpan.FromSeconds(2);
        var timeoutPolicy = Policy.TimeoutAsync<HttpResponseMessage>(30);

        var retryPolicy = HttpPolicyExtensions
            .HandleTransientHttpError()
            .Or<TimeoutRejectedException>()
            .WaitAndRetryAsync(maxRetryAttempts, i => pauseBetweenFailures,
                onRetry: async (outcome, timespan, retryAttempt, context) =>
                {
                    var logger = services.BuildServiceProvider().GetService<ILogger<RetryPolicy>>();
                    logger.LogWarning($"Delaying for {timespan.TotalSeconds}s, then making retry {retryAttempt}.");

                    try
                    {
                        if (outcome.Exception != null)
                        {
                            logger.LogWarning($"Resultado da Request: Message Exception:{outcome.Exception.Message}");
                        }

                        if (outcome.Result != null)
                        {
                            var messageResult = await outcome.Result.Content.ReadAsStringAsync();
                            logger.LogWarning(
                                $"Resultado da Request:  Response Status Code: {outcome.Result.StatusCode}, Response Body:{messageResult}");
                        }
                    }
                    catch (Exception ex)
                    {
                        var details = ex.GetType().FullName;

                        logger.LogError($"Error: {ex.Message} - StackTrace: {ex.StackTrace}, details: {details}");
                    }
                })
            .WrapAsync(timeoutPolicy);

        return retryPolicy;
    }
}

