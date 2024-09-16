using desafio_dev.API.Model;

namespace desafio_dev.API.Core.Services.Interface;

public interface IHttpService
{
    Task<WeatherModel?> GetPrevisaoAtualAsync(string cidade);
    Task<WeatherForecastModel?> GetPrevisaoEstendidaAsync(string cidade, int diasPrevisao = 1);
}