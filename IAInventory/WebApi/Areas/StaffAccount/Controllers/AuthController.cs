using Microsoft.AspNetCore.Mvc;

namespace WebApi.Areas.StaffAccount.Controllers
{
    public class AuthController : ControllerBase
    {
        public IActionResult Index()
        {
            return Ok() ;
        }
    }
}
