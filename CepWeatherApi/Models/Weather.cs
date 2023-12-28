using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CepWeatherApi.Models
{
    public class Weather
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "O campo {0} precisa ser preenchido")]
        public double? Latitude { get; set; }
        [Required(ErrorMessage = "O campo {0} precisa ser preenchido")]
        public double? Longitude { get; set; }

        public Weather()
        {

        }
        public Weather(double latitude, double longitude)
        {
            Latitude = latitude;
            Longitude = longitude;
        }

    }
}
