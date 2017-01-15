using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace ProgrammingGame.Data.Infrastructure.Context
{
    public class ProgrammingGameContextFactory : IDbContextFactory<ProgrammingGameContext>
    {
        private static readonly string connectionString = "Server=(localdb)\\mssqllocaldb;Database=ProgrammingGameDB;Trusted_Connection=True;MultipleActiveResultSets=true";

        public ProgrammingGameContext Create(DbContextFactoryOptions options)
        {
            var builder = new DbContextOptionsBuilder<ProgrammingGameContext>();
            builder.UseSqlServer(connectionString);
            return new ProgrammingGameContext(builder.Options);
        }

        public static ProgrammingGameContext Create()
        {   
            var builder = new DbContextOptionsBuilder<ProgrammingGameContext>();
            builder.UseSqlServer(connectionString);
            return new ProgrammingGameContext(builder.Options);
        }
    }
}
