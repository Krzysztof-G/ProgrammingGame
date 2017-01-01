﻿using ProgrammingGame.Data.Entities;
using ProgrammingGame.Data.Repositories.Instances;
using ProgrammingGame.Data.Services.Instances;
using ProgrammingGame.Data.Services.Interfaces;

namespace ProgrammingGame.Server.CharactersBehaviors
{
    public abstract class CharacterBehaviorBase
    {
        protected readonly ICharactersService CharactersService;
        protected readonly IIndicatorsService IndicatorsService;
        protected readonly ISystemActionsService SystemActionsService;
        protected readonly Character Character;

        protected CharacterBehaviorBase(Character character)
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