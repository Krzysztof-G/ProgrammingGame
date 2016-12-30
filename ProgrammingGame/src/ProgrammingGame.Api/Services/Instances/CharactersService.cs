using System;
using System.Linq;
using ProgrammingGame.Api.Services.Interfaces;
using ProgrammingGame.Common.Enums;
using ProgrammingGame.Data.Domain.Entities;
using ProgrammingGame.Data.Repositories.Interfaces;

namespace ProgrammingGame.Api.Services.Instances
{
    public class CharactersService : ICharactersService
    {
        private readonly ICharactersRepository _charactersRepository;

        public CharactersService(ICharactersRepository charactersRepository)
        {
            _charactersRepository = charactersRepository;
        }

        public Character GetCharacterByKey(Guid characterKey)
        {
            return _charactersRepository.FindBy(ch => ch.Key == characterKey).FirstOrDefault();
        }

        public void SetCharacterState(Character character, CharacterStates newState)
        {
            character.State = (int)newState;
            _charactersRepository.Edit(character);
            _charactersRepository.Save();
        }
    }
}