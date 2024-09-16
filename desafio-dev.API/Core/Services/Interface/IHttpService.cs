using desafio_dev.API.Domain;

namespace desafio_dev.API.Core.Services.Interface;

public interface IHttpService
{
    Task<WeatherResponse?> GetPrevisaoAtualAsync(string cidade);
    Task<WeatherForecastResponse?> GetPrevisaoEstendidaAsync(string cidade, int diasPrevisao = 1);
}