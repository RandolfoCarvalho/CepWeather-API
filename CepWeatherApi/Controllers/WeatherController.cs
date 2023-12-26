using Microsoft.AspNetCore.Mvc;

namespace CepWeatherApi.Controllers
{
    public class WeatherController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
