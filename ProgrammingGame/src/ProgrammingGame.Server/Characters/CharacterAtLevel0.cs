using ProgrammingGame.Common;
using ProgrammingGame.Common.Enums;
using ProgrammingGame.Data.Entities;
using System;
using System.Linq;

namespace ProgrammingGame.Server.Characters
{
    public class CharacterAtLevel0 : CharacterBase
    {
        private readonly long ExperienceForNextLevel = 16;

        private readonly int EnergyPointsLostWhenNotSleeping = 6;
        private readonly int EnergyPointsGeinWhenSleeping = 12;
        private readonly TimeSpan SpanBeetwenEnergyAnalyseActions = new TimeSpan(1, 0, 0);

        private readonly int ExperienceGainForBeingRested = 1;
        private readonly TimeSpan SpanBeetwenExperienceGainForBeingRestedActions = new TimeSpan(1, 0, 0);

        private readonly int ExperienceLostForBeingSleepy = 1;
        private readonly TimeSpan SpanBeetwenExperienceLostForBeingSleepyActions = new TimeSpan(1, 0, 0);

        private readonly int ExperienceLostForSleepToMuch = 1;
        private readonly TimeSpan SpanBeetwenExperienceLostForSleepToMuchActions = new TimeSpan(1, 0, 0);

        public CharacterAtLevel0(Character character) : base(character)
        {
        }

        public override void GainAndLostPoints()
        {
            GainPointsForBeingRested();
            LostPointsForBeingSleepy();
            LostPointsForSleepToMuch();
        }

        public void GainPointsForBeingRested()
        {
            var action = Character.SystemActions.FirstOrDefault(x => x.TypeId == (int)SystemActionTypes.GainPointsForBeingRested);
            var energy = Character.Indicators.FirstOrDefault(x => x.IndicatorTypeId == (int)IndicatorTypes.Energy);
            if (action.LastExecutionTime.Add(SpanBeetwenExperienceGainForBeingRestedActions) <= CommonValues.ActaulaDateTime
                && energy.Value > 0)
            {
                CharactersService.AddExperienceToCharacter(Character, ExperienceGainForBeingRested);
                SystemActionsService.ResetLastExecutionTime(action);
            }
        }

        public void LostPointsForBeingSleepy()
        {
            var action = Character.SystemActions.FirstOrDefault(x => x.TypeId == (int)SystemActionTypes.LostPointsForBeingSleepy);
            var energy = Character.Indicators.FirstOrDefault(x => x.IndicatorTypeId == (int)IndicatorTypes.Energy);
            if (action.LastExecutionTime.Add(SpanBeetwenExperienceLostForBeingSleepyActions) <= CommonValues.ActaulaDateTime
                && energy.Value == 0)
            {
                CharactersService.AddExperienceToCharacter(Character, -ExperienceLostForBeingSleepy);
                SystemActionsService.ResetLastExecutionTime(action);
            }
        }

        private void LostPointsForSleepToMuch()
        {
            var action = Character.SystemActions.FirstOrDefault(x => x.TypeId == (int)SystemActionTypes.LostPointsForSleepToMuch);
            var energy = Character.Indicators.FirstOrDefault(x => x.IndicatorTypeId == (int)IndicatorTypes.Energy);
            if (action.LastExecutionTime.Add(SpanBeetwenExperienceLostForSleepToMuchActions) <= CommonValues.ActaulaDateTime
                && energy.Value == 100
                && Character.State == (int)CharacterStates.Sleep)
            {
                CharactersService.AddExperienceToCharacter(Character, -ExperienceLostForSleepToMuch);
                SystemActionsService.ResetLastExecutionTime(action);
            }
        }

        public override void AnalyseIndicators()
        {
            var energy = Character.Indicators.FirstOrDefault(x => x.IndicatorTypeId == (int)IndicatorTypes.Energy);
            var action = Character.SystemActions.FirstOrDefault(x => x.TypeId == (int)SystemActionTypes.SpanBeetwenEnergyAnalyse);

            if (Character.State == (int)CharacterStates.Idle
                && Character.LastStateChangeTime.Add(SpanBeetwenEnergyAnalyseActions) <= CommonValues.ActaulaDateTime
                && action.LastExecutionTime.Add(SpanBeetwenEnergyAnalyseActions) <= CommonValues.ActaulaDateTime)
            {
                IndicatorsService.ChangeValue(energy, -EnergyPointsLostWhenNotSleeping);
            }
            else if (Character.State == (int)CharacterStates.Sleep
                && Character.LastStateChangeTime.Add(SpanBeetwenEnergyAnalyseActions) <= CommonValues.ActaulaDateTime
                && action.LastExecutionTime.Add(SpanBeetwenEnergyAnalyseActions) <= CommonValues.ActaulaDateTime)
            {
                IndicatorsService.ChangeValue(energy, EnergyPointsGeinWhenSleeping);
            }
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

            SystemActionsService.AddSystemAction(Character.Id, SystemActionTypes.LostPointsForBeingThirst);
            SystemActionsService.AddSystemAction(Character.Id, SystemActionTypes.LostPointsForBeingHungry);
        }
    }
}
