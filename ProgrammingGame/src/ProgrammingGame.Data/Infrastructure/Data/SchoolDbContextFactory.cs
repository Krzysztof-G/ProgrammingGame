using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace ProgrammingGame.Data.Infrastructure.Data
{
    public class SchoolDbContextFactory : IDbContextFactory<ProgrammingGameContext>
    {
        public ProgrammingGameContext Create(DbContextFactoryOptions options)
        {
            var builder = new DbContextOptionsBuilder<ProgrammingGameContext>();
            builder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=ProgrammingGameDB;Trusted_Connection=True;MultipleActiveResultSets=true");
            return new ProgrammingGameContext(builder.Options);
        }
    }
}
