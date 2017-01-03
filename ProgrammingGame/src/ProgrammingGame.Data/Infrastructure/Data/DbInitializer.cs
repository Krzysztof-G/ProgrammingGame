using ProgrammingGame.Common.Enums;
using ProgrammingGame.Data.Entities;
using System;
using System.Linq;

namespace ProgrammingGame.Data.Infrastructure.Data
{
    public static class DbInitializer
    {
        public static void Initialize(ProgrammingGameContext context)
        {
            context.Database.EnsureCreated();

            IndicatorTypesInitialization(context);
            SystemActionTypesInitialization(context);
        }

        private static void IndicatorTypesInitialization(ProgrammingGameContext context)
        {
            if (!context.IndicatorTypes.Any())
            {
                context.IndicatorTypes.Add(new IndicatorType
                {
                    Name = IndicatorTypes.Energy.ToString(),
                    MinValue = 0,
                    MaxValue = 100,
                    DefaultValue = 100,
                });

                context.IndicatorTypes.Add(new IndicatorType
                {
                    Name = IndicatorTypes.Thirst.ToString(),
                    MinValue = 0,
                    MaxValue = 100,
                    DefaultValue = 100,
                });

                context.IndicatorTypes.Add(new IndicatorType
                {
                    Name = IndicatorTypes.Hunger.ToString(),
                    MinValue = 0,
                    MaxValue = 100,
                    DefaultValue = 100,
                });

                context.IndicatorTypes.Add(new IndicatorType
                {
                    Name = IndicatorTypes.Entertainment.ToString(),
                    MinValue = 0,
                    MaxValue = 100,
                    DefaultValue = 50,
                });

                context.SaveChanges();
            }
        }
        private static void SystemActionTypesInitialization(ProgrammingGameContext context)
        {
            if (!context.SystemActionTypes.Any())
            {
                //Level 0
                context.SystemActionTypes.Add(new SystemActionType
                {
                    Id = (int)SystemActionTypes.SpanBeetwenEnergyAnalyze,
                    Name = SystemActionTypes.SpanBeetwenEnergyAnalyze.ToString(),
                    DelayBeetwenExecuting = new TimeSpan(1, 0, 0)
                });

                context.SystemActionTypes.Add(new SystemActionType
                {
                    Id = (int)SystemActionTypes.GainPointsForBeingRested,
                    Name = SystemActionTypes.GainPointsForBeingRested.ToString(),
                    DelayBeetwenExecuting = new TimeSpan(1, 0, 0)
                });

                context.SystemActionTypes.Add(new SystemActionType
                {
                    Id = (int)SystemActionTypes.LostPointsForBeingSleepy,
                    Name = SystemActionTypes.LostPointsForBeingSleepy.ToString(),
                    DelayBeetwenExecuting = new TimeSpan(1, 0, 0)
                });

                context.SystemActionTypes.Add(new SystemActionType
                {
                    Id = (int)SystemActionTypes.LostPointsForSleepToMuch,
                    Name = SystemActionTypes.LostPointsForSleepToMuch.ToString(),
                    DelayBeetwenExecuting = new TimeSpan(1, 0, 0)
                });

                //Level 1
                context.SystemActionTypes.Add(new SystemActionType
                {
                    Id = (int)SystemActionTypes.SpanBeetwenHungerAnalyze,
                    Name = SystemActionTypes.SpanBeetwenHungerAnalyze.ToString(),
                    DelayBeetwenExecuting = new TimeSpan(1, 0, 0)
                });

                context.SystemActionTypes.Add(new SystemActionType
                {
                    Id = (int)SystemActionTypes.SpanBeetwenThirstAnalyze,
                    Name = SystemActionTypes.SpanBeetwenThirstAnalyze.ToString(),
                    DelayBeetwenExecuting = new TimeSpan(1, 0, 0)
                });

                context.SystemActionTypes.Add(new SystemActionType
                {
                    Id = (int)SystemActionTypes.LostPointsForBeingHungry,
                    Name = SystemActionTypes.LostPointsForBeingHungry.ToString(),
                    DelayBeetwenExecuting = new TimeSpan(1, 0, 0)
                });

                context.SystemActionTypes.Add(new SystemActionType
                {
                    Id = (int)SystemActionTypes.GainPointsForBeingRestedNotHungryAndNotThirsty,
                    Name = SystemActionTypes.GainPointsForBeingRestedNotHungryAndNotThirsty.ToString(),
                    DelayBeetwenExecuting = new TimeSpan(1, 0, 0)
                });

                context.SystemActionTypes.Add(new SystemActionType
                {
                    Id = (int)SystemActionTypes.LostPointsForBeingThirst,
                    Name = SystemActionTypes.LostPointsForBeingThirst.ToString(),
                    DelayBeetwenExecuting = new TimeSpan(1, 0, 0)
                });

                //Level 2
                context.SystemActionTypes.Add(new SystemActionType
                {
                    Id = (int)SystemActionTypes.SpanBeetwenEntertainmentAnalyze,
                    Name = SystemActionTypes.SpanBeetwenEntertainmentAnalyze.ToString(),
                    DelayBeetwenExecuting = new TimeSpan(1, 0, 0)
                });

                context.SaveChanges();
            }
        }
    }
}