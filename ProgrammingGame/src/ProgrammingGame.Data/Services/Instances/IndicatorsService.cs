using ProgrammingGame.Common.Enums;
using ProgrammingGame.Data.Entities;
using ProgrammingGame.Data.Services.Interfaces;
using ProgrammingGame.Data.Infrastructure;

namespace ProgrammingGame.Data.Services.Instances
{
    public class IndicatorsService : IIndicatorsService
    {
        private readonly IUnitOfWork _unitOfWork;

        public IndicatorsService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void AddIndicator(long characterId, IndicatorTypes indicatorType)
        {
            var indicatorsRepository = _unitOfWork.Repository<Indicator>();
            var indicatorsTypesRepository = _unitOfWork.Repository<IndicatorType>();

            indicatorsRepository.Insert(new Indicator
            {
                CharacterId = characterId,
                IndicatorTypeId = (int)indicatorType,
                Value = indicatorsTypesRepository.GetSingle(x => x.Id == (int)indicatorType).DefaultValue
            });
            _unitOfWork.SaveChanges();
        }

        public void ChangeValue(Indicator indicator, int difference)
        {
            var indicatorsRepository = _unitOfWork.Repository<Indicator>();
            var indicatorType = indicator.IndicatorType;

            indicator.Value += difference;

            if (indicator.Value > indicatorType.MaxValue)
                indicator.Value = indicatorType.MaxValue;

            if (indicator.Value < indicatorType.MinValue)
                indicator.Value = indicatorType.MinValue;

            indicatorsRepository.Update(indicator);
            _unitOfWork.SaveChanges();
        }
    }
}