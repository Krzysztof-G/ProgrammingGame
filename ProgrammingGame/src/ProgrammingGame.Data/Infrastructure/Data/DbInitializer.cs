using ProgrammingGame.Common.Enums;
using System;
using System.Linq;
using ProgrammingGame.Data.Entities;

namespace ProgrammingGame.Data.Infrastructure.Data
{
    public static class DbInitializer
    {
        public static void Initialize(ProgrammingGameContext context)
        {
            context.Database.EnsureCreated();

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
                    Name = IndicatorTypes.Fun.ToString(),
                    MinValue = 0,
                    MaxValue = 100,
                    DefaultValue = 50,
                });

                context.SaveChanges();
            }

            if (!context.SystemActionTypes.Any())
            {
                context.SystemActionTypes.Add(new SystemActionType
                {
                    Name = SystemActionTypes.SpanBeetwenEnergyAnalyse.ToString(),
                    DelayBeetwenExecuting = new TimeSpan(1, 0, 0)
                });

                context.SystemActionTypes.Add(new SystemActionType
                {
                    Name = SystemActionTypes.GainPointsForBeingRested.ToString(),
                    DelayBeetwenExecuting = new TimeSpan(1, 0, 0)
                });

                context.SystemActionTypes.Add(new SystemActionType
                {
                    Name = SystemActionTypes.LostPointsForBeingSleepy.ToString(),
                    DelayBeetwenExecuting = new TimeSpan(1, 0, 0)
                });

                context.SystemActionTypes.Add(new SystemActionType
                {
                    Name = SystemActionTypes.LostPointsForSleepToMuch.ToString(),
                    DelayBeetwenExecuting = new TimeSpan(1, 0, 0)
                });

                context.SystemActionTypes.Add(new SystemActionType
                {
                    Name = SystemActionTypes.LostPointsForBeingThirst.ToString(),
                    DelayBeetwenExecuting = new TimeSpan(1, 0, 0)
                });

                context.SystemActionTypes.Add(new SystemActionType
                {
                    Name = SystemActionTypes.LostPointsForBeingHungry.ToString(),
                    DelayBeetwenExecuting = new TimeSpan(1, 0, 0)
                });

                context.SaveChanges();
            }
        }
    }
}