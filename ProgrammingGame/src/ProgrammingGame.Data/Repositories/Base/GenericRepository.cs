using System;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace ProgrammingGame.Data.Repositories.Base
{
    public abstract class GenericRepository<C, T> : IGenericRepository<T>, IRepository
            where C : DbContext
            where T : class
    {
        protected readonly C Entities;

        public C Context => Entities;

        protected GenericRepository()
        {
            Entities = Activator.CreateInstance<C>();
        }

        protected GenericRepository(IRepository existingRepository)
        {
            Entities = (existingRepository as GenericRepository<C, T>)?.Context;
        }
        
        public virtual IQueryable<T> GetAll()
        {
            return Entities.Set<T>();
        }

        public IQueryable<T> FindBy(Expression<Func<T, bool>> predicate)
        {
            return GetAll().Where<T>(predicate);
        }

        public virtual void Add(T entity)
        {
            Entities.Set<T>().Add(entity);
        }

        public virtual void Delete(T entity)
        {
            Entities.Set<T>().Remove(entity);
        }

        public virtual void Edit(T entity)
        {
            Entities.Entry<T>(entity).State = EntityState.Modified;
        }

        public virtual void Save()
        {
            Entities.SaveChanges();
        }
    }
}
