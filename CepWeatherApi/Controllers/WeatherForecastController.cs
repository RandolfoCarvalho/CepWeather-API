using Microsoft.AspNetCore.Mvc;
using CepWeatherApi.Models;
using CepWeatherApi.Models.ViewModels;
using System.Globalization;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Newtonsoft.Json.Linq;

namespace CepWeatherApi.Controllers
{
    public class WeatherForecastController : Controller
    {
        private readonly WeatherForecastService _weatherForecastService;

        public WeatherForecastController(WeatherForecastService weatherForecastService)
        {
            _weatherForecastService = weatherForecastService;
        }

        //Encontra todos as entidades no banco de dados
        public IActionResult Index()
        {
            var result = _weatherForecastService.FindAll();
            if (result == null)
            {
                return NotFound("O Id não existe");
            }
            return View(result);
        }

        [HttpGet]
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
        [HttpPost]
        public async Task<IActionResult> GetWeatherForecast(Weather weather)
        {
            try
            {
                var teste = $"Latitude: {weather.Latitude}, Longitude: {weather.Longitude}, Timezone: " +
                    $"{weather.Timezone}, Inicio: {weather.Inicio}, Fim: {weather.Fim}";
                var consulta = await _weatherForecastService.GetWeatherForecast(weather.Latitude, weather.Longitude, weather.Timezone,
                    weather.Inicio, weather.Fim);
                return Ok(new { Consulta = consulta, Teste = teste });

            } catch(Exception e)
            {
                return BadRequest("Não foi possivel consultar a previsão " + e.Message);
            }
        }
    }
}