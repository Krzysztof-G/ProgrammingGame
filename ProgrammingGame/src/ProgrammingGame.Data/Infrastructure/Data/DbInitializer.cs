using Microsoft.EntityFrameworkCore;
using ProgrammingGame.Common.Enums;
using ProgrammingGame.Data.Entities;
using System;
using System.Linq;

namespace ProgrammingGame.Data.Infrastructure.Data
{
    public static class DbInitializer
    {
        public static void Initialize(DbContext context)
        {
            context.Database.EnsureCreated();

            IndicatorTypesInitialization(context);
            SystemActionTypesInitialization(context);
            EventLogTypesInitialization(context);
        }

        private static void IndicatorTypesInitialization(DbContext context)
        {
            if (context.Set<IndicatorType>().Any()) return;

            //Level 0
            context.Set<IndicatorType>().Add(new IndicatorType
            {
                Name = IndicatorTypes.Energy.ToString(),
                MinValue = 0,
                MaxValue = 100,
                DefaultValue = 100,
            });

            //Level 1
            context.Set<IndicatorType>().Add(new IndicatorType
            {
                Name = IndicatorTypes.Thirst.ToString(),
                MinValue = 0,
                MaxValue = 100,
                DefaultValue = 100,
            });

            context.Set<IndicatorType>().Add(new IndicatorType
            {
                Name = IndicatorTypes.Hunger.ToString(),
                MinValue = 0,
                MaxValue = 100,
                DefaultValue = 100,
            });

            //Level 2
            context.Set<IndicatorType>().Add(new IndicatorType
            {
                Name = IndicatorTypes.Entertainment.ToString(),
                MinValue = 0,
                MaxValue = 100,
                DefaultValue = 50,
            });

            context.SaveChanges();
        }
        private static void SystemActionTypesInitialization(DbContext context)
        {
            if (context.Set<SystemActionType>().Any()) return;

            //Level 0
            context.Set<SystemActionType>().Add(new SystemActionType
            {
                Name = SystemActionTypes.SpanBeetwenEnergyAnalyze.ToString(),
                DelayBeetwenExecuting = new TimeSpan(1, 0, 0)
            });

            context.Set<SystemActionType>().Add(new SystemActionType
            {
                Name = SystemActionTypes.GainPointsForBeingRested.ToString(),
                DelayBeetwenExecuting = new TimeSpan(1, 0, 0)
            });

            context.Set<SystemActionType>().Add(new SystemActionType
            {
                Name = SystemActionTypes.LostPointsForBeingSleepy.ToString(),
                DelayBeetwenExecuting = new TimeSpan(1, 0, 0)
            });

            context.Set<SystemActionType>().Add(new SystemActionType
            {
                Name = SystemActionTypes.LostPointsForSleepToMuch.ToString(),
                DelayBeetwenExecuting = new TimeSpan(1, 0, 0)
            });

            //Level 1
            context.Set<SystemActionType>().Add(new SystemActionType
            {
                Name = SystemActionTypes.SpanBeetwenHungerAnalyze.ToString(),
                DelayBeetwenExecuting = new TimeSpan(1, 0, 0)
            });

            context.Set<SystemActionType>().Add(new SystemActionType
            {
                Name = SystemActionTypes.SpanBeetwenThirstAnalyze.ToString(),
                DelayBeetwenExecuting = new TimeSpan(1, 0, 0)
            });

            context.Set<SystemActionType>().Add(new SystemActionType
            {
                Name = SystemActionTypes.GainPointsForBeingRestedNotHungryAndNotThirsty.ToString(),
                DelayBeetwenExecuting = new TimeSpan(1, 0, 0)
            });

            context.Set<SystemActionType>().Add(new SystemActionType
            {
                Name = SystemActionTypes.LostPointsForBeingThirst.ToString(),
                DelayBeetwenExecuting = new TimeSpan(1, 0, 0)
            });

            context.Set<SystemActionType>().Add(new SystemActionType
            {
                Name = SystemActionTypes.LostPointsForBeingHungry.ToString(),
                DelayBeetwenExecuting = new TimeSpan(1, 0, 0)
            });

            //Level 2
            context.Set<SystemActionType>().Add(new SystemActionType
            {
                Name = SystemActionTypes.SpanBeetwenEntertainmentAnalyze.ToString(),
                DelayBeetwenExecuting = new TimeSpan(1, 0, 0)
            });

            context.SaveChanges();
        }

        private static void EventLogTypesInitialization(DbContext context)
        {
            if (context.Set<EventLogType>().Any()) return;

            context.Set<EventLogType>().Add(new EventLogType
            {
                Name = EventLogTypes.GainExp.ToString()
            });

            context.Set<EventLogType>().Add(new EventLogType
            {
                Name = EventLogTypes.LostExp.ToString()
            });

            context.Set<EventLogType>().Add(new EventLogType
            {
                Name = EventLogTypes.LevelUp.ToString()
            });

            context.Set<EventLogType>().Add(new EventLogType
            {
                Name = EventLogTypes.IndicatorHasChanged.ToString()
            });

            context.SaveChanges();
        }
    }
}