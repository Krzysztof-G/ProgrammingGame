using ProgrammingGame.Data.Entities;
using ProgrammingGame.Data.Repositories.Interfaces;
using ProgrammingGame.Server.CharactersBehaviors;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ProgrammingGame.Data.Repositories.Instances;
using ProgrammingGame.Data.Services.Instances;
using ProgrammingGame.Data.Services.Interfaces;

namespace ProgrammingGame.Server
{
    public class MainServerFlow
    {
        private readonly ICharactersService _charactersService;

        public MainServerFlow()
        {
            _charactersService = new CharactersService(new CharactersRepository(), new IndicatorsRepository(), new IndicatorTypesRepository(), new SystemActionsRepository());
        }


        public void Run()
        {
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
            return _charactersService.GetAllWithRelatedEntities().ToList();
        }

        private List<CharacterBehaviorBase> ConvertToCharacterBehaviors(List<Character> characters)
        {
            return characters.Select(GetBehaviorClass).ToList();
        }

        private CharacterBehaviorBase GetBehaviorClass(Character character)
        {
            switch (character.Level)
            {
                case 0: return new CharacterBehaviorAtLevel0(character);
                default:
                    throw new InvalidDataException();
            }
        }
    }
}
