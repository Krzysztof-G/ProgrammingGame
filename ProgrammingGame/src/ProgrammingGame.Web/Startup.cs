using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ProgrammingGame.Data.Entities;
using ProgrammingGame.Data.Infrastructure.Data;
using ProgrammingGame.Data.Repositories.Instances;
using ProgrammingGame.Data.Repositories.Interfaces;
using ProgrammingGame.Data.Services.Instances;
using ProgrammingGame.Data.Services.Interfaces;
using ProgrammingGame.Web.Services;

namespace ProgrammingGame.Web
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

            if (env.IsDevelopment())
            {
                builder.AddUserSecrets();
            }

            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ProgrammingGameContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<User, IdentityRole<long>>()
                .AddEntityFrameworkStores<ProgrammingGameContext, long>()
                .AddDefaultTokenProviders();

            services.AddMvc();

            services.AddTransient<IEmailSender, EmailService>();

            services.AddAutoMapper();
            ConfigureRepositoriensInjection(services);
            ConfigureServicesInjection(services);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, ProgrammingGameContext context)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseIdentity();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            DbInitializer.Initialize(context);
        }

        private void ConfigureRepositoriensInjection(IServiceCollection services)
        {
            services.AddTransient<IUsersRepository, UsersRepository>();
            services.AddTransient<ICharactersRepository, CharactersRepository>();
            services.AddTransient<IIndicatorsRepository, IndicatorsRepository>();
            services.AddTransient<IIndicatorTypesRepository, IndicatorTypesRepository>();
            services.AddTransient<IOwnedItemsRepository, OwnedItemsRepository>();
            services.AddTransient<ISystemActionsRepository, SystemActionsRepository>();
            services.AddTransient<ISystemActionTypesRepository, SystemActionTypesRepository>();
        }

        private void ConfigureServicesInjection(IServiceCollection services)
        {
            services.AddTransient<ICharactersService, CharactersService>();
            services.AddTransient<IIndicatorsService, IndicatorsService>();
            services.AddTransient<ISystemActionsService, SystemActionsService>();
        }
    }
}
