using Microsoft.AspNetCore.Mvc;

namespace WebAPIpractice.Controllers
{
    public class carrerasController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
