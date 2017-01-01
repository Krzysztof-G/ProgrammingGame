using ProgrammingGame.Common.Enums;

namespace ProgrammingGame.Server.Services.Interfaces
{
    public interface ISystemActionsService
    {
        void AddSystemAction(long characterId, SystemActionTypes systemActionType);
    }
}