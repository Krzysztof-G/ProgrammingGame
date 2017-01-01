using ProgrammingGame.Common.Enums;
using ProgrammingGame.Data.Entities;

namespace ProgrammingGame.Server.Services.Interfaces
{
    public interface ISystemActionsService
    {
        void AddSystemAction(long characterId, SystemActionTypes systemActionType);
        void ResetLastExecutionTime(SystemAction action);
    }
}