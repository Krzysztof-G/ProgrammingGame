using AutoMapper;
using ProgrammingGame.Api.Models;
using ProgrammingGame.Data.Domain.Entities;

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
