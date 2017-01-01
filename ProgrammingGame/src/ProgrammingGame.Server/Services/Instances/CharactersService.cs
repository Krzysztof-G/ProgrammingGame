using ProgrammingGame.Data.Entities;
using ProgrammingGame.Data.Repositories.Interfaces;
using ProgrammingGame.Server.Services.Interfaces;

namespace ProgrammingGame.Server.Services.Instances
{
    public class CharactersService : ICharactersService
    {
        private readonly ICharactersRepository _charactersRepository;

        public CharactersService(ICharactersRepository charactersRepository)
        {
            _charactersRepository = charactersRepository;
        }

        public void AddExperienceToCharacter(Character character, long experienceToAdd)
        {
            character.Experience += experienceToAdd;
            _charactersRepository.Edit(character);
            _charactersRepository.Save();
        }

        public void LevelUpCharacter(Character character, long pointsSurplus)
        {
            character.Level++;
            character.Experience = pointsSurplus;
            _charactersRepository.Edit(character);
            _charactersRepository.Save();
        }
    }
}