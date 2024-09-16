using desafio_dev.API.Domain;

namespace desafio_dev.API.Core.Services.Interface;
public interface IService
{
    Task<WeatherResponse?> GetPrevisaoAtualAsync(string cidade);
    Task<WeatherForecastResponse?> GetPrevisaoEstendidaAsync(string cidade, int diasPrevisao = 1);
    Task<List<WeatherData?>> GetHistoricoAsync();
    Task<int> DeleteCache();
}