using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProgrammingGame.Api.Filters;
using ProgrammingGame.Api.Models;
using ProgrammingGame.Common.Enums;
using ProgrammingGame.Data.Entities;
using ProgrammingGame.Data.Services.Interfaces;
using System;

namespace ProgrammingGame.Api.Controllers
{
    public class CharactersController : BaseController
    {
        private readonly ILogger<SystemController> _logger;
        private readonly ICharactersService _charactersService;

        public CharactersController(IMapper mapper, ILogger<SystemController> logger, ICharactersService charactersService) : base(mapper)
        {
            _logger = logger;
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
                ModelState.AddModelError("State", "You can go sleep, because you already a sleep.");
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
                ModelState.AddModelError("State", "You can wake up, because you already not sleeping.");
                return BadRequest(ModelState);
            }

            _charactersService.SetCharacterState(characterFromDatabase, CharacterStates.Idle);

            return Ok();
        }

        [HttpPatch]
        [CharacterLevelRestrictionFilter(1)]
        public IActionResult EatMalPreparedByMom(Guid characterKey)
        {
            var characterFromDatabase = _charactersService.GetCharacterByKey(characterKey);
            if (characterFromDatabase == null)
                return NotFound();

            if (characterFromDatabase.State != (int)CharacterStates.Idle)
            {
                ModelState.AddModelError("State", "You can eat during sleep.");
                return BadRequest(ModelState);
            }

            _charactersService.RenewIndicator(characterFromDatabase, IndicatorTypes.Hunger);

            return Ok();
        }

        [HttpPatch]
        [CharacterLevelRestrictionFilter(1)]
        public IActionResult DrinkWater(Guid characterKey)
        {
            var characterFromDatabase = _charactersService.GetCharacterByKey(characterKey);
            if (characterFromDatabase == null)
                return NotFound();

            if (characterFromDatabase.State != (int)CharacterStates.Idle)
            {
                ModelState.AddModelError("State", "You can drink during sleep.");
                return BadRequest(ModelState);
            }

            _charactersService.RenewIndicator(characterFromDatabase, IndicatorTypes.Thirst, 40);

            return Ok();
        }
    }
}
