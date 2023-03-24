using Microsoft.AspNetCore.Mvc;

namespace WebAPIpractice.Controllers
{
    public class facultadesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
