using ProgrammingGame.Data.Entities;
using ProgrammingGame.Data.Repositories.Base;
using ProgrammingGame.Data.Repositories.Interfaces;

namespace ProgrammingGame.Data.Repositories.Instances
{
    public class CharactersRepository : GenericRepository<Character>, ICharactersRepository
    {
    }
}
