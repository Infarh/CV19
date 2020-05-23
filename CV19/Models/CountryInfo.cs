using System.Collections.Generic;

namespace CV19.Models
{
    internal class CountryInfo : PlaceInfo
    {
        public IEnumerable<PlaceInfo> ProvinceCounts { get; set; }
    }
}