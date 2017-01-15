using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ProgrammingGame.Data.Infrastructure.Context
{
    public static class ServiceCollectionExtensions
    {
        public static void AddEntityFramework(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<ProgrammingGameContext>(options =>
                    options.UseSqlServer(connectionString));
        }
    }
}
