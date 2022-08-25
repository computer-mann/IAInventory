using Microsoft.AspNetCore.Mvc;

namespace WebApi.Areas.Controllers
{
    public class Admin : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
