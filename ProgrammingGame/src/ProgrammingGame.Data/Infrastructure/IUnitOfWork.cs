using ProgrammingGame.Data.Entities;

namespace ProgrammingGame.Data.Infrastructure
{
    public interface IUnitOfWork
    {
        int SaveChanges();
        IRepository<TEntity> Repository<TEntity>() where TEntity : class, IEntity;       
    }
}