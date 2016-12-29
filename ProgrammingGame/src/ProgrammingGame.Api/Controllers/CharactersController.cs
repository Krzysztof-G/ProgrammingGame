using System;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ProgrammingGame.Api.Models;
using ProgrammingGame.Data.Domain.Entities;
using ProgrammingGame.Data.Repositories.Interfaces;

namespace ProgrammingGame.Api.Controllers
{
    public class CharactersController : BaseController
    {
        private readonly ICharactersRepository _charactersRepository;

        public CharactersController(IMapper mapper, ICharactersRepository charactersRepository) : base(mapper)
        {
            _charactersRepository = charactersRepository;
        }

        public IActionResult Get(Guid characterKey)
        {
            var characterFromDatabase = _charactersRepository.FindBy(c => c.Key == characterKey).FirstOrDefault();

            if (characterFromDatabase == null)
                return NotFound();

            var characterDto = Mapper.Map<Character, CharacterDto>(characterFromDatabase);

            return Ok(characterDto);
        }
    }
}
