using AutoMapper;
using Ergenekon.Domain;
using Ergenekon.Services.DTOs;

namespace Ergenekon.Services
{
    internal class BusProfile : Profile
    {
        public BusProfile()
        {
            CreateMap<Country, CountryDto>();
            CreateMap<List<Country>, List<CountryDto>>();
            CreateMap<StateProvince, StateProvinceDto>();
            CreateMap<List<StateProvince>, List<StateProvinceDto>>();

        }
    }
}
