using System.Text.Json.Serialization;

namespace desafio_dev.API.Domain
{
    public class Day
    {
        [JsonPropertyName("maxtemp_c")]
        public double MaxtempC { get; set; }
        [JsonPropertyName("maxtemp_f")]
        public double MaxtempF { get; set; }
        [JsonPropertyName("mintemp_c")]
        public double MintempC { get; set; }
        [JsonPropertyName("mintemp_f")]
        public double MintempF { get; set; }
        [JsonPropertyName("avgtemp_c")]
        public double AvgtempC { get; set; }
        [JsonPropertyName("avgtemp_f")]
        public double AvgtempF { get; set; }
        [JsonPropertyName("maxwind_mph")]
        public double MaxwindMph { get; set; }
        [JsonPropertyName("maxwind_kph")]
        public double MaxwindKph { get; set; }
        public Condition Condition { get; set; }
        public double Uv { get; set; }
    }
}