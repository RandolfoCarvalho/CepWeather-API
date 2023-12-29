using Microsoft.AspNetCore.Mvc;
using CepWeatherApi.Models;
using CepWeatherApi.Models.ViewModels;
using System.Globalization;
using static System.Runtime.InteropServices.JavaScript.JSType;

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
            if (!ModelState.IsValid)
            {
                var viewModel = new WeatherFormView { Weather = weather};
                return View(viewModel);
            }
            await _weatherForecastService.InsertAsync(weather);
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public async Task<IActionResult> GetWeatherForecast(double latitude, double longitude, string timezone, DateTime inicio, DateTime fim)
        {
            try
            {
                var consulta = await _weatherForecastService.GetWeatherForecast(latitude, longitude, timezone, inicio, fim);
                return Ok(consulta);

            } catch(Exception e)
            {
                return BadRequest("Não foi possivel consultar a previsão " + e.Message);
            }
        }
    }
}