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
                Value = _indicatorsRepository.FindBy(x => x.IndicatorTypeId == (int)indicatorType).FirstOrDefault()?.IndicatorType?.DefaultValue ?? 0
            });
            _indicatorsRepository.Save();
        }

        public void ChangeValue(Indicator indicator, int difference)
        {
            var indicatorTye = indicator.IndicatorType;

            indicator.Value += difference;

            if (indicator.Value > indicatorTye.MaxValue)
                indicator.Value = indicatorTye.MaxValue;

            if (indicator.Value < indicatorTye.MinValue)
                indicator.Value = indicatorTye.MinValue;

            _indicatorsRepository.Edit(indicator);
            _indicatorsRepository.Save();
        }
    }
}