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
            CreateMap<BookTicketDto, BookTicket>();
            CreateMap<LocationDto, Location>();
            CreateMap<ArtistDto, Artist>();
            CreateMap<NewsDto, News>();
            CreateMap<TicketDto, Ticket>();
            CreateMap<Ticket, TicketDetailDto>();
            // CreateMap<BookTicket, BookTicketDto>()
            //         .ForMember(c => c.FullName,
            //             opt => opt.MapFrom(x => string.Join(' ', x.LastName, x.FisrtName)));
        }
    }
}