using desafio_dev.API.Domain;
using desafio_dev.API.Infrastructure.Context;
using desafio_dev.API.Model;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;
using System.Diagnostics.CodeAnalysis;

namespace desafio_dev.API.Repository
{
    [ExcludeFromCodeCoverage]
    public class WeatherRepository : IWeatherRepository
    {
        private readonly PrevisaoDbContext _previsaoDbContext;
        private readonly ILogger<WeatherRepository> _logger;
        public WeatherRepository(ILogger<WeatherRepository> logger, PrevisaoDbContext previsaoDbContext)
        {

            _previsaoDbContext = previsaoDbContext;
            _logger = logger;

        }
        public async Task<List<WeatherModel?>> GetCacheAsync()
        {
            try
            {
                var currents = await _previsaoDbContext.Current.ToListAsync();

                var locations = await _previsaoDbContext.Location.ToListAsync();

                var weathers = await _previsaoDbContext.Weather.ToListAsync();

                weathers.ForEach(weather =>
                {
                    locations.Where(location => location.Id == weather.Id)
                             .ToList()
                             .ForEach(location =>
                             {
                                 weather.Location = location;
                                 weather.Current = currents.First(x => x.Id == weather.Id);
                             });
                });

                var weatherModel = new List<WeatherModel>();

                foreach (var weather in weathers)
                {
                    weatherModel.Add(new WeatherModel()
                    {
                        Current = new (weather.Current),
                        Location = new(weather.Location)
                    });
                }
                return weatherModel;
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        public async Task InsertWeatherAsync(WeatherModel weatherModel)
        {
            try
            {
                await _previsaoDbContext.Weather.AddAsync(new Weather(weatherModel));
                _previsaoDbContext.SaveChanges();

            }
            catch (SqlException ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }
        public async Task<int> DeleteCacheAsync()
        {
            try
            {
                var countRemove = await _previsaoDbContext.Weather.ExecuteDeleteAsync();
                return countRemove;
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }

        }
    }
}
