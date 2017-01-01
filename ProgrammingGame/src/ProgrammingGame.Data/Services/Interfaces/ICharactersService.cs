using System;
using ProgrammingGame.Common.Enums;
using ProgrammingGame.Data.Entities;

namespace ProgrammingGame.Data.Services.Interfaces
{
    public interface ICharactersService
    {
        Character GetCharacterByKey(Guid characterKey);
        void SetCharacterState(Character character, CharacterStates newState);
        void AddExperienceToCharacter(Character character, long experienceToAdd);
        void LevelUpCharacter(Character character, long pointsSurplus);
    }
}