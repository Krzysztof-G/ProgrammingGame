using ProgrammingGame.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;


namespace ProgrammingGame.Data.Infrastructure
{
    public interface IRepository<TEntity> where TEntity : class, IEntity
    {
        TEntity GetSingle(Expression<Func<TEntity, bool>> predicate);
        TEntity GetSingleWithIncluding(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includeProperties);
        IEnumerable<TEntity> FindBy(Expression<Func<TEntity, bool>> predicate);
        IEnumerable<TEntity> FindByWithIncluding(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includeProperties);
        IEnumerable<TEntity> GetAllWithIncluding(params Expression<Func<TEntity, object>>[] includeProperties);
        void Insert(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
    }
}