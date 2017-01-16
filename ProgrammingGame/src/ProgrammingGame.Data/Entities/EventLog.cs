using System;

namespace ProgrammingGame.Data.Entities
{
    public class EventLog : IEntity
    {
        public long Id { get; set; }
        public string Header { get; set; }
        public string Content { get; set; }
        public DateTime OccurrenceTime { get; set; }

        public int TypeId { get; set; }
        public EventLogType Type { get; set; }

        public long CharacterId { get; set; }
        public Character Character { get; set; }
    }
}
