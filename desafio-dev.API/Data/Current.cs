using desafio_dev.API.Model;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace desafio_dev.API.Domain
{
    [ExcludeFromCodeCoverage]
    public class Current
    {
        public Current()
        {
            
        }
        public Current(CurrentModel current)
        {
            Id = current.Id;
            LastUpdated = current.LastUpdated;
            LastUpdatedEpoch = current.LastUpdatedEpoch;
            TempC = current.TempC;
            TempF = current.TempF;
            IsDay = current.IsDay;
            WindMph = current.WindMph;
            WindDegree = current.WindDegree;
            PressureMb = current.PressureMb;
        }
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
        [JsonPropertyName("wind_mph")]
        public float WindMph { get; set; }
        [JsonPropertyName("wind_kph")]
        public float WindKph { get; set; }
        [JsonPropertyName("wind_degree")]
        public int WindDegree { get; set; }        
        [JsonPropertyName("pressure_mb")]
        public float PressureMb { get; set; }

        public ICollection<Weather> Weather { get; set; }
    }
}