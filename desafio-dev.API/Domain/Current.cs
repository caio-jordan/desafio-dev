using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace desafio_dev.API.Domain
{
    public class Current
    {
        [Key]
        public int Id { get; set; }
        [JsonPropertyName("last_updated_epoch")]
        public decimal LastUpdatedEpoch { get; set; }
        [JsonPropertyName("last_updated")]
        public string LastUpdated { get; set; }
        [JsonPropertyName("temp_c")]
        public float TempC { get; set; }
        [JsonPropertyName("temp_f")]
        public float TempF { get; set; }
        [JsonPropertyName("is_day")]
        public int IsDay { get; set; }
        public Condition Condition { get; set; }
        [JsonPropertyName("wind_mph")]
        public float WindMph { get; set; }
        [JsonPropertyName("wind_kph")]
        public float WindKph { get; set; }
        [JsonPropertyName("wind_degree")]
        public int WindDegree { get; set; }
        [JsonPropertyName("wind_dir")]
        public string WindDir { get; set; }
        [JsonPropertyName("pressure_mb")]
        public float PressureMb { get; set; }
    }
}