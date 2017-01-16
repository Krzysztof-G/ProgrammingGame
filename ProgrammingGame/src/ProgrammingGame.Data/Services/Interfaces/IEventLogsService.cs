using ProgrammingGame.Common.Enums;

namespace ProgrammingGame.Data.Services.Interfaces
{
    public interface IEventLogsService
    {
        void AddEventLog(long characterId, EventLogTypes type, string Header, string Content);
    }
}