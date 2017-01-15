using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ProgrammingGame.Data.Entities;

namespace ProgrammingGame.Data.Infrastructure.Context
{
    public class ProgrammingGameContext : IdentityDbContext<User, IdentityRole<long>, long>, IEntitiesContext
    {
        public ProgrammingGameContext(DbContextOptions<ProgrammingGameContext> options) : base(options)
        {
        }
        
        public DbSet<TEntity> Set<TEntity>() where TEntity : class, IEntity
        {
            return base.Set<TEntity>();
        }

        public void SetAsAdded<TEntity>(TEntity entity) where TEntity : class, IEntity
        {
            UpdateEntityState<TEntity>(entity, EntityState.Added);
        }

        public void SetAsModified<TEntity>(TEntity entity) where TEntity : class, IEntity
        {
            UpdateEntityState<TEntity>(entity, EntityState.Modified);
        }

        public void SetAsDeleted<TEntity>(TEntity entity) where TEntity : class, IEntity
        {
            UpdateEntityState<TEntity>(entity, EntityState.Deleted);
        }

        private void UpdateEntityState<TEntity>(TEntity entity, EntityState entityState) where TEntity : class, IEntity
        {
            var dbEntityEntry = Entry<TEntity>(entity);
            dbEntityEntry.State = entityState;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>().HasOne(x => x.Character).WithOne(x => x.User).IsRequired(false).HasForeignKey<User>(x => x.CharacterId);
            modelBuilder.Entity<User>().HasIndex(x => x.CharacterId).IsUnique(false);

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