using desafio_dev.API.Domain;

namespace desafio_dev.API.Model
{
    public class WeatherForecastModel
    {
        public Location Location { get; set; }
        public Current Current { get; set; }
        public ForecastModel Forecast { get; set; }
    }
}
