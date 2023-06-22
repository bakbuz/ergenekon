namespace Ergenekon.Application.Common.Models;

public class LookupDto1<TKey> where TKey : struct
{
    public LookupDto1()
    {

    }

    public LookupDto1(TKey id, string name)
    {
        Id = id;
        Name = name;
    }

    public TKey Id { get; set; }

    public string? Name { get; set; }
}
