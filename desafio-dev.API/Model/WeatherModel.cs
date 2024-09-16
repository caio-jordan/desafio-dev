using desafio_dev.API.Domain;

namespace desafio_dev.API.Model
{
    public class WeatherModel
    {
        public CurrentModel Current { get; set; }
        public LocationModel Location { get; set; }
    }
}