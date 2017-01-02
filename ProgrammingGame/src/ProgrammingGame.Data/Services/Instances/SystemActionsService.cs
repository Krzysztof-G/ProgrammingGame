using ProgrammingGame.Common;
using ProgrammingGame.Common.Enums;
using ProgrammingGame.Data.Entities;
using ProgrammingGame.Data.Repositories.Interfaces;
using ProgrammingGame.Data.Services.Interfaces;

namespace ProgrammingGame.Data.Services.Instances
{
    public class SystemActionsService : ISystemActionsService
    {
        private readonly ISystemActionsRepository _systemActionsRepository;

        public SystemActionsService(ISystemActionsRepository systemActionsRepository)
        {
            _systemActionsRepository = systemActionsRepository;
        }

        public void AddSystemAction(long characterId, SystemActionTypes systemActionType)
        {
            _systemActionsRepository.Add(new SystemAction
            {
                CharacterId = characterId,
                TypeId = (int)systemActionType,
                LastExecutionTime = Common.CommonValues.ActaulaDateTime
            });
            _systemActionsRepository.Save();
        }

        public void ResetLastExecutionTime(SystemAction action)
        {
            action.LastExecutionTime = Common.CommonValues.ActaulaDateTime;
            _systemActionsRepository.Edit(action);
            _systemActionsRepository.Save();
        }

        public bool AcionShouldBeExecuted(SystemAction action)
        {
            return action.LastExecutionTime.Add(action.Type.DelayBeetwenExecuting) <= CommonValues.ActaulaDateTime;
        }
    }
}