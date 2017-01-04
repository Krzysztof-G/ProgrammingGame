using NLog;
using ProgrammingGame.Data.Entities;
using ProgrammingGame.Data.Services.Interfaces;
using ProgrammingGame.Server.CharactersBehaviors;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ProgrammingGame.Server
{
    public class MainServerFlow
    {
        private readonly Logger _logger = LogManager.GetCurrentClassLogger();
        private readonly ICharactersService _charactersService;
        private readonly IIndicatorsService _indicatorsService;
        private readonly ISystemActionsService _systemActionsService;

        public MainServerFlow(ICharactersService charactersService, IIndicatorsService indicatorsService, ISystemActionsService systemActionsService)
        {
            _charactersService = charactersService;
            _indicatorsService = indicatorsService;
            _systemActionsService = systemActionsService;
        }


        public void Run()
        {
            _logger.ConditionalDebug("Main flow start.");
            while (true)
            {
                var characters = GetCharacters();
                var charactersBehaviors = ConvertToCharacterBehaviors(characters);
                foreach (var characterBehavior in charactersBehaviors)
                {
                    characterBehavior.AnalyseIndicators();
                    characterBehavior.GainAndLostPoints();
                    if (characterBehavior.ShouldLevelUp())
                        characterBehavior.LevelUp();
                }
            }
        }

        private List<Character> GetCharacters()
        {
            return _charactersService.GetAllWithRelatedEntities().Where(x => x.IsActive).ToList();
        }

        private List<CharacterBehaviorBase> ConvertToCharacterBehaviors(List<Character> characters)
        {
            return characters.Select(GetBehaviorClass).ToList();
        }

        private CharacterBehaviorBase GetBehaviorClass(Character character)
        {
            switch (character.Level)
            {
                case 0: return new CharacterBehaviorAtLevel0(character, _charactersService, _indicatorsService, _systemActionsService);
                case 1: return new CharacterBehaviorAtLevel1(character, _charactersService, _indicatorsService, _systemActionsService);
                default:
                    throw new InvalidDataException();
            }
        }
    }
}
