using PL.PO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL
{
    internal class PrivateMethods
    {
        public static PO.Location ConvertBoLocationToPoLocation(BO.Location location)
        {
            PO.Coordinate longitude = new(location.CoorLongitude.Degrees, Locations.Longitude);
            PO.Coordinate latitude = new(location.CoorLatitude.Degrees, Locations.Latitude);
            return new(longitude, latitude);
        }
    }
}
