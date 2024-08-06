using Microsoft.AspNetCore.Mvc;

namespace ShaTask.Controllers
{
    public class BranchController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
