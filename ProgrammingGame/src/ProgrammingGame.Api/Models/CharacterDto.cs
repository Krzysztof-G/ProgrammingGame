using ProgrammingGame.Common.Enums;

namespace ProgrammingGame.Api.Models
{
    public class CharacterDto
    {
        public string Name { get; set; }
        public CharacterStates State { get; set; }
        public int Level { get; set; }
        public long Experience { get; set; }
        public long Cash { get; set; }
    }
}
