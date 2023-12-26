using Microsoft.AspNetCore.Mvc;

namespace CepWeatherApi.Controllers
{
    public class CepController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
