using NLog;
using ProgrammingGame.Data.Entities;
using ProgrammingGame.Data.Repositories.Instances;
using ProgrammingGame.Data.Services.Instances;
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

        protected CharacterBehaviorBase(Character character)
        {
            Logger = LogManager.GetCurrentClassLogger();
            CharactersService = new CharactersService(new CharactersRepository(), new IndicatorsRepository(), new IndicatorTypesRepository(), new SystemActionsRepository());
            IndicatorsService = new IndicatorsService(new IndicatorsRepository());
            SystemActionsService = new SystemActionsService(new SystemActionsRepository());
            Character = character;
        }

        public abstract void GainAndLostPoints();
        public abstract void AnalyseIndicators();
        public abstract bool ShouldLevelUp();
        public abstract void LevelUp();
    }
}