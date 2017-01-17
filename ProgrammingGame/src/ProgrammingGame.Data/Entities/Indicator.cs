namespace ProgrammingGame.Data.Entities
{
    public class Indicator : IEntity
    {
        public int TypeId { get; set; }
        public IndicatorType Type { get; set; }

        public long CharacterId { get; set; }
        public Character Character { get; set; }

        public decimal Value { get; set; }
    }
}
