using System;
using Microsoft.Extensions.DependencyInjection;
using ProgrammingGame.Data.Services.Instances;
using ProgrammingGame.Data.Services.Interfaces;
using ProgrammingGame.Data.Infrastructure;
using ProgrammingGame.Data.Infrastructure.Context;

namespace ProgrammingGame.Server
{
    public static class DependencyInjection
    {
        public static IServiceProvider Provider { get; private set; }

        static DependencyInjection()
        {
            var serviceCollection = new ServiceCollection();
            Infrastructure(serviceCollection);
            RegisterServices(serviceCollection);
            Provider = serviceCollection.BuildServiceProvider();
        }

        private static void Infrastructure(ServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<IUnitOfWork, UnitOfWork>();
            serviceCollection.AddTransient<IEntitiesContext, ProgrammingGameContext>();
        }

        private static void RegisterServices(ServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<ICharactersService, CharactersService>();
            serviceCollection.AddSingleton<IIndicatorsService, IndicatorsService>();
            serviceCollection.AddSingleton<ISystemActionsService, SystemActionsService>();
        }
    }
}