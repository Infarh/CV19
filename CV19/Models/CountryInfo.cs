using System.Collections.Generic;

namespace CV19.Models
{
    /// <summary>
    /// Информация по стране
    /// </summary>
    internal class CountryInfo : PlaceInfo
    {
        /// <summary>
        /// Информация по провинциям
        /// </summary>
        public IEnumerable<ProvinceInfo> ProvinceCounts { get; set; }
    }
}