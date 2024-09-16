using Polly;
using Polly.Extensions.Http;
using Polly.Retry;
using Polly.Timeout;
using Polly.Wrap;
using System.Diagnostics.CodeAnalysis;
using System.Net.Http.Headers;
using System.Security.Authentication;
using System.Text.Json.Serialization;
using Polly.NoOp;
using desafio_dev.API.Core.Services.Interface;
using desafio_dev.API.Core.Services;
using desafio_dev.API.Domain;

namespace desafio_dev.API.Core.IoC;

[ExcludeFromCodeCoverage]
public static class ServiceCollectionExtensions
{
    public static void AddInitServices(this IServiceCollection services)
    {

        services.AddTransient<IHttpService, HttpService>();
        services.AddTransient<IService, Service>();

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
                //options.JsonSerializerOptions.WriteIndented = true;
            });
    }

    private static void AddPrevisaoAtualService(this IServiceCollection services)
    {
        services.AddHttpClient(nameof(WeatherResponse), c =>
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
        services.AddHttpClient(nameof(WeatherForecastResponse), c =>
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

