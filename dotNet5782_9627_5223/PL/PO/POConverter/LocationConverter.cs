using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL.PO
{
    public static partial class POConverter
    {
        //-----------------------------Coordinate & Location Converting--------------------------
        public static PO.Coordinate CoordinateBoToPo(BO.Coordinate coor)
        {
            return new PO.Coordinate(coor.InputCoorValue,(Locations)coor.MyLocation);
        }

        public static BO.Coordinate CoordinatePoToBo(PO.Coordinate coor)
        {
            return new BO.Coordinate( coor.InputCoorValue,(BO.Locations)coor.MyLocation);
        }

        public static PO.Location LocationBOTOPO(BO.Location location)
        {
            if (location == null) return new PO.Location();
            return new PO.Location(CoordinateBoToPo(location.CoorLongitude), CoordinateBoToPo(location.CoorLatitude));
        }

        public static BO.Location LocationPOTOBO(PO.Location location)
        {
            return new BO.Location(CoordinatePoToBo(location.CoorLongitude), CoordinatePoToBo(location.CoorLatitude));
        }


       

    }
}
