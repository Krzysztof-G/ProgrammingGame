using System;
using System.Linq;
using ProgrammingGame.Common.Enums;
using ProgrammingGame.Data.Entities;
using ProgrammingGame.Data.Repositories.Interfaces;
using ProgrammingGame.Data.Services.Interfaces;

namespace ProgrammingGame.Data.Services.Instances
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

        public void AddExperienceToCharacter(Character character, long experienceToAdd)
        {
            character.Experience += experienceToAdd;
            _charactersRepository.Edit(character);
            _charactersRepository.Save();
        }

        public void LevelUpCharacter(Character character, long pointsSurplus)
        {
            character.Level++;
            character.Experience = pointsSurplus;
            _charactersRepository.Edit(character);
            _charactersRepository.Save();
        }
    }
}