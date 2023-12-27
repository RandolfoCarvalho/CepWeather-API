using Microsoft.AspNetCore.Mvc;

namespace CepWeatherApi.Controllers
{
    public class WeatherForecastController : Controller
    {
        private readonly WeatherForecastService weatherForecastService;

        public WeatherForecastController(WeatherForecastService weatherForecastService)
        {
            this.weatherForecastService = weatherForecastService;
        }
        
        [HttpGet("/WeatherForecast")]
        public async Task<IActionResult> GetWeatherForecast(double latitude, double longitude)
        {
            try
            {
                var forecast = await weatherForecastService.GetWeatherForecast(latitude, longitude);
                return Ok(forecast);
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao obter previsão do tempo: {ex.Message}");
            }
        }
    }
}