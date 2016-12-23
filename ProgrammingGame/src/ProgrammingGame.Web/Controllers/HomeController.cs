using Microsoft.AspNetCore.Mvc;

namespace ProgrammingGame.Web.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return Ok("Home! Sweet Home!");
        }
    }
}
