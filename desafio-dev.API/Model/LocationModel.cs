using desafio_dev.API.Domain;
using System.Text.Json.Serialization;

namespace desafio_dev.API.Model
{
    public class LocationModel
    {
        public LocationModel()
        {
            
        }

        public LocationModel(Location location)
        {
            Name = location.Name;
            Region = location.Region;
            Country = location.Country;
            Lat = location.Lat;
            Lon = location.Lon;
            TzId = location.TzId;            
            LocaltimeEpoch = location.LocaltimeEpoch;
            Localtime = location.Localtime;
        }
        public string Name { get; set; }
        public string Region { get; set; }
        public string Country { get; set; }
        public float Lat { get; set; }
        public float Lon { get; set; }
        [JsonPropertyName("tz_id")]
        public string TzId { get; set; }
        [JsonPropertyName("localtime_epoch")]
        public float LocaltimeEpoch { get; set; }
        [JsonPropertyName("localtime")]
        public string Localtime { get; set; }
    }
}
