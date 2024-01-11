using AutoMapper;
using WorldAPI.DTO.Country;
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
        }
    }
}
