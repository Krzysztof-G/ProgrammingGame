using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ProgrammingGame.Data.Entities;

namespace ProgrammingGame.Data.Infrastructure.Data
{
    public class ProgrammingGameContext : IdentityDbContext<User, IdentityRole<long>, long>
    {
        public ProgrammingGameContext(DbContextOptions<ProgrammingGameContext> options) : base(options)
        {
        }

        public DbSet<Character> Characters { get; set; }
        public DbSet<Indicator> Indicators { get; set; }
        public DbSet<IndicatorType> IndicatorTypes { get; set; }
        public DbSet<ItemType> Item { get; set; }
        public DbSet<OwnedItem> OwnedItems { get; set; }
        public DbSet<SystemAction> SystemActions { get; set; }
        public DbSet<SystemActionType> SystemActionTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>().HasOne(x => x.Character).WithOne(x => x.User).IsRequired(false).HasForeignKey<User>(x => x.CharacterId);

            modelBuilder.Entity<Character>().HasMany(x => x.Indicators).WithOne(x => x.Character).HasForeignKey(x => x.CharacterId);
            modelBuilder.Entity<Character>().HasMany(x => x.OwnedItems).WithOne(x => x.Character).HasForeignKey(x => x.CharacterId);

            modelBuilder.Entity<Indicator>().HasKey(x => new { x.CharacterId, x.IndicatorTypeId });

            modelBuilder.Entity<OwnedItem>().HasKey(x => new { x.CharacterId, x.ItemTypeId });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("Users", "security");

            });

            modelBuilder.Entity<IdentityRole<long>>(entity =>
            {
                entity.ToTable("Roles", "security");
            });

            modelBuilder.Entity<IdentityUserClaim<long>>(entity =>
            {
                entity.ToTable("UserClaims", "security");
                entity.Property(e => e.UserId).HasColumnName("UserId");

            });

            modelBuilder.Entity<IdentityUserLogin<long>>(entity =>
            {
                entity.ToTable("UserLogins", "security");
            });

            modelBuilder.Entity<IdentityRoleClaim<long>>(entity =>
            {
                entity.ToTable("RoleClaims", "security");
                entity.Property(e => e.RoleId).HasColumnName("RoleId");
            });

            modelBuilder.Entity<IdentityUserRole<long>>(entity =>
            {
                entity.ToTable("UserRoles", "security");
                entity.Property(e => e.UserId).HasColumnName("UserId");
                entity.Property(e => e.RoleId).HasColumnName("RoleId");
            });

            modelBuilder.Entity<IdentityUserToken<long>>(entity =>
            {
                entity.ToTable("UserTokens", "security");
                entity.Property(e => e.UserId).HasColumnName("UserId");

            });
        }
    }
}