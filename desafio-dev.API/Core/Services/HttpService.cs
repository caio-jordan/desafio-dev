using desafio_dev.API.Core.Services.Interface;
using desafio_dev.API.Model;
using System.Text.Json;

namespace desafio_dev.API.Core.Services;

public class HttpService : IHttpService
{
    private readonly HttpClient _httpClient;
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly ILogger<HttpService> _logger;
    private readonly JsonSerializerOptions _jsonOptions;

    public HttpService(IHttpClientFactory httpClientFactory, ILogger<HttpService> logger)
    {
        _httpClient = httpClientFactory.CreateClient("HttpClient");
        _httpClientFactory = httpClientFactory;
        _logger = logger;
        _jsonOptions = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };
    }

    public async Task<WeatherModel?> GetPrevisaoAtualAsync(string cidade)
    {
        try
        {
            using var httpClient = _httpClientFactory.CreateClient(nameof(WeatherModel));

            var request = new HttpRequestMessage(HttpMethod.Get, $"current.json?key=56f99aed071d4672bed223559241009&q={cidade}&aqi=no");

            var result = await httpClient.SendAsync(request);

            if (!result.IsSuccessStatusCode)
            {
                return null;
            }

            var responseContent = await result.Content.ReadAsStringAsync();

            var responseResult = ValidateResponseContent(responseContent);

            if (responseResult is null)
            {
                return null;
            }

            return JsonSerializer.Deserialize<WeatherModel>(responseResult, _jsonOptions);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            throw;
        }

    }
    public async Task<WeatherForecastModel?> GetPrevisaoEstendidaAsync(string cidade, int diasPrevisao = 1)
    {
        try
        {
            using var httpClient = _httpClientFactory.CreateClient(nameof(WeatherForecastModel));

            var request = new HttpRequestMessage(HttpMethod.Get, $"forecast.json?q={cidade}&days={diasPrevisao}&key=56f99aed071d4672bed223559241009");

            var result = await httpClient.SendAsync(request);

            if (!result.IsSuccessStatusCode)
            {
                return null;
            }

            var responseContent = await result.Content.ReadAsStringAsync();

            var responseResult = ValidateResponseContent(responseContent);

            if (responseResult is null)
            {
                return null;
            }


            return responseContent.Equals("{ }") ? null : JsonSerializer.Deserialize<WeatherForecastModel>(responseContent, _jsonOptions);

        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            return null;
        }
    }

    private string? ValidateResponseContent(string responseContent)
    {
        if (string.IsNullOrEmpty(responseContent) || responseContent.Equals("{ }"))
        {
            _logger.LogWarning("Cidade Não encontrada");
            return null;
        }
        return responseContent;

    }
}

