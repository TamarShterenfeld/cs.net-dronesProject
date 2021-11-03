using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL
{
    namespace BO
    {
        public class Location
        {
            private Coordinate coorLongitude;
            private Coordinate coorLatitude;

            public Coordinate CoorLongitude { get; set; }

            public Coordinate CoorLatitude { get; set; }

            public Location(Coordinate longitude, Coordinate latitude)
            {
                coorLongitude = longitude;
                coorLatitude = latitude;
            }

            public override string ToString()
            {
                return $"Longitude: {coorLongitude}\n"+ $"Latitude{coorLongitude}"; 
            }
        }
    }
}
