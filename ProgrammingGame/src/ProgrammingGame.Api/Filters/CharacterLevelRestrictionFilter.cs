using Microsoft.AspNetCore.Mvc.Filters;
using ProgrammingGame.Api.Exceptions;
using ProgrammingGame.Common.Enums;
using ProgrammingGame.Data.Entities;
using ProgrammingGame.Data.Repositories.Instances;
using ProgrammingGame.Data.Services.Instances;
using ProgrammingGame.Data.Services.Interfaces;
using System;

namespace ProgrammingGame.Api.Filters
{
    public class CharacterLevelRestrictionFilterAttribute : ActionFilterAttribute
    {
        private readonly int _minCharacterLevel;
        private readonly ICharactersService _charactersService;

        public CharacterLevelRestrictionFilterAttribute(int minCharacterLevel)
        {
            _charactersService = new CharactersService(new CharactersRepository());
            _minCharacterLevel = minCharacterLevel;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            string characterKeyArgument = context.ActionArguments["characterKey"].ToString();
            CheckWhateverCharacterKeyIsOnArgumentList(characterKeyArgument);
            Guid characterKey = GetCharacterKey(characterKeyArgument);
            var character = _charactersService.GetCharacterByKey(characterKey);
            CheckWhateverCharacterExists(character);
            CheckWhateverCharacterHasSufficientLevel(character);
        }

        private void CheckWhateverCharacterKeyIsOnArgumentList(string characterKeyArgument)
        {
            if (string.IsNullOrEmpty(characterKeyArgument))
            {
                throw new ApiException("Missing character key argument.", ErrorCodes.MissingArgument);
            }
        }

        private Guid GetCharacterKey(string characterKeyArgument)
        {
            Guid characterKey;
            if (!Guid.TryParse(characterKeyArgument, out characterKey))
            {
                throw new ApiException("Invalid character key format.", ErrorCodes.WrongArgumentFormat);
            }

            return characterKey;
        }

        private void CheckWhateverCharacterHasSufficientLevel(Character character)
        {
            if (character.Level < _minCharacterLevel)
            {
                throw new ActionNotAvailableException("Level of your character is to low for this action.", ErrorCodes.CharacterDoesNotMeetRequirements);
            }
        }

        private void CheckWhateverCharacterExists(Character character)
        {
            if (character == null)
            {
                throw new ActionNotAvailableException("Invalid character key.", ErrorCodes.NonExistentEntity);
            }
        }
    }
}