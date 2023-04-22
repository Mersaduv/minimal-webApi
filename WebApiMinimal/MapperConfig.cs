using AutoMapper;
using WebApiMinimal.Models;
using WebApiMinimal.Models.DTO;

namespace WebApiMinimal
{
    public class MapperConfig : Profile
    {
        public MapperConfig() {
            CreateMap<EventsList, EventCreateDTO>().ReverseMap();
            CreateMap<EventsList, EventDTO>().ReverseMap();
        }
        
    }
}
