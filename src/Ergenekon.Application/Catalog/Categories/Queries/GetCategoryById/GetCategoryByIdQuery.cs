using AutoMapper;
using Ergenekon.Application.Common.Interfaces;
using Ergenekon.Domain.Entities.Catalog;
using MediatR;

namespace Ergenekon.Application.Catalog.Categories.Queries.GetCategoryById;

public record GetCategoryByIdQuery(int Id) : IRequest<Category>
{
}

public class GetCategoryByIdQueryHandler : IRequestHandler<GetCategoryByIdQuery, Category>
{
    private readonly ICategoryService _categoryService;
    private readonly IMapper _mapper;

    public GetCategoryByIdQueryHandler(ICategoryService categoryService, IMapper mapper)
    {
        _categoryService = categoryService;
        _mapper = mapper;
    }

    public async Task<Category> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
    {
        var category = await _categoryService.GetByIdAsync(request.Id, cancellationToken);

        return category;
    }
}