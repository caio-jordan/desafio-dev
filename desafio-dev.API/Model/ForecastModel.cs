using System.Diagnostics.CodeAnalysis;

namespace desafio_dev.API.Model
{
    [ExcludeFromCodeCoverage]
    public class ForecastModel
    {
        public int Id { get; set; }
        public List<ForecastDayModel> Forecastday { get; set; }
    }

}
