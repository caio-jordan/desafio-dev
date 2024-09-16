using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;
using desafio_dev.API.Domain;

namespace desafio_dev.API.Model
{
    [ExcludeFromCodeCoverage]
    public class DayModel
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