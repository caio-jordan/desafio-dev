using desafio_dev.API.Model;

namespace desafio_dev.API.Domain;


public class Weather
{
    public Weather()
    {
        
    }

    public Weather(WeatherModel weatherModel)
    {
        
        Current = new(weatherModel.Current);
        Location = new( weatherModel.Location);
    }


    public int Id { get; set; }

    public int IdCurrent { get; set; }
    public Current Current { get; set; }
    public int IdLocation { get; set; }
    public Location Location { get; set; }

}