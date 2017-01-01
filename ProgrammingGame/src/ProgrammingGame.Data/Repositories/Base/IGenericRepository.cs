using System;
using System.Linq;
using System.Linq.Expressions;
using ProgrammingGame.Data.Infrastructure.Data;

namespace ProgrammingGame.Data.Repositories.Base
{
    public interface IGenericRepository<T> : IRepository 
        where T : class
    {
        ProgrammingGameContext Context { get; }

        void Add(T entity);
        void Delete(T entity);
        void Edit(T entity);
        IQueryable<T> FindBy(Expression<Func<T, bool>> predicate);
        IQueryable<T> GetAll();
        void Save();
    }
}
