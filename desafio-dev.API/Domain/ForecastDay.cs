using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace desafio_dev.API.Domain
{
    public class ForecastDay
    {
        [Key]
        public int Id { get; set; }
        public string Date { get; set; }
        [JsonPropertyName("date_epoch")]
        public long DateEpoch { get; set; }
        public Day Day { get; set; }
    }
}