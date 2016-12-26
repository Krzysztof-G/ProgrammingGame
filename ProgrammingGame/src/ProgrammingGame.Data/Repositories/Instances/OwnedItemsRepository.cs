using ProgrammingGame.Data.Domain.Entities;
using ProgrammingGame.Data.Infrastructure.Data;
using ProgrammingGame.Data.Repositories.Base;
using ProgrammingGame.Data.Repositories.Interfaces;

namespace ProgrammingGame.Data.Repositories.Instances
{
    public class OwnedItemsRepository : GenericRepository<OwnedItem>, IOwnedItemsRepository
    {
    }
}
