using ProgrammingGame.Common.Enums;

namespace ProgrammingGame.Server.Services.Interfaces
{
    public interface IIndicatorsService
    {
        void AddIndicator(long characterId, IndicatorTypes indicatorType);
    }
}