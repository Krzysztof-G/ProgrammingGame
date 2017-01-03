using System;
using System.Linq;
using ProgrammingGame.Common;
using ProgrammingGame.Common.Enums;
using ProgrammingGame.Data.Entities;

namespace ProgrammingGame.Server.CharactersBehaviors
{
    public class CharacterBehaviorAtLevel0 : CharacterBehaviorBase
    {
        private readonly long ExperienceForNextLevel = 16;

        private readonly int EnergyPointsLostWhenNotSleeping = 6;
        private readonly int EnergyPointsGeinWhenSleeping = 12;

        private readonly TimeSpan SpanBeetwenEnergyAnalyseActions = new TimeSpan(1, 0, 0);

        private readonly int ExperienceGainForBeingRested = 1;
        private readonly int ExperienceLostForBeingSleepy = 1;
        private readonly int ExperienceLostForSleepToMuch = 1;

        public CharacterBehaviorAtLevel0(Character character) : base(character)
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
            if (SystemActionsService.AcionShouldBeExecuted(action)
                && energy.Value > 0)
            {
                CharactersService.AddExperienceToCharacter(Character, ExperienceGainForBeingRested);
                SystemActionsService.ResetLastExecutionTime(action);
                Logger.ConditionalDebug($"Character with id: {Character.Id} execute action GainPointsForBeingRested at {action.LastExecutionTime}.");
            }
        }

        public void LostPointsForBeingSleepy()
        {
            var action = Character.SystemActions.FirstOrDefault(x => x.TypeId == (int)SystemActionTypes.LostPointsForBeingSleepy);
            var energy = Character.Indicators.FirstOrDefault(x => x.IndicatorTypeId == (int)IndicatorTypes.Energy);
            if (SystemActionsService.AcionShouldBeExecuted(action)
                && energy.Value == 0)
            {
                CharactersService.AddExperienceToCharacter(Character, -ExperienceLostForBeingSleepy);
                SystemActionsService.ResetLastExecutionTime(action);
                Logger.ConditionalDebug($"Character with id: {Character.Id} execute action LostPointsForBeingSleepy at {action.LastExecutionTime}.");
            }
        }

        private void LostPointsForSleepToMuch()
        {
            var action = Character.SystemActions.FirstOrDefault(x => x.TypeId == (int)SystemActionTypes.LostPointsForSleepToMuch);
            var energy = Character.Indicators.FirstOrDefault(x => x.IndicatorTypeId == (int)IndicatorTypes.Energy);
            if (SystemActionsService.AcionShouldBeExecuted(action)
                && energy.Value == 100
                && Character.State == (int)CharacterStates.Sleep)
            {
                CharactersService.AddExperienceToCharacter(Character, -ExperienceLostForSleepToMuch);
                SystemActionsService.ResetLastExecutionTime(action);
                Logger.ConditionalDebug($"Character with id: {Character.Id} execute action LostPointsForSleepToMuch at {action.LastExecutionTime}.");
            }
        }

        public override void AnalyseIndicators()
        {
            var energy = Character.Indicators.FirstOrDefault(x => x.IndicatorTypeId == (int)IndicatorTypes.Energy);
            var action = Character.SystemActions.FirstOrDefault(x => x.TypeId == (int)SystemActionTypes.SpanBeetwenEnergyAnalyse);

            if (Character.State == (int)CharacterStates.Idle
                && Character.LastStateChangeTime.Add(SpanBeetwenEnergyAnalyseActions) <= CommonValues.ActaulaDateTime
                && SystemActionsService.AcionShouldBeExecuted(action))
            {
                IndicatorsService.ChangeValue(energy, -EnergyPointsLostWhenNotSleeping);
                CharactersService.ResetLastStateChangeTime(Character);
                Logger.ConditionalDebug($"Character with id: {Character.Id} execute action AnalyseIndicators at {action.LastExecutionTime}.");
            }
            else if (Character.State == (int)CharacterStates.Sleep
                && Character.LastStateChangeTime.Add(SpanBeetwenEnergyAnalyseActions) <= CommonValues.ActaulaDateTime
                && SystemActionsService.AcionShouldBeExecuted(action))
            {
                IndicatorsService.ChangeValue(energy, EnergyPointsGeinWhenSleeping);
                CharactersService.ResetLastStateChangeTime(Character);
                Logger.ConditionalDebug($"Character with id: {Character.Id} execute action AnalyseIndicators at {action.LastExecutionTime}.");
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
            Logger.ConditionalDebug($"Character with id: {Character.Id} level up form {Character.Level - 1} level to {Character.Level} at {CommonValues.ActaulaDateTime}.");
        }
    }
}
