using AutoMapper;
using WorldAPI.DTO.Country;
using WorldAPI.DTO.States;
using WorldAPI.Models;

namespace WorldAPI.CommonMapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Source, Destination
            CreateMap<Country, CreateCountryDto>().ReverseMap();
            CreateMap<Country, CountryDto>().ReverseMap();
            CreateMap<Country, UpdateCountryDto>().ReverseMap();
            CreateMap<States, CreateStatesDto>().ReverseMap();
            CreateMap<States, StatesDto>().ReverseMap();
            CreateMap<States, UpdateStatesDto>().ReverseMap();
        }
    }
}
