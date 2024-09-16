﻿using desafio_dev.API.Core.Services.Interface;
using desafio_dev.API.Domain;
using Microsoft.EntityFrameworkCore;
using Hangfire;
using desafio_dev.API.Repository;
using desafio_dev.API.Model;
using desafio_dev.API.Infrastructure.Context;
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
        return  await _weatherRepository.GetCacheAsync();
    }

    public async Task<int> DeleteCacheAsync()
    {
        return await _weatherRepository.DeleteCacheAsync();
    }
    private void BackgroundJobCleanCache()
    {
        try
        {
            BackgroundJob.Schedule(
                () => DeleteCacheAsync(), TimeSpan.FromHours(1));

        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
        }
    }
}
