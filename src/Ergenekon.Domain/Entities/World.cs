using System.Text.Json.Serialization;

namespace Ergenekon.Domain.Entities;

public class Country : BaseEntity<ushort>
{
    public Country()
    {
        Provinces = new HashSet<Province>();
    }

    public string Name { get; set; } = null!;

    public string EnglishName { get; set; } = null!;

    public string Iso2Code { get; set; } = null!;

    public string Iso3Code { get; set; } = null!;

    public short NumericIsoCode { get; set; }

    public short? CallingCode { get; set; }

    //[JsonIgnore]
    public bool Published { get; set; }

    //[JsonIgnore]
    public short DisplayOrder { get; set; }

    //[JsonIgnore]
    public virtual ICollection<Province> Provinces { get; set; }
}

public class Province : BaseEntity<ushort>
{
    //[JsonIgnore]
    public ushort CountryId { get; set; }

    public string Name { get; set; } = null!;

    public string Abbreviation { get; set; } = null!;

    //[JsonIgnore]
    public short DisplayOrder { get; set; }

    //[JsonIgnore]
    public virtual Country Country { get; set; } = null!;

    public virtual ICollection<District> Districts { get; set; } = new List<District>();
}

public class District : BaseEntity<ushort>
{
    [JsonIgnore]
    public ushort ProvinceId { get; set; }

    public string Name { get; set; } = null!;

    public virtual Province Province { get; set; } = null!;

    public virtual ICollection<Neighborhood> Neighborhoods { get; set; } = new List<Neighborhood>();
}

public class Neighborhood : BaseEntity<uint>
{
    public ushort DistrictId { get; set; }

    public string Name { get; set; } = null!;

    public virtual District District { get; set; } = null!;
}