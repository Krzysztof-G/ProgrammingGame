using Microsoft.EntityFrameworkCore;
using ProgrammingGame.Data.Entities;
using ProgrammingGame.Data.Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace ProgrammingGame.Data.Infrastructure
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class, IEntity
    {
        private readonly IEntitiesContext _context;
        private readonly DbSet<TEntity> _dbEntitySet;
        private bool _disposed;

        public Repository(IEntitiesContext context)
        {
            _context = context;
            _dbEntitySet = _context.Set<TEntity>();
        }

        public TEntity GetSingle(Expression<Func<TEntity, bool>> predicate)
        {
            return _dbEntitySet.AsNoTracking().FirstOrDefault(predicate);
        }

        public TEntity GetSingleWithIncluding(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            return IncludeProperties(includeProperties).FirstOrDefault(predicate);
        }

        public IEnumerable<TEntity> FindBy(Expression<Func<TEntity, bool>> predicate)
        {
            return _dbEntitySet.AsNoTracking().Where(predicate);
        }

        public IEnumerable<TEntity> FindByWithIncluding(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            return IncludeProperties(includeProperties).Where(predicate);
        }

        public IEnumerable<TEntity> GetAllWithIncluding(params Expression<Func<TEntity, object>>[] includeProperties)
        {
            var entities = IncludeProperties(includeProperties);
            return entities;
        }

        public void Insert(TEntity entity)
        {
            _context.SetAsAdded<TEntity>(entity);
        }

        public void Update(TEntity entity)
        {
            _context.SetAsModified<TEntity>(entity);
        }

        public void Delete(TEntity entity)
        {
            _context.SetAsDeleted<TEntity>(entity);
        }

        private IQueryable<TEntity> IncludeProperties(params Expression<Func<TEntity, object>>[] includeProperties)
        {
            IQueryable<TEntity> entities = _dbEntitySet;
            foreach (var includeProperty in includeProperties)
            {
                entities = entities.Include(includeProperty);
            }
            return entities;
        }
    }
}