using ProgrammingGame.Data.Entities;
using ProgrammingGame.Data.Repositories.Instances;
using ProgrammingGame.Server.Services.Instances;
using ProgrammingGame.Server.Services.Interfaces;

namespace ProgrammingGame.Server.Characters
{
    public abstract class CharacterBase
    {
        protected readonly ICharactersService CharactersService;
        protected readonly IIndicatorsService IndicatorsService;
        protected readonly ISystemActionsService SystemActionsService;
        protected readonly Character Character;

        protected CharacterBase(Character character)
        {
            CharactersService = new CharactersService(new CharactersRepository());
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