using Microsoft.AspNetCore.Mvc;

namespace ShaTask.Controllers
{
    public class CityController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
