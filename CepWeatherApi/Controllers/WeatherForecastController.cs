using Microsoft.AspNetCore.Mvc;
using CepWeatherApi.Models;
using CepWeatherApi.Models.ViewModels;

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
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Weather weather)
        {
            if(!ModelState.IsValid)
            {
                var viewModel = new WeatherFormView { Weather = weather};
                return View(viewModel);
            }
            await _weatherForecastService.InserAsync(weather);
            return RedirectToAction(nameof(Index));
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