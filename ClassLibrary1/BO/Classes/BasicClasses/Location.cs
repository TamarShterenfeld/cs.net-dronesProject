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
            static readonly  Random rand = new();
            Coordinate coorLongitude = new(0.1234 * rand.Next(0, 180) + 0.8766 * rand.Next(-180, 0), Locations.Longitude);
            Coordinate coorLatitude = new (0.8766 * rand.Next(0, 180) + 0.1234 * rand.Next(-180, 0), Locations.Latitude);
            public Coordinate CoorLongitude
            {
                set
                {
                    coorLongitude = value;
                }
                get
                {
                    return coorLongitude;
                }
            }

            public Coordinate CoorLatitude
            {
                set
                {
                    coorLatitude = value;
                }
                get
                {
                    return coorLatitude;
                }
            }

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
            public Location()
            { }

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
