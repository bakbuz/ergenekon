using AutoMapper;
using Ergenekon.Application.Catalog.Categories.Shared;
using Ergenekon.Application.Common.Interfaces;
using MediatR;

namespace Ergenekon.Application.Catalog.Categories.Queries.GetCategoriesByParentId;

public record GetCategoriesByParentIdQuery(int ParentId) : IRequest<CategoriesVm>
{
}

public class GetCategoriesByParentIdQueryHandler : IRequestHandler<GetCategoriesByParentIdQuery, CategoriesVm>
{
    private readonly ICategoryService _categoryService;
    private readonly IMapper _mapper;

    public GetCategoriesByParentIdQueryHandler(ICategoryService categoryService, IMapper mapper)
    {
        _categoryService = categoryService;
        _mapper = mapper;
    }

    public async Task<CategoriesVm> Handle(GetCategoriesByParentIdQuery request, CancellationToken cancellationToken)
    {
        var categories = await _categoryService.GetCategoriesByParentId(request.ParentId, cancellationToken);

        var vm = new CategoriesVm
        {
            Count = categories.Count,
            Categories = categories,
        };

        return vm;
    }
}