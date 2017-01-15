using System;

namespace ProgrammingGame.Data.Entities
{
    public class SystemActionType : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public TimeSpan DelayBeetwenExecuting { get; set; }
    }
}
