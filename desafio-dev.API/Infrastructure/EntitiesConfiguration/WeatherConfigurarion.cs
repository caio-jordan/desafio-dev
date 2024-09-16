using desafio_dev.API.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace desafio_dev.API.Infrastructure.EntitiesConfiguration
{
    public class WeatherConfigurarion : IEntityTypeConfiguration<Weather>
    {
        public void Configure(EntityTypeBuilder<Weather> builder)
        {
            builder.HasOne(x => x.Current).WithMany(x => x.Weather)
                .HasForeignKey(x=>x.IdCurrent).OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.Location).WithMany(x => x.Weather)
                .HasForeignKey(x => x.IdLocation).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
