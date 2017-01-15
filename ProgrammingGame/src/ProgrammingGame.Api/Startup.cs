using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;
using ProgrammingGame.Api.Middleware;
using ProgrammingGame.Data.Infrastructure;
using ProgrammingGame.Data.Infrastructure.Context;
using ProgrammingGame.Data.Infrastructure.Data;
using ProgrammingGame.Data.Services.Instances;
using ProgrammingGame.Data.Services.Interfaces;

namespace ProgrammingGame.Api
{
    public class Startup
    {
        public IConfigurationRoot Configuration { get; }

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc()
                .AddMvcOptions(o => o.OutputFormatters.Add(new XmlDataContractSerializerOutputFormatter()));

            services.AddEntityFramework(Configuration.GetConnectionString("DefaultConnection"));
            services.AddAutoMapper();

            ConfigureInfrastructureInjection(services);
            ConfigureServicesInjection(services);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, ProgrammingGameContext context)
        {
            loggerFactory.AddConsole();
            loggerFactory.AddDebug();
            loggerFactory.AddNLog();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler();
            }

            app.UseStatusCodePages();
            app.UseMiddleware(typeof(ErrorHandlingMiddleware));
            app.UseMvc(ConfigureRoutes);

            DbInitializer.Initialize(context);
        }

        private void ConfigureInfrastructureInjection(IServiceCollection services)
        {
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddTransient<IEntitiesContext, ProgrammingGameContext>();
        }

        private void ConfigureServicesInjection(IServiceCollection services)
        {
            services.AddTransient<ICharactersService, CharactersService>();
            services.AddTransient<IIndicatorsService, IndicatorsService>();
            services.AddTransient<ISystemActionsService, SystemActionsService>();
        }

        private void ConfigureRoutes(IRouteBuilder routeBuilder)
        {
            routeBuilder.MapRoute("character", "api/{characterKey:guid}/{controller}/{action=Get}");
            routeBuilder.MapRoute("default", "api/{controller}/{action}");
        }
    }
}
