namespace desafio_dev.API.Domain
{
    public class WeatherForecastResponse
    {
        public Location Location { get; set; }
        public Current Current { get; set; }
        public Forecast Forecast { get; set; }
    }
}
