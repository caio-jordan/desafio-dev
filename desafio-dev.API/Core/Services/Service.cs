using desafio_dev.API.Core.Services.Interface;
using desafio_dev.API.Domain;

namespace desafio_dev.API.Core.Services;

public class Service : IService
{

    private readonly IHttpService _httpService;
    private readonly ILogger<Service> _logger;

    public Service(IHttpService httpService, ILogger<Service> logger)
    {
        _httpService = httpService;
        _logger = logger;
    }

    public async Task<WeatherResponse?> GetPrevisaoAtualAsync(string cidade)
    {
        try
        {
            var responsePrevisaoAtual = await _httpService.GetPrevisaoAtualAsync(cidade);


            return responsePrevisaoAtual;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            return null;
        }

    }
    public async Task<WeatherForecastResponse?> GetPrevisaoEstendidaAsync(string cidade, int diasPrevisao = 1)
    {
        try
        {
            var responsePrevisaoEstendida = await _httpService.GetPrevisaoEstendidaAsync(cidade, diasPrevisao);



            return responsePrevisaoEstendida;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            return null;
        }
    }

    public async Task<int> DeleteCache()
    {
        return 1;
    }
}
