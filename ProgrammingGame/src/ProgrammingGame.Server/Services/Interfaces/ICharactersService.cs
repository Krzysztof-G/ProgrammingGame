using ProgrammingGame.Data.Entities;

namespace ProgrammingGame.Server.Services.Interfaces
{
    public interface ICharactersService
    {
        void AddExperienceToCharacter(Character character, long experienceToAdd);
        void LevelUpCharacter(Character character, long pointsSurplus);
    }
}