namespace ProgrammingGame.Common.Enums
{
    public enum SystemActionTypes
    {
        //Level 0
        SpanBeetwenEnergyAnalyze = 1,
        GainPointsForBeingRested = 2,
        LostPointsForBeingSleepy = 3,
        LostPointsForSleepToMuch = 4,
        //Level 1
        SpanBeetwenHungerAnalyze = 5,
        SpanBeetwenThirstAnalyze = 6,
        GainPointsForBeingRestedNotHungryAndNotThirsty = 7,
        LostPointsForBeingThirst = 8,
        LostPointsForBeingHungry = 9,
        //Level 2
        SpanBeetwenEntertainmentAnalyze = 10,
    }
}