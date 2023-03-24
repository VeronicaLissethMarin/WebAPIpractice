using Microsoft.AspNetCore.Mvc;

namespace WebAPIpractice.Controllers
{
    public class reservasController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
