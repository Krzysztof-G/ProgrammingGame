using System;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ProgrammingGame.Api.Models;
using ProgrammingGame.Common.Enums;
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

        [HttpGet]
        public IActionResult Get(Guid characterKey)
        {
            var characterFromDatabase = _charactersRepository.GetCharacterByKey(characterKey);
            if (characterFromDatabase == null)
                return NotFound();

            var characterDto = Mapper.Map<Character, CharacterDto>(characterFromDatabase);
            return Ok(characterDto);
        }

        [HttpPatch]
        public IActionResult GoSleep(Guid characterKey)
        {
            var characterFromDatabase = _charactersRepository.GetCharacterByKey(characterKey);
            if (characterFromDatabase == null)
                return NotFound();

            if (characterFromDatabase.State == (int)CharacterStates.Sleep)
            {
                throw new NotImplementedException();
            }
            characterFromDatabase.State = (int) CharacterStates.Sleep;
            _charactersRepository.Edit(characterFromDatabase);
            _charactersRepository.Save();

            return Ok();
        }

        [HttpPatch]
        public IActionResult WakeUp(Guid characterKey)
        {
            var characterFromDatabase = _charactersRepository.GetCharacterByKey(characterKey);
            if (characterFromDatabase == null)
                return NotFound();

            if (characterFromDatabase.State == (int)CharacterStates.Idle)
            {
                throw new NotImplementedException();
            }

            characterFromDatabase.State = (int)CharacterStates.Idle;
            _charactersRepository.Edit(characterFromDatabase);
            _charactersRepository.Save();

            return Ok();
        }
    }
}
