﻿using Ergenekon.Application.Common.Models;
using Ergenekon.Application.Territory.Services;
using MediatR;

namespace Ergenekon.Application.Territory.Queries.GetGetDistrictsByProvinceId;

public record GetDistrictsByProvinceIdQuery(ushort ProvinceId) : IRequest<List<LookupDto1<ushort>>>;

public class GetGetDistrictsByProvinceIdQueryHandler : IRequestHandler<GetDistrictsByProvinceIdQuery, List<LookupDto1<ushort>>>
{
    private readonly ITerritoryService _territoryService;

    public GetGetDistrictsByProvinceIdQueryHandler(ITerritoryService territoryService)
    {
        _territoryService = territoryService;
    }

    public async Task<List<LookupDto1<ushort>>> Handle(GetDistrictsByProvinceIdQuery request, CancellationToken cancellationToken)
    {
        return await _territoryService.GetDistrictsAsync(request.ProvinceId, cancellationToken);
    }
}

