using System;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using ProgrammingGame.Data.Infrastructure.Data;

namespace ProgrammingGame.Data.Repositories.Base
{
    public abstract class GenericRepository<T> : IGenericRepository<T>, IRepository
            where T : class
    {
        protected readonly ProgrammingGameContext Entities;

        public ProgrammingGameContext Context => Entities;

        protected GenericRepository()
        {
            this.Entities = ProgrammingGameContextFactory.Create();
        }

        protected GenericRepository(IRepository existingRepository)
        {
            Entities = (existingRepository as GenericRepository<T>).Context;
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
            this.Entities.Set<T>().Add(entity);
        }

        public virtual void Delete(T entity)
        {
            this.Entities.Set<T>().Remove(entity);
        }

        public virtual void Edit(T entity)
        {
            this.Entities.Entry<T>(entity).State = EntityState.Modified;
        }

        public virtual void Save()
        {
            this.Entities.SaveChanges();
        }
    }
}
