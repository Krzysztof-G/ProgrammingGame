using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using ProgrammingGame.Api.Exceptions;
using ProgrammingGame.Common.Enums;
using ProgrammingGame.Data.Entities;
using ProgrammingGame.Data.Services.Interfaces;
using System;
using Microsoft.Extensions.Logging;

namespace ProgrammingGame.Api.Filters
{
    public class CharacterLevelRestrictionFilterAttribute : ActionFilterAttribute
    {
        private readonly int _minCharacterLevel;
        private ILogger<CharacterLevelRestrictionFilterAttribute> _logger;

        public CharacterLevelRestrictionFilterAttribute(int minCharacterLevel)
        {
            _minCharacterLevel = minCharacterLevel;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            ICharactersService charactersService = context.HttpContext.RequestServices.GetService<ICharactersService>();
            _logger = context.HttpContext.RequestServices.GetService<ILoggerFactory>().CreateLogger<CharacterLevelRestrictionFilterAttribute>();

            string characterKeyArgument = context.ActionArguments["characterKey"].ToString();
            CheckWhateverCharacterKeyIsOnArgumentList(characterKeyArgument);
            Guid characterKey = GetCharacterKey(characterKeyArgument);
            var character = charactersService.GetCharacterByKey(characterKey);
            CheckWhateverCharacterExists(character);
            CheckWhateverCharacterHasSufficientLevel(character);
        }

        private void CheckWhateverCharacterKeyIsOnArgumentList(string characterKeyArgument)
        {
            if (string.IsNullOrEmpty(characterKeyArgument))
            {
                _logger.LogDebug("Missing character key argument.");
                throw new ApiException("Missing character key argument.", ErrorCodes.MissingArgument);
            }
        }

        private Guid GetCharacterKey(string characterKeyArgument)
        {
            Guid characterKey;
            if (!Guid.TryParse(characterKeyArgument, out characterKey))
            {
                _logger.LogDebug("Invalid character key format.");
                throw new ApiException("Invalid character key format.", ErrorCodes.WrongArgumentFormat);
            }

            return characterKey;
        }

        private void CheckWhateverCharacterHasSufficientLevel(Character character)
        {
            if (character.Level < _minCharacterLevel)
            {
                _logger.LogDebug("Level of your character is to low for this action.");
                throw new ActionNotAvailableException("Level of your character is to low for this action.", ErrorCodes.CharacterDoesNotMeetRequirements);
            }
        }

        private void CheckWhateverCharacterExists(Character character)
        {
            if (character == null)
            {
                _logger.LogDebug("Invalid character key.");
                throw new ActionNotAvailableException("Invalid character key.", ErrorCodes.NonExistentEntity);
            }
        }
    }
}