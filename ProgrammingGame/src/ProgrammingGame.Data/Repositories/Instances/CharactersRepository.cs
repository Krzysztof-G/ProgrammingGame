using ProgrammingGame.Data.Repositories.Base;
using ProgrammingGame.Data.Repositories.Interfaces;
using System;
using System.Linq;
using ProgrammingGame.Data.Entities;

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
