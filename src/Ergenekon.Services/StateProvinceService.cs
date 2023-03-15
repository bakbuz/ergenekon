using AutoMapper;
using Ergenekon.Infrastructure;
using Ergenekon.Services.DTOs;
using Microsoft.EntityFrameworkCore;

namespace Ergenekon.Services
{
    public interface IStateProvinceService
    {
        Task<List<StateProvinceDto>> GetStateProvincesAsync(int countryId);
    }

    public class StateProvinceService : IStateProvinceService
    {
        private readonly DataContext _ctx;
        private readonly IMapper _mappper;

        public StateProvinceService(DataContext ctx, IMapper mappper)
        {
            _ctx = ctx;
            _mappper = mappper;
        }

        public async Task<List<StateProvinceDto>> GetStateProvincesAsync(int countryId)
        {
            var stateProvinces = await _ctx.StateProvinces
                .Where(q => q.CountryId == countryId)
                .OrderBy(o => o.DisplayOrder)
                .ThenBy(o => o.Id)
                .ToListAsync();

            return _mappper.Map<List<StateProvinceDto>>(stateProvinces);
        }
    }
}
