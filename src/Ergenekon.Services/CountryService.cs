using Ergenekon.Domain;
using Ergenekon.Infrastructure;
using Microsoft.Extensions.Logging;
using System.Diagnostics.CodeAnalysis;

namespace Ergenekon.Services
{
    public interface ICountryService
    {
        List<Country> GetAllCountries();

        Task<List<Country>> GetAllCountriesAsync();
    }

    public class CountryService : BaseOperationManager<Country, int>, ICountryService
    {
        public CountryService([NotNull] DataContext context, ILogger logger) : base(context, logger)
        {
        }

        public List<Country> GetAllCountries()
        {
            return base.GetAll();
        }

        public async Task<List<Country>> GetAllCountriesAsync()
        {
            return await base.GetAllAsync();
        }
    }
}