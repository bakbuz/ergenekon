using Ergenekon.Common;

namespace Ergenekon.Domain
{
    public class Country : Entity<int>
    {
        public Country()
        {
            StateProvinces = new HashSet<StateProvince>();
        }

        public string Name { get; set; }

        public string EnglishName { get; set; }

        public string Iso2Code { get; set; }

        public string Iso3Code { get; set; }

        public short NumericIsoCode { get; set; }

        public short? CallingCode { get; set; }

        public bool Published { get; set; }

        public short DisplayOrder { get; set; }

        public virtual ICollection<StateProvince> StateProvinces { get; set; }
    }

    public class StateProvince : Entity<int>
    {
        public int CountryId { get; set; }

        public string Name { get; set; }

        public string Abbreviation { get; set; }

        public short DisplayOrder { get; set; }

        public virtual Country Country { get; set; }
    }
}
