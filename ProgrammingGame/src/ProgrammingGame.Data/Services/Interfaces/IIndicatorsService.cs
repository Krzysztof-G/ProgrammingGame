using ProgrammingGame.Common.Enums;
using ProgrammingGame.Data.Entities;

namespace ProgrammingGame.Data.Services.Interfaces
{
    public interface IIndicatorsService
    {
        void AddIndicator(long characterId, IndicatorTypes indicatorType);
        void ChangeValue(Indicator indicator, int difference);
    }
}