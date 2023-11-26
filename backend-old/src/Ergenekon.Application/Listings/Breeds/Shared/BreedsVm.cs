namespace Ergenekon.Application.Listings.Breeds.Shared;

public class BreedsVm
{
}

public class BreedVm
{
    public int Id { get; set; }
    public string Name { get; set; }
    public List<BreedItemVm> Children { get; set; }
}

public class BreedItemVm
{
    public BreedItemVm()
    {

    }
    public BreedItemVm(short id, string name)
    {
        Id = id;
        Name = name;
    }

    public int Id { get; set; }
    public string Name { get; set; }
}
