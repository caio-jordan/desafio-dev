using desafio_dev.API.Domain;
using Microsoft.EntityFrameworkCore;

namespace desafio_dev.API.Repository
{
    public class PrevisaoDbContext : DbContext
    {

        public PrevisaoDbContext(DbContextOptions<PrevisaoDbContext> options) : base(options)
        {

        }

        public DbSet<WeatherData> WeatherData { get; set; }
    }
}
