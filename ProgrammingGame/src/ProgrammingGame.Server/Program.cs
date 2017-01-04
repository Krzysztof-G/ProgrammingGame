using Microsoft.Extensions.DependencyInjection;
using ProgrammingGame.Data.Services.Interfaces;

namespace ProgrammingGame.Server
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var mainServerFlow = new MainServerFlow(
                DependencyInjection.Provider.GetService<ICharactersService>(),
                DependencyInjection.Provider.GetService<IIndicatorsService>(),
                DependencyInjection.Provider.GetService<ISystemActionsService>());
            mainServerFlow.Run();
        }
    }
}
