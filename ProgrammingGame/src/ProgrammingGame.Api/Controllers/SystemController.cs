using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace ProgrammingGame.Api.Controllers
{
    public class SystemController : BaseController
    {
        public SystemController(IMapper mapper) : base(mapper)
        {
        }

        [HttpGet]
        public IActionResult Ping()
        {
            return Ok("Pong");
        }
    }
}
