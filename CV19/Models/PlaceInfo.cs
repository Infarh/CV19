using System.Collections.Generic;
using System.Windows;

namespace CV19.Models
{
    /// <summary>
    /// Информация по месту
    /// </summary>
    internal class PlaceInfo
    {
        /// <summary>
        /// Название места
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Географическое расположение
        /// </summary>
        public Point Location { get; set; }
        /// <summary>
        /// Количество заболевших
        /// </summary>
        public IEnumerable<ConfirmedCount> Counts { get; set; }
    }
}
