using System.Text.Json.Serialization;

namespace Ergenekon.Services.DTOs
{
    public class EntityDto<TKey> where TKey : struct
    {
        [JsonPropertyOrder(-1)]
        public TKey Id { get; set; } = default!;
    }

    public class CountryDto : EntityDto<int>
    {
        public string Name { get; set; }

        public string EnglishName { get; set; }

        public string Iso2Code { get; set; }

        public string Iso3Code { get; set; }

        public short NumericIsoCode { get; set; }

        public short? CallingCode { get; set; }

        public short DisplayOrder { get; set; }
    }

    public class StateProvinceDto : EntityDto<int>
    {
        public string Name { get; set; }

        public string Abbreviation { get; set; }

        public short DisplayOrder { get; set; }
    }
}
