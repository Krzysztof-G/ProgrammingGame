using System.Linq;
using ProgrammingGame.Common.Enums;
using ProgrammingGame.Data.Entities;
using ProgrammingGame.Data.Repositories.Interfaces;
using ProgrammingGame.Data.Services.Interfaces;

namespace ProgrammingGame.Data.Services.Instances
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
                IndicatorTypeId = (int)indicatorType,
                Value = _indicatorsRepository.Context.IndicatorTypes.FirstOrDefault(x => x.Id == (int)indicatorType)?.DefaultValue ?? 0
            });
            _indicatorsRepository.Save();
        }

        public void ChangeValue(Indicator indicator, int difference)
        {
            var indicatorType = indicator.IndicatorType;

            indicator.Value += difference;

            if (indicator.Value > indicatorType.MaxValue)
                indicator.Value = indicatorType.MaxValue;

            if (indicator.Value < indicatorType.MinValue)
                indicator.Value = indicatorType.MinValue;

            _indicatorsRepository.Edit(indicator);
            _indicatorsRepository.Save();
        }
    }
}