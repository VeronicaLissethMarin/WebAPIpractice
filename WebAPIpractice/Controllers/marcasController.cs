using Microsoft.AspNetCore.Mvc;

namespace WebAPIpractice.Controllers
{
    public class marcasController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
