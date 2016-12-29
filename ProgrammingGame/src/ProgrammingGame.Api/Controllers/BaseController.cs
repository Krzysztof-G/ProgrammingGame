using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace ProgrammingGame.Api.Controllers
{
    public abstract class BaseController : Controller
    {
        protected readonly IMapper Mapper;

        protected BaseController(IMapper mapper)
        {
            Mapper = mapper;
        }
    }
}
