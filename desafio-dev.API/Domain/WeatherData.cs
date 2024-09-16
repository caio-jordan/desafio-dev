using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace desafio_dev.API.Domain;

[Table("Weather")]
public class WeatherData
{
    [Key]
    public int Id { get; set; }
    public Current Current { get; set; }
    public Location Location { get; set; }

}