using desafio_dev.API.Model;
using System.Text.Json.Serialization;

namespace desafio_dev.API.Domain;

public class Location
{
    public Location()
    {
        
    }
    public Location(LocationModel location)
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
    public int Id { get; set; }
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

    
    public ICollection<Weather> Weather { get; }
}