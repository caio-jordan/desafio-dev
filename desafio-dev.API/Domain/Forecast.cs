using System.ComponentModel.DataAnnotations;

namespace desafio_dev.API.Domain
{
    public class Forecast
    {
        [Key]
        public int Id { get; set; }
        public List<ForecastDay> Forecastday { get; set; }
    }

}
