using AutoMapper;
using app.Models;

namespace app.Map
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<ProgramInfo, ProgramListViewDto>();
            CreateMap<ProgramDto, ProgramInfo>();
            //         .ForMember(c => c.,
            //             opt => opt.MapFrom(x => string.Join(' ', x., x.)));
        }
    }
}