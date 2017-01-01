using AutoMapper;
using ProgrammingGame.Api.Models;
using ProgrammingGame.Data.Entities;

namespace ProgrammingGame.Api.Core
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Character, CharacterDto>();
        }
    }
}
