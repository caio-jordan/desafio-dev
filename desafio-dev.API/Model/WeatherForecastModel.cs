using System.Diagnostics.CodeAnalysis;

namespace desafio_dev.API.Model
{
    [ExcludeFromCodeCoverage]
    public class WeatherForecastModel
    {
        public LocationModel Location { get; set; }
        public CurrentModel Current { get; set; }
        public ForecastModel Forecast { get; set; }
    }
}
