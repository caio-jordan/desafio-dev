using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace desafio_dev.API.Model
{
    [ExcludeFromCodeCoverage]
    public class ForecastDayModel
    {
        public string Date { get; set; }
        [JsonPropertyName("date_epoch")]
        public long DateEpoch { get; set; }
        public DayModel Day { get; set; }
    }
}