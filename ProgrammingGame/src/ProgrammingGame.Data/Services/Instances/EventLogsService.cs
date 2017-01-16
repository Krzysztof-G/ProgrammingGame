using ProgrammingGame.Common;
using ProgrammingGame.Common.Enums;
using ProgrammingGame.Data.Entities;
using ProgrammingGame.Data.Infrastructure;
using ProgrammingGame.Data.Services.Interfaces;

namespace ProgrammingGame.Data.Services.Instances
{
    public class EventLogsService : IEventLogsService
    {
        private readonly IUnitOfWork _unitOfWork;

        public EventLogsService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void AddEventLog(long characterId, EventLogTypes type, string Header, string Content)
        {
            var eventLogsRepository = _unitOfWork.Repository<EventLog>();

            var eventLogToAdd = new EventLog
            {
                Header = Header,
                Content = Content,
                OccurrenceTime = CommonValues.ActaulaDateTime,
                CharacterId = characterId,
                TypeId = (int)type,
            };

            eventLogsRepository.Insert(eventLogToAdd);
            _unitOfWork.SaveChanges();
        }
    }
}