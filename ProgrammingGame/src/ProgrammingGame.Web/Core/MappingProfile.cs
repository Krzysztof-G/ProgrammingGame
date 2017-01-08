using AutoMapper;
using ProgrammingGame.Common.Enums;
using ProgrammingGame.Data.Entities;
using ProgrammingGame.Web.ViewModels;

namespace ProgrammingGame.Web.Core
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Character, CharacterViewModel>()
                .ForMember(d => d.State, o => o.MapFrom(s => (CharacterStates)s.State));

            CreateMap<Indicator, IndicatorViewModel>()
                .ForMember(d => d.MaxValue, o => o.MapFrom(s => s.IndicatorType.MaxValue))
                .ForMember(d => d.MinValue, o => o.MapFrom(s => s.IndicatorType.MinValue))
                .ForMember(d => d.Name, o => o.MapFrom(s => s.IndicatorType.Name));

            CreateMap<OwnedItem, OwnedItemViewModel>()
                .ForMember(d => d.Name, o => o.MapFrom(s => s.ItemType.Name))
                .ForMember(d => d.Price, o => o.MapFrom(s => s.ItemType.Price));
        }
    }
}
