using System.Text.Json.Serialization;

namespace Ergenekon.Domain.Entities;

public class Country : BaseEntity<byte>
{
    public Country()
    {
        StateProvinces = new HashSet<StateProvince>();
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
    public virtual ICollection<StateProvince> StateProvinces { get; set; }
}

public class StateProvince : BaseEntity<short>
{
    //[JsonIgnore]
    public byte CountryId { get; set; }

    public string Name { get; set; } = null!;

    public string Abbreviation { get; set; } = null!;

    //[JsonIgnore]
    public short DisplayOrder { get; set; }

    //[JsonIgnore]
    public virtual Country Country { get; set; } = null!;
}

public class District : BaseEntity<short>
{
    [JsonIgnore]
    public short StateProvinceId { get; set; }

    public string Name { get; set; } = null!;
}
