using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace ProgrammingGame.Api.Controllers
{
    public class HomeController : BaseController
    {
        public HomeController(IMapper mapper) : base (mapper)
        {
        }

        [HttpGet]
        public IActionResult Index()
        {
            return Ok("Home! Sweet Home!");
        }
    }
}
