namespace ProgrammingGame.Data.Entities
{
    public class ItemType : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public long Price { get; set; }
    }
}
