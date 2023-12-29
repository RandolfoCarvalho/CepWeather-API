using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Text.Json.Serialization;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CepWeatherApi.Models
{
    public class Weather
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "O campo {0} precisa ser preenchido")]
        public double? Latitude { get; set; }
        [Required(ErrorMessage = "O campo {0} precisa ser preenchido")]
        public double? Longitude { get; set; }
        public string Timezone { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime Inicio { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime Fim { get; set; }

        public Weather()
        {

        }
        public Weather(double latitude, double longitude, string timezone, DateTime inicio, DateTime fim)
        {
            Latitude = latitude;
            Longitude = longitude;
            Timezone = timezone;
            Inicio = DateTime.Parse(inicio.ToString("yyyy-MM-dd"));
            Fim = DateTime.Parse(fim.ToString("yyyy-MM-dd"));
        }

    }
}
