namespace ProgrammingGame.Web.Entities
{
    public class Indicator
    {
        public int IndicatorTypeId { get; set; }
        public IndicatorType IndicatorType { get; set; }

        public long CharacterId { get; set; }
        public Character Character { get; set; }

        public decimal Value { get; set; }
    }
}
