using Microsoft.AspNetCore.Mvc;
using CepWeatherApi.Models;
using CepWeatherApi.Models.ViewModels;
using System.Globalization;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Newtonsoft.Json.Linq;
using System.Data;
using Newtonsoft.Json;
using System.Text.Json.Nodes;

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

        public async Task<IActionResult> Edit(int? id)
        {
            if(id == null)
            {
                return RedirectToAction(nameof(Error), new { Message = "Id not provided" });
            }

            var obj = _weatherForecastService.FindById(id.Value);
            WeatherFormView viewModel = new WeatherFormView { Weather = obj };
            return View(viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, Weather weather)
        {
            if (!ModelState.IsValid)
            {
                var viewModel = new WeatherFormView { Weather = weather };
                return View(viewModel);
            }
            if (id != weather.Id)
            {
                return RedirectToAction(nameof(Error), new { Message = "Id mismatch" });
            }
            try
            {
                await _weatherForecastService.Update(weather);
                return RedirectToAction(nameof(Index));
            } catch (DBConcurrencyException e)
            {
                return RedirectToAction(nameof(Error), new { Message = e.Message });
            }



        }

        public IActionResult Details(int? id)
        {
            if(id == null)
            {
                return RedirectToAction(nameof(Error), new { Message = "Id not provided" });
            }
            var obj =  _weatherForecastService.FindById(id.Value);
            if(obj == null)
            {
                return RedirectToAction(nameof(Error), new { Message = "obj not found" });
            }
            return View(obj);

        }

        public class HourlyData
        {
            public List<string> Time { get; set; }
            public List<double> Temperature_2m { get; set; }
        }

        [HttpGet]
        public async Task<IActionResult> QueryWeather(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { Message = "Id not provided" });
            }
            var obj = _weatherForecastService.FindById(id.Value);
            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { Message = "obj not found" });
            }
            try
            {
                var consulta = await _weatherForecastService.GetWeatherForecast(obj.Latitude, obj.Longitude, obj.Timezone,
                    obj.Inicio, obj.Fim);
                return Ok(consulta);

            }
            catch (Exception e)
            {
                return BadRequest("Não foi possivel consultar a previsão " + e.Message);
            }
        }



    }
}