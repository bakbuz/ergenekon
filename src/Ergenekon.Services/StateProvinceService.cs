using Ergenekon.Domain;
using Ergenekon.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Ergenekon.Services
{
    public interface IStateProvinceService
    {
        Task<List<StateProvince>> GetStateProvincesAsync(int countryId);
    }

    public class StateProvinceService : IStateProvinceService
    {
        private readonly DataContext _ctx;

        public StateProvinceService(DataContext ctx)
        {
            _ctx = ctx;
        }

        public async Task<List<StateProvince>> GetStateProvincesAsync(int countryId)
        {
            return await _ctx.StateProvinces.Where(q => q.CountryId == countryId).ToListAsync();
        }
    }
}
