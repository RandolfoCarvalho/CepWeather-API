using Microsoft.AspNetCore.Mvc;
using CepWeatherApi.Models;

namespace CepWeatherApi.Controllers
{
    public class WeatherForecastController : Controller
    {
        private readonly WeatherForecastService _weatherForecastService;

        public WeatherForecastController(WeatherForecastService weatherForecastService)
        {
            _weatherForecastService = weatherForecastService;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> GetWeatherForecast(double latitude, double longitude)
        {
            try
            {
                var consulta = await _weatherForecastService.GetWeatherForecast(latitude, longitude);

                return Ok(consulta);

            } catch(Exception e)
            {
                return BadRequest("Não foi possivel consultar a previsão" + e.Message);
            }
        }




    }
}