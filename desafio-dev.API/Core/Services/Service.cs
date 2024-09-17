using desafio_dev.API.Core.Services.Interface;
using Hangfire;
using desafio_dev.API.Repository;
using desafio_dev.API.Model;
using System.Diagnostics.CodeAnalysis;

namespace desafio_dev.API.Core.Services;

[ExcludeFromCodeCoverage]
public class Service : IService
{

    private readonly IHttpService _httpService;
    private readonly ILogger<Service> _logger;
    private readonly IWeatherRepository _weatherRepository;

    public Service(IHttpService httpService, ILogger<Service> logger, IWeatherRepository weatherRepository)
    {
        _httpService = httpService;
        _logger = logger;
        _weatherRepository = weatherRepository;

    }

    public async Task<WeatherModel?> GetPrevisaoAtualAsync(string cidade)
    {
        var responsePrevisaoAtual = await _httpService.GetPrevisaoAtualAsync(cidade);

        if (responsePrevisaoAtual == null)
        {
            return responsePrevisaoAtual;
        }

        await _weatherRepository.InsertWeatherAsync(responsePrevisaoAtual);

        return responsePrevisaoAtual;
    }
    public async Task<WeatherForecastModel?> GetPrevisaoEstendidaAsync(string cidade, int diasPrevisao = 1)
    {
        return await _httpService.GetPrevisaoEstendidaAsync(cidade, diasPrevisao);
    }
    public async Task<List<WeatherModel>?> GetCacheAsync()
    {
        var weathers = await _weatherRepository.GetCacheAsync();

        return weathers.Select(x => new WeatherModel()
        {
            Current = new(x.Current),
            Location = new(x.Location)
        }).ToList();
    }

    public async Task<int> DeleteCacheAsync()
    {
        return await _weatherRepository.DeleteCacheAsync();
    }
 }
