using Microsoft.EntityFrameworkCore;
using ProgrammingGame.Data.Entities;
using System;

namespace ProgrammingGame.Data.Infrastructure.Context
{
    public interface IEntitiesContext : IDisposable
    {
        DbSet<TEntity> Set<TEntity>() where TEntity : class, IEntity;
        void SetAsAdded<TEntity>(TEntity entity) where TEntity : class, IEntity;
        void SetAsModified<TEntity>(TEntity entity) where TEntity : class, IEntity;
        void SetAsDeleted<TEntity>(TEntity entity) where TEntity : class, IEntity;
        int SaveChanges();
    }
}