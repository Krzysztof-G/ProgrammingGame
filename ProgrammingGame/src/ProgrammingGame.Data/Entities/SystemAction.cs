using System;

namespace ProgrammingGame.Data.Entities
{
    public class SystemAction
    {
        public long Id { get; set; }
        public DateTime LastExecutionTime { get; set; }

        public int TypeId { get; set; }
        public SystemActionType Type { get; set; }

        public long CharacterId { get; set; }
        public Character Character { get; set; }
    }
}
