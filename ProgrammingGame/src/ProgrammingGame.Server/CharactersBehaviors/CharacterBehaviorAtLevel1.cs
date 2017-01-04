using System;
using System.Linq;
using ProgrammingGame.Common;
using ProgrammingGame.Common.Enums;
using ProgrammingGame.Data.Entities;
using ProgrammingGame.Data.Services.Interfaces;

namespace ProgrammingGame.Server.CharactersBehaviors
{
    public class CharacterBehaviorAtLevel1 : CharacterBehaviorBase
    {
        public CharacterBehaviorAtLevel1(Character character, ICharactersService charactersService, IIndicatorsService indicatorsService, ISystemActionsService systemActionsService) : base(character, charactersService, indicatorsService, systemActionsService)
        {
        }

        #region Points Analyze

        private readonly int ExperienceGainPointsForBeingRestedNotHungryAndNotThirsty = 2;
        private readonly int ExperienceLostForBeingSleepy = 1;
        private readonly int ExperienceLostForSleepToMuch = 1;
        private readonly int ExperienceLostForBeingHungry = 5;
        private readonly int ExperienceLostForBeingThirsty = 2;

        public override void GainAndLostPoints()
        {
            GainPointsForBeingRestedNotHungryAndNotThirsty();
            LostPointsForBeingSleepy();
            LostPointsForSleepToMuch();
            LostPointsForBeingHungry();
            LostPointsForBeingThirsty();
        }

        private void GainPointsForBeingRestedNotHungryAndNotThirsty()
        {
            var action = Character.SystemActions.FirstOrDefault(x => x.TypeId == (int)SystemActionTypes.GainPointsForBeingRestedNotHungryAndNotThirsty);
            var energy = Character.Indicators.FirstOrDefault(x => x.IndicatorTypeId == (int)IndicatorTypes.Energy);
            var hunger = Character.Indicators.FirstOrDefault(x => x.IndicatorTypeId == (int)IndicatorTypes.Hunger);
            var thirst = Character.Indicators.FirstOrDefault(x => x.IndicatorTypeId == (int)IndicatorTypes.Thirst);

            if (SystemActionsService.AcionShouldBeExecuted(action)
                && energy.Value > 0
                && hunger.Value > 0
                && thirst.Value > 0)
            {
                CharactersService.AddExperienceToCharacter(Character, ExperienceGainPointsForBeingRestedNotHungryAndNotThirsty);
                SystemActionsService.ResetLastExecutionTime(action);
                Logger.ConditionalDebug($"Character with id: {Character.Id} execute action GainPointsForBeingRestedNotHungryAndNotThirsty at {action.LastExecutionTime}.");
            }
        }

        private void LostPointsForBeingSleepy()
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

        private void LostPointsForBeingHungry()
        {
            var action = Character.SystemActions.FirstOrDefault(x => x.TypeId == (int)SystemActionTypes.LostPointsForBeingHungry);
            var hunger = Character.Indicators.FirstOrDefault(x => x.IndicatorTypeId == (int)IndicatorTypes.Hunger);

            if (SystemActionsService.AcionShouldBeExecuted(action)
                && hunger.Value == 0)
            {
                CharactersService.AddExperienceToCharacter(Character, -ExperienceLostForBeingHungry);
                SystemActionsService.ResetLastExecutionTime(action);
                Logger.ConditionalDebug($"Character with id: {Character.Id} execute action LostPointsForBeingHungry at {action.LastExecutionTime}.");
            }
        }

        private void LostPointsForBeingThirsty()
        {
            var action = Character.SystemActions.FirstOrDefault(x => x.TypeId == (int)SystemActionTypes.LostPointsForBeingThirst);
            var thirst = Character.Indicators.FirstOrDefault(x => x.IndicatorTypeId == (int)IndicatorTypes.Thirst);

            if (SystemActionsService.AcionShouldBeExecuted(action)
                && thirst.Value == 0)
            {
                CharactersService.AddExperienceToCharacter(Character, -ExperienceLostForBeingThirsty);
                SystemActionsService.ResetLastExecutionTime(action);
                Logger.ConditionalDebug($"Character with id: {Character.Id} execute action LostPointsForBeingThirsty at {action.LastExecutionTime}.");
            }
        }

        #endregion Points Analyze

        #region Indicators Analyze

        private readonly int EnergyPointsLostWhenNotSleeping = 6;
        private readonly int EnergyPointsGeinWhenSleeping = 12;
        private readonly TimeSpan SpanBeetwenEnergyAnalyzeActions = new TimeSpan(1, 0, 0);

        private readonly int HungerPointsLostWhenNotSleeping = 10;
        private readonly int HungerPointsLostWhenSleeping = 5;
        private readonly TimeSpan SpanBeetwenHungerAnalyzeActions = new TimeSpan(1, 0, 0);

        private readonly int ThirstPointsLostWhenNotSleeping = 16;
        private readonly int ThirstPointsLostWhenSleeping = 8;
        private readonly TimeSpan SpanBeetwenThirstAnalyzeActions = new TimeSpan(1, 0, 0);

        public override void AnalyseIndicators()
        {
            AnalyseEnergyIndicator();
            AnalyseHungerIndicator();
            AnalyseThirstIndicator();
        }

        private void AnalyseEnergyIndicator()
        {
            var energy = Character.Indicators.FirstOrDefault(x => x.IndicatorTypeId == (int)IndicatorTypes.Energy);
            var action = Character.SystemActions.FirstOrDefault(x => x.TypeId == (int)SystemActionTypes.SpanBeetwenEnergyAnalyze);

            if (Character.State == (int) CharacterStates.Idle
                && CharactersService.EnoughTimeHasPassedFromPreviousStatusAnalyze(Character, SpanBeetwenEnergyAnalyzeActions)
                && SystemActionsService.AcionShouldBeExecuted(action))
            {
                IndicatorsService.ChangeValue(energy, -EnergyPointsLostWhenNotSleeping);
                CharactersService.ResetLastStateChangeTime(Character);
                Logger.ConditionalDebug($"Character with id: {Character.Id} execute action AnalyseEnergyIndicator at {action.LastExecutionTime}.");
            }
            else if (Character.State == (int) CharacterStates.Sleep
                     && CharactersService.EnoughTimeHasPassedFromPreviousStatusAnalyze(Character, SpanBeetwenEnergyAnalyzeActions)
                     && SystemActionsService.AcionShouldBeExecuted(action))
            {
                IndicatorsService.ChangeValue(energy, EnergyPointsGeinWhenSleeping);
                CharactersService.ResetLastStateChangeTime(Character);
                Logger.ConditionalDebug($"Character with id: {Character.Id} execute action AnalyseEnergyIndicator at {action.LastExecutionTime}.");
            }
        }

        private void AnalyseHungerIndicator()
        {
            var hunger = Character.Indicators.FirstOrDefault(x => x.IndicatorTypeId == (int)IndicatorTypes.Hunger);
            var action = Character.SystemActions.FirstOrDefault(x => x.TypeId == (int)SystemActionTypes.SpanBeetwenHungerAnalyze);

            if (Character.State == (int)CharacterStates.Idle
                && CharactersService.EnoughTimeHasPassedFromPreviousStatusAnalyze(Character, SpanBeetwenHungerAnalyzeActions)
                && SystemActionsService.AcionShouldBeExecuted(action))
            {
                IndicatorsService.ChangeValue(hunger, -HungerPointsLostWhenNotSleeping);
                CharactersService.ResetLastStateChangeTime(Character);
                Logger.ConditionalDebug($"Character with id: {Character.Id} execute action AnalyseHungerIndicator at {action.LastExecutionTime}.");
            }
            else if (Character.State == (int)CharacterStates.Sleep
                     && CharactersService.EnoughTimeHasPassedFromPreviousStatusAnalyze(Character, SpanBeetwenHungerAnalyzeActions)
                     && SystemActionsService.AcionShouldBeExecuted(action))
            {
                IndicatorsService.ChangeValue(hunger, -HungerPointsLostWhenSleeping);
                CharactersService.ResetLastStateChangeTime(Character);
                Logger.ConditionalDebug($"Character with id: {Character.Id} execute action AnalyseHungerIndicator at {action.LastExecutionTime}.");
            }
        }

        private void AnalyseThirstIndicator()
        {
            var thirst = Character.Indicators.FirstOrDefault(x => x.IndicatorTypeId == (int)IndicatorTypes.Thirst);
            var action = Character.SystemActions.FirstOrDefault(x => x.TypeId == (int)SystemActionTypes.SpanBeetwenThirstAnalyze);

            if (Character.State == (int)CharacterStates.Idle
                && CharactersService.EnoughTimeHasPassedFromPreviousStatusAnalyze(Character, SpanBeetwenThirstAnalyzeActions)
                && SystemActionsService.AcionShouldBeExecuted(action))
            {
                IndicatorsService.ChangeValue(thirst, -ThirstPointsLostWhenNotSleeping);
                CharactersService.ResetLastStateChangeTime(Character);
                Logger.ConditionalDebug($"Character with id: {Character.Id} execute action AnalyseThirstIndicator at {action.LastExecutionTime}.");
            }
            else if (Character.State == (int)CharacterStates.Sleep
                     && CharactersService.EnoughTimeHasPassedFromPreviousStatusAnalyze(Character, SpanBeetwenThirstAnalyzeActions)
                     && SystemActionsService.AcionShouldBeExecuted(action))
            {
                IndicatorsService.ChangeValue(thirst, -ThirstPointsLostWhenSleeping);
                CharactersService.ResetLastStateChangeTime(Character);
                Logger.ConditionalDebug($"Character with id: {Character.Id} execute action AnalyseThirstIndicator at {action.LastExecutionTime}.");
            }

        }

        #endregion Indicators Analyze

        #region Level Up

        private readonly long ExperienceForNextLevel = 32;

        public override bool ShouldLevelUp()
        {
            return Character.Experience >= ExperienceForNextLevel;
        }

        public override void LevelUp()
        {
            CharactersService.LevelUpCharacter(Character, Character.Experience - ExperienceForNextLevel);

            IndicatorsService.AddIndicator(Character.Id, IndicatorTypes.Entertainment);

            SystemActionsService.AddSystemAction(Character.Id, SystemActionTypes.SpanBeetwenEntertainmentAnalyze);

            Logger.ConditionalDebug($"Character with id: {Character.Id} level up form {Character.Level - 1} level to {Character.Level} at {CommonValues.ActaulaDateTime}.");
        }

        #endregion Level Up
    }
}
