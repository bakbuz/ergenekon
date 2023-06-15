using AutoMapper;
using Ergenekon.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Ergenekon.Application.Catalog.Categories.Queries.GetCategories;

public class GetCategoriesQuery : IRequest<CategoriesVm>
{
}

public class GetCategoriesQueryHandler : IRequestHandler<GetCategoriesQuery, CategoriesVm>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetCategoriesQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<CategoriesVm> Handle(GetCategoriesQuery request, CancellationToken cancellationToken)
    {
        var categories = await _context.Categories.ToListAsync(cancellationToken);

        var vm = new CategoriesVm
        {
            Count = categories.Count,
            Categories = categories,
        };

        return vm;
    }
}