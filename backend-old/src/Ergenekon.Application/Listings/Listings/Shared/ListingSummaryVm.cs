namespace Ergenekon.Application.Listings.Listings.Shared;

public class ListingSummaryVm
{
    public int Total { get; set; }

    public List<ListingSummaryItemVm> Items { get; set; }
}

public class ListingSummaryItemVm
{
    public int Id { get; set; }

    public string Slug { get; set; }

    public string Title { get; set; }

    public DateTime CreatedAt { get; set; }

    public string FeaturedImage { get; set; }
}