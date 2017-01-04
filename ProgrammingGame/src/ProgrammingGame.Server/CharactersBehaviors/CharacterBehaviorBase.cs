using NLog;
using ProgrammingGame.Data.Entities;
using ProgrammingGame.Data.Services.Interfaces;

namespace ProgrammingGame.Server.CharactersBehaviors
{
    public abstract class CharacterBehaviorBase
    {
        protected readonly Logger Logger;
        protected readonly ICharactersService CharactersService;
        protected readonly IIndicatorsService IndicatorsService;
        protected readonly ISystemActionsService SystemActionsService;
        protected readonly Character Character;

        protected CharacterBehaviorBase(Character character, ICharactersService charactersService, IIndicatorsService indicatorsService, ISystemActionsService systemActionsService)
        {
            Logger = LogManager.GetCurrentClassLogger();
            CharactersService = charactersService;
            IndicatorsService = indicatorsService;
            SystemActionsService = systemActionsService;
            Character = character;
        }

        public abstract void GainAndLostPoints();
        public abstract void AnalyseIndicators();
        public abstract bool ShouldLevelUp();
        public abstract void LevelUp();
    }
}