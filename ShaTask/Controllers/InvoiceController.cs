using Microsoft.AspNetCore.Mvc;

namespace ShaTask.Controllers
{
    public class InvoiceController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
