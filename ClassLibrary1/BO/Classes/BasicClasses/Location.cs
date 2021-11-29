using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL
{
    namespace BO
    { 
        //a class which contains two coordinates which build together the class Location.
        public class Location
        {
            public Coordinate CoorLongitude { get; set; }
            public Coordinate CoorLatitude { get; set; }

            /// <summary>
            /// constructor
            /// </summary>
            /// <param name="longitude">location's longitude</param>
            /// <param name="latitude">location's latitude</param>
            public Location(Coordinate longitude, Coordinate latitude)
            {
                CoorLongitude = longitude;
                CoorLatitude = latitude;
            }

            // default constructor
            public Location() { }

            /// <summary>
            /// ovveride ToString function.
            /// </summary>
            /// <returns>description of the Location object</returns>
            public override string ToString()
            {
                return $"longitude: {CoorLongitude} , latitude: {CoorLatitude}";
            }
        }
    }
}
