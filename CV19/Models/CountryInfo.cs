using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace CV19.Models
{
    internal class CountryInfo : PlaceInfo
    {
        private Point? _Location;

        public override Point Location
        {
            get
            {
                if (_Location != null)
                    return (Point)_Location;

                if (ProvinceCounts is null) return default;

                var average_x = ProvinceCounts.Average(p => p.Location.X);
                var average_y = ProvinceCounts.Average(p => p.Location.Y);

                return (Point)(_Location = new Point(average_x, average_y));
            }
            set => _Location = value;
        }

        public IEnumerable<PlaceInfo> ProvinceCounts { get; set; }
    }
}