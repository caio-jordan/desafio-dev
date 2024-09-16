using desafio_dev.API.Domain;
using desafio_dev.API.Model;

namespace desafio_dev.API.Core.Services.Interface;
public interface IService
{
    Task<WeatherModel?> GetPrevisaoAtualAsync(string cidade);
    Task<WeatherForecastModel?> GetPrevisaoEstendidaAsync(string cidade, int diasPrevisao = 1);
    Task<List<WeatherModel?>> GetCacheAsync();
    Task<int> DeleteCacheAsync();
}