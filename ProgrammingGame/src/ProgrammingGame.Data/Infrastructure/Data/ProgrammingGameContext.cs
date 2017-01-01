using Microsoft.EntityFrameworkCore;
using ProgrammingGame.Data.Entities;

namespace ProgrammingGame.Data.Infrastructure.Data
{
    public class ProgrammingGameContext : DbContext
    {
        public ProgrammingGameContext(DbContextOptions<ProgrammingGameContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Character> Characters { get; set; }
        public DbSet<Indicator> Indicators { get; set; }
        public DbSet<IndicatorType> IndicatorTypes { get; set; }
        public DbSet<ItemType> Item { get; set; }
        public DbSet<OwnedItem> OwnedItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Character>().HasMany(x => x.Indicators).WithOne(x => x.Character).HasForeignKey(x => x.CharacterId);
            modelBuilder.Entity<Character>().HasMany(x => x.OwnedItems).WithOne(x => x.Character).HasForeignKey(x => x.CharacterId);

            modelBuilder.Entity<Indicator>().HasKey(x => new { x.CharacterId, x.IndicatorTypeId });
            modelBuilder.Entity<OwnedItem>().HasKey(x => new { x.CharacterId, x.ItemTypeId });
        }
    }
}