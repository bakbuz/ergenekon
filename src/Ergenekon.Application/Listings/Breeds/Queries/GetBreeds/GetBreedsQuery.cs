using Ergenekon.Application.Common.Interfaces;
using Ergenekon.Application.Listings.Breeds.Shared;
using MediatR;

namespace Ergenekon.Application.Listings.Breeds.Queries.GetBreeds;

public class GetBreedsQuery : IRequest<List<BreedVm>>
{
}

public class GetBreedsQueryHandler : IRequestHandler<GetBreedsQuery, List<BreedVm>>
{
    private readonly IBreedService _breedService;

    public GetBreedsQueryHandler(IBreedService breedService)
    {
        _breedService = breedService;
    }

    public async Task<List<BreedVm>> Handle(GetBreedsQuery request, CancellationToken cancellationToken)
    {
        var allBreeds = await _breedService.GetAllAsync(cancellationToken);

        var rootBreeds = allBreeds
            .Where(q => q.ParentId == 0)
            .OrderBy(o => o.DisplayOrder)
            .ThenBy(o => o.Name)
            .ToList();

        var vm = new List<BreedVm>();
        foreach (var root in rootBreeds)
        {
            var rootVm = new BreedVm
            {
                Id = root.Id,
                Name = root.Name,
            };
            rootVm.Children = allBreeds
                .Where(q => q.ParentId == root.Id)
                .OrderBy(o => o.Name)
                .Select(s => new BreedItemVm(s.Id, s.Name))
                .ToList();

            vm.Add(rootVm);
        }

        return vm;
    }
}