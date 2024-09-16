using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace desafio_dev.API.Domain;

public class Location
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; }
    public string Region { get; set; }
    public string Country { get; set; }
    public float Lat { get; set; }
    public float Lon { get; set; }
    [JsonPropertyName("tz_id")]
    public string TzId { get; set; }
    [JsonPropertyName("local_time_epoch")]
    public float LocaltimeEpoch { get; set; }
    [JsonPropertyName("local_time")]
    public string Localtime { get; set; }
}