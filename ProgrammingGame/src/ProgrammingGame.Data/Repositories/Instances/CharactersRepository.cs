using ProgrammingGame.Data.Entities;
using ProgrammingGame.Data.Repositories.Interfaces;
using System;
using System.Linq;

namespace ProgrammingGame.Data.Repositories.Instances
{
    public class CharactersRepository : GenericRepository<Character>, ICharactersRepository
    {
        public Character GetCharacterByKey(Guid characterKey)
        {
            return FindBy(c => c.Key == characterKey).FirstOrDefault();
        }
    }
}
