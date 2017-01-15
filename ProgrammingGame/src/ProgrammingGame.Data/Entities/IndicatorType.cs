namespace ProgrammingGame.Data.Entities
{
    public class IndicatorType : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal MinValue { get; set; }
        public decimal MaxValue { get; set; }
        public decimal DefaultValue { get; set; }
    }
}
