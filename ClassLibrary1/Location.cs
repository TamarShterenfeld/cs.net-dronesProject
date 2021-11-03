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

            /// <summary>
            /// constructor
            /// </summary>
            /// <param name="longitude">location's longitude</param>
            /// <param name="latitude">location's latitude</param>
            public Location(Coordinate longitude, Coordinate latitude) 
            {
                coorLongitude = longitude;
                coorLatitude = latitude;
            }

            /// <summary>
            /// ovveride ToString function.
            /// </summary>
            /// <returns>description of the Location object</returns>
            public override string ToString()
            {
                return $"Longitude: {coorLongitude}\n"+ $"Latitude{coorLongitude}"; 
            }
        }
    }
}
