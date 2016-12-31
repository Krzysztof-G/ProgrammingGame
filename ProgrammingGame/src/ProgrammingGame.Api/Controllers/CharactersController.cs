using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ProgrammingGame.Api.Filters;
using ProgrammingGame.Api.Models;
using ProgrammingGame.Api.Services.Interfaces;
using ProgrammingGame.Common.Enums;
using ProgrammingGame.Data.Domain.Entities;
using System;

namespace ProgrammingGame.Api.Controllers
{
    public class CharactersController : BaseController
    {
        private readonly ICharactersService _charactersService;

        public CharactersController(IMapper mapper, ICharactersService charactersService) : base(mapper)
        {
            _charactersService = charactersService;
        }

        [HttpGet]
        [CharacterLevelRestrictionFilter(0)]
        public IActionResult Get(Guid characterKey)
        {

            var characterFromDatabase = _charactersService.GetCharacterByKey(characterKey);
            if (characterFromDatabase == null)
                return NotFound();

            var characterDto = Mapper.Map<Character, CharacterDto>(characterFromDatabase);
            return Ok(characterDto);
        }

        [HttpPatch]
        [CharacterLevelRestrictionFilter(0)]
        public IActionResult GoSleep(Guid characterKey)
        {
            var characterFromDatabase = _charactersService.GetCharacterByKey(characterKey);
            if (characterFromDatabase == null)
                return NotFound();

            if (characterFromDatabase.State == (int)CharacterStates.Sleep)
            {
                ModelState.AddModelError("State", "You can go sleep, because you already sleep.");
                return BadRequest(ModelState);
            }
            
            _charactersService.SetCharacterState(characterFromDatabase, CharacterStates.Sleep);

            return Ok();
        }

        [HttpPatch]
        [CharacterLevelRestrictionFilter(0)]
        public IActionResult WakeUp(Guid characterKey)
        {
            var characterFromDatabase = _charactersService.GetCharacterByKey(characterKey);
            if (characterFromDatabase == null)
                return NotFound();

            if (characterFromDatabase.State == (int)CharacterStates.Idle)
            {
                ModelState.AddModelError("State", "You can wake up, because you already wake up.");
                return BadRequest(ModelState);
            }

            _charactersService.SetCharacterState(characterFromDatabase, CharacterStates.Idle);

            return Ok();
        }
    }
}
