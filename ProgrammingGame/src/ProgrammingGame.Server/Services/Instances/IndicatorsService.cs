using ProgrammingGame.Common.Enums;
using ProgrammingGame.Data.Entities;
using ProgrammingGame.Data.Repositories.Interfaces;
using ProgrammingGame.Server.Services.Interfaces;

namespace ProgrammingGame.Server.Services.Instances
{
    public class IndicatorsService : IIndicatorsService
    {
        private readonly IIndicatorsRepository _indicatorsRepository;

        public IndicatorsService(IIndicatorsRepository indicatorsRepository)
        {
            _indicatorsRepository = indicatorsRepository;
        }


        public void AddIndicator(long characterId, IndicatorTypes indicatorType)
        {
            _indicatorsRepository.Add(new Indicator
            {
                CharacterId = characterId,
                IndicatorTypeId = (int)indicatorType
            });
            _indicatorsRepository.Save();
        }
    }
}