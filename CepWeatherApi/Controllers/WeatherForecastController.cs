using Microsoft.AspNetCore.Mvc;

namespace CepWeatherApi.Controllers
{
    public class WeatherForecastController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
