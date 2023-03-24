using Microsoft.AspNetCore.Mvc;

namespace WebAPIpractice.Controllers
{
    public class usuariosController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
