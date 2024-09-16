using desafio_dev.API.Core.Services.Interface;
using desafio_dev.API.Domain;
using desafio_dev.API.Repository;
using Microsoft.EntityFrameworkCore;

namespace desafio_dev.API.Core.Services;

public class Service : IService
{

    private readonly IHttpService _httpService;
    private readonly ILogger<Service> _logger;
    private PrevisaoDbContext _contextDb;

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
        try
        {
            var weatherData = await _contextDb.WeatherData.ExecuteDeleteAsync();
            return weatherData;
            
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            return 0;
        }
    }

    public async Task<List<WeatherData>?> GetHistoricoAsync()
    {
        try
        {
            return await _contextDb.WeatherData.ToListAsync();            
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            return await Task.FromResult(default(List<WeatherData>));
        }
        
    }
}
