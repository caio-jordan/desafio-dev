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
        public async Task<List<Weather>> GetCacheAsync()
        {
            try
            {
                return await _previsaoDbContext.Weather.Include(x =>x .Location).Include(y=>y.Current).ToListAsync();               
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
