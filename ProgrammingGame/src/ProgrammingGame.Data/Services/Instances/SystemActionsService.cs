using ProgrammingGame.Common;
using ProgrammingGame.Common.Enums;
using ProgrammingGame.Data.Entities;
using ProgrammingGame.Data.Infrastructure;
using ProgrammingGame.Data.Services.Interfaces;

namespace ProgrammingGame.Data.Services.Instances
{
    public class SystemActionsService : ISystemActionsService
    {
        private readonly IUnitOfWork _unitOfWork;

        public SystemActionsService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void AddSystemAction(long characterId, SystemActionTypes systemActionType)
        {
            var systemActionsRepository = _unitOfWork.Repository<SystemAction>();
            systemActionsRepository.Insert(new SystemAction
            {
                CharacterId = characterId,
                TypeId = (int)systemActionType,
                LastExecutionTime = CommonValues.ActaulaDateTime
            });
            _unitOfWork.SaveChanges();
        }

        public void ResetLastExecutionTime(SystemAction action)
        {
            var systemActionsRepository = _unitOfWork.Repository<SystemAction>();
            action.LastExecutionTime = CommonValues.ActaulaDateTime;
            systemActionsRepository.Update(action);
            _unitOfWork.SaveChanges();
        }

        public bool AcionShouldBeExecuted(SystemAction action)
        {
            return action.LastExecutionTime.Add(action.Type.DelayBeetwenExecuting) <= CommonValues.ActaulaDateTime;
        }
    }
}