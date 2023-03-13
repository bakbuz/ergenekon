using AutoMapper;
using Ergenekon.Domain;
using Ergenekon.Infrastructure;
using Ergenekon.Services.DTOs;
using Microsoft.Extensions.Logging;
using System.Diagnostics.CodeAnalysis;

namespace Ergenekon.Services
{
    public interface ICountryService
    {
        List<Country> GetAllCountries();

        Task<List<CountryDto>> GetAllCountriesAsync();
    }

    public class CountryService : BaseOperationManager<Country, int>, ICountryService
    {
        private readonly IMapper _mappper;

        public CountryService([NotNull] DataContext context, IMapper mapper) : base(context)
        {
            _mappper = mapper;
        }

        public List<Country> GetAllCountries()
        {
            return base.GetAll();
        }

        public async Task<List<CountryDto>> GetAllCountriesAsync()
        {
            var countries = await base.GetAllAsync();
            return _mappper.Map<List<CountryDto>>(countries);
        }
    }
}