namespace ProgrammingGame.Web.Entities
{
    public class IndicatorType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal MinValue { get; set; }
        public decimal MaxValue { get; set; }
        public decimal DefaultValue { get; set; }
    }
}
