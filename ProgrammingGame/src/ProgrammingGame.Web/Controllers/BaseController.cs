using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ProgrammingGame.Data.Entities;

namespace ProgrammingGame.Web.Controllers
{
    public abstract class BaseController : Controller
    {
        protected readonly IMapper Mapper;
        protected readonly UserManager<User> UserManager;

        protected BaseController(IMapper mapper, UserManager<User> userManager)
        {
            UserManager = userManager;
            Mapper = mapper;
        }

        protected User GetCurrentUser()
        {
            return UserManager.GetUserAsync(HttpContext.User).Result;
        }
    }
}
