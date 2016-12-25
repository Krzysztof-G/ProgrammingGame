namespace ProgrammingGame.Data.Domain.Entities
{
    public class OwnedItem
    {
        public int ItemTypeId { get; set; }
        public ItemType ItemType { get; set; }

        public long CharacterId { get; set; }
        public Character Character { get; set; }

        public long Amount { get; set; }
    }
}
