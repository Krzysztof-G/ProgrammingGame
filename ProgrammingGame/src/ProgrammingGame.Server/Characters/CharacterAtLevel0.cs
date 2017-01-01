using ProgrammingGame.Common.Enums;
using ProgrammingGame.Data.Entities;
using System;

namespace ProgrammingGame.Server.Characters
{
    public class CharacterAtLevel0 : CharacterBase
    {
        private readonly long ExperienceForNextLevel = 16;

        private readonly int ExperienceGainForBeingRested = 1;
        private readonly int ExperienceLostForBeingSleepy = 1;
        private readonly int ExperienceLostForSleepToMuch = 1;

        public CharacterAtLevel0(Character character) : base(character)
        {
        }

        public override void GainPoints()
        {
            GainPointsForBeingWellRested();
            LostPointsForBeingSleepy();
            LostPointsForSleepToMuch();
        }

        public override void AnalyseIndicators()
        {
            throw new NotImplementedException();
        }

        public override bool ShouldLevelUp()
        {
            return Character.Experience >= ExperienceForNextLevel;
        }

        public override void LevelUp()
        {
            CharactersService.LevelUpCharacter(Character, Character.Experience - ExperienceForNextLevel);
            IndicatorsService.AddIndicator(Character.Id, IndicatorTypes.Thirst);
            IndicatorsService.AddIndicator(Character.Id, IndicatorTypes.Hunger);

        }

        public void GainPointsForBeingWellRested()
        {
            throw new NotImplementedException();
        }

        public void LostPointsForBeingSleepy()
        {
            throw new NotImplementedException();
        }

        public void LostPointsForSleepToMuch()
        {
            throw new NotImplementedException();
        }
    }
}
