using System.Diagnostics.CodeAnalysis;

namespace desafio_dev.API.Model
{
    [ExcludeFromCodeCoverage]
    public class WeatherModel
    {
        public CurrentModel Current { get; set; }
        public LocationModel Location { get; set; }
    }
}