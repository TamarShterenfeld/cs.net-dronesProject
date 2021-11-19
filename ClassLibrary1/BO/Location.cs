using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static IDal.DO.Coordinate;
using IDal.DO;

namespace IBL
{
    namespace BO
    { 
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

            public Location(double longitude, double latitude)
            {
                Coordinate longi = new Coordinate(longitude, Locations.Longitude);
                Coordinate lati = new Coordinate(latitude, Locations.Latitude);
                CoorLongitude = longi;
                CoorLatitude = lati;
            }

            // default constructor
            public Location() { }

            /// <summary>
            /// ovveride ToString function.
            /// </summary>
            /// <returns>description of the Location object</returns>
            public override string ToString()
            {
                return $"Longitude: {CoorLongitude}\n" + $"Latitude{CoorLatitude}";
            }
        }
    }
}
