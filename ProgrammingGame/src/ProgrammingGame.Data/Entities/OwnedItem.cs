namespace ProgrammingGame.Data.Entities
{
    public class OwnedItem : IEntity
    {
        public int TypeId { get; set; }
        public ItemType Type { get; set; }

        public long CharacterId { get; set; }
        public Character Character { get; set; }

        public long Amount { get; set; }
    }
}
