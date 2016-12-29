using Microsoft.AspNetCore.Mvc;

namespace ProgrammingGame.Api.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return Ok("Home! Sweet Home!");
        }
    }
}
