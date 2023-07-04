using AutoMapper;
using Ergenekon.Application.Catalog.Categories.Shared;
using Ergenekon.Application.Common.Interfaces;
using MediatR;

namespace Ergenekon.Application.Catalog.Categories.Queries.GetCategories;

public record GetCategoriesQuery : IRequest<CategoriesVm>
{
}

public class GetCategoriesQueryHandler : IRequestHandler<GetCategoriesQuery, CategoriesVm>
{
    private readonly ICategoryService _categoryService;
    private readonly IMapper _mapper;

    public GetCategoriesQueryHandler(ICategoryService categoryService, IMapper mapper)
    {
        _categoryService = categoryService;
        _mapper = mapper;
    }

    public async Task<CategoriesVm> Handle(GetCategoriesQuery request, CancellationToken cancellationToken)
    {
        var categories = await _categoryService.GetAllAsync(cancellationToken);

        var vm = new CategoriesVm
        {
            Count = categories.Count,
            Categories = categories,
        };

        return vm;
    }
}