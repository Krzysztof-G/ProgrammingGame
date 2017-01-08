using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ProgrammingGame.Data.Entities;
using ProgrammingGame.Data.Services.Interfaces;
using ProgrammingGame.Web.ViewModels;

namespace ProgrammingGame.Web.Controllers
{
    public class CharactersController : BaseController
    {
        private readonly ICharactersService _charactersService;

        public CharactersController(IMapper mapper, UserManager<User> userManager, ICharactersService charactersService) : base(mapper, userManager)
        {
            _charactersService = charactersService;
        }

        [HttpGet]
        public IActionResult My()
        {
            var user = GetCurrentUser();
            var characterFromDatabase = _charactersService.GetCharacterById(user.CharacterId ?? 0);
            if (characterFromDatabase == null)
                return NotFound();

            var character = Mapper.Map<CharacterViewModel>(characterFromDatabase);
            return View(character);
        }
    }
}
