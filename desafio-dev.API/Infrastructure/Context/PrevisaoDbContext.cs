using desafio_dev.API.Domain;
using Microsoft.EntityFrameworkCore;


namespace desafio_dev.API.Infrastructure.Context
{
    public class PrevisaoDbContext : DbContext
    {

        public PrevisaoDbContext(DbContextOptions<PrevisaoDbContext> options) : base(options)
        {
        }

        public DbSet<Weather> Weather { get; set; }
        public DbSet<Location> Location { get; set; }
        public DbSet<Current> Current { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(typeof(PrevisaoDbContext).Assembly);
        }

    }
}
