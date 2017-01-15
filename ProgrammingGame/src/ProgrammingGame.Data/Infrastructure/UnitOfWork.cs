using ProgrammingGame.Data.Entities;
using ProgrammingGame.Data.Infrastructure.Context;
using System;
using System.Collections;

namespace ProgrammingGame.Data.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IEntitiesContext _context;
        private Hashtable _repositories;

        public UnitOfWork(IEntitiesContext context)
        {
            _context = context;
        }

        public int SaveChanges()
        {
            return _context.SaveChanges();
        }

        public IRepository<TEntity> Repository<TEntity>() where TEntity : class, IEntity
        {
            if (_repositories == null)
            {
                _repositories = new Hashtable();
            }
            var type = typeof(TEntity).Name;
            if (_repositories.ContainsKey(type))
            {
                return (IRepository<TEntity>)_repositories[type];
            }
            var repositoryType = typeof(Repository<>);
            _repositories.Add(type, Activator.CreateInstance(repositoryType.MakeGenericType(new Type[] { typeof(TEntity) }), _context));
            return (IRepository<TEntity>)_repositories[type];
        }
    }
}