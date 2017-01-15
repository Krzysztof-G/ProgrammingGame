namespace ProgrammingGame.Data.Entities
{
    public class OwnedItem : IEntity
    {
        public int ItemTypeId { get; set; }
        public ItemType ItemType { get; set; }

        public long CharacterId { get; set; }
        public Character Character { get; set; }

        public long Amount { get; set; }
    }
}
