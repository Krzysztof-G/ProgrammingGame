using System;
using Microsoft.Extensions.DependencyInjection;
using ProgrammingGame.Data.Repositories.Instances;
using ProgrammingGame.Data.Repositories.Interfaces;
using ProgrammingGame.Data.Services.Instances;
using ProgrammingGame.Data.Services.Interfaces;

namespace ProgrammingGame.Server
{
    public static class DependencyInjection
    {
        public static IServiceProvider Provider { get; private set; }

        static DependencyInjection()
        {
            var serviceCollection = new ServiceCollection();
            RegisterRepositoriens(serviceCollection);
            RegisterServices(serviceCollection);
            Provider = serviceCollection.BuildServiceProvider();
        }

        private static void RegisterRepositoriens(ServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<ICharactersRepository, CharactersRepository>();
            serviceCollection.AddSingleton<IIndicatorsRepository, IndicatorsRepository>();
            serviceCollection.AddSingleton<IIndicatorTypesRepository, IndicatorTypesRepository>();
            serviceCollection.AddSingleton<IOwnedItemsRepository, OwnedItemsRepository>();
            serviceCollection.AddSingleton<ISystemActionsRepository, SystemActionsRepository>();
            serviceCollection.AddSingleton<ISystemActionTypesRepository, SystemActionTypesRepository>();
        }

        private static void RegisterServices(ServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<ICharactersService, CharactersService>();
            serviceCollection.AddSingleton<IIndicatorsService, IndicatorsService>();
            serviceCollection.AddSingleton<ISystemActionsService, SystemActionsService>();
        }
    }
}