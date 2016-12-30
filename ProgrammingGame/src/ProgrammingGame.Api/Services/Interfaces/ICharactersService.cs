using System;
using ProgrammingGame.Common.Enums;
using ProgrammingGame.Data.Domain.Entities;

namespace ProgrammingGame.Api.Services.Interfaces
{
    public interface ICharactersService
    {
        Character GetCharacterByKey(Guid characterKey);
        void SetCharacterState(Character character, CharacterStates newState);
    }
}