namespace ProgrammingGame.Data.Entities
{
    public class Indicator : IEntity
    {
        public int IndicatorTypeId { get; set; }
        public virtual IndicatorType IndicatorType { get; set; }

        public long CharacterId { get; set; }
        public virtual Character Character { get; set; }

        public decimal Value { get; set; }
    }
}
