using desafio_dev.API.Domain;
using desafio_dev.API.Model;

namespace desafio_dev.API.Repository
{
    public interface IWeatherRepository
    {                
        Task<List<WeatherModel?>> GetCacheAsync();
        Task InsertWeatherAsync(WeatherModel weatherModel);
        Task<int> DeleteCacheAsync();

    }
}
