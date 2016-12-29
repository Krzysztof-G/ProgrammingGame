using System;
using ProgrammingGame.Data.Domain.Entities;
using ProgrammingGame.Data.Repositories.Base;

namespace ProgrammingGame.Data.Repositories.Interfaces
{
    public interface ICharactersRepository : IGenericRepository<Character>
    {
        Character GetCharacterByKey(Guid characterKey);
    }
}
