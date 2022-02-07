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
            return new PO.Coordinate() { InputCoorValue = coor.InputCoorValue, Degrees = coor.Degrees, Direction = (Directions)coor.Direction, MyLocation = (Locations)coor.MyLocation, Minutes = coor.Minutes, Seconds = coor.Seconds };
        }

        public static BO.Coordinate CoordinatePoToBo(PO.Coordinate coor)
        {
            return new BO.Coordinate() { InputCoorValue = coor.InputCoorValue, Degrees = coor.Degrees, Direction = (BO.Directions)coor.Direction, MyLocation = (BO.Locations)coor.MyLocation, Minutes = coor.Minutes, Seconds = coor.Seconds };
        }

        public static PO.Location LocationBOTOPO(BO.Location location)
        {
            return new PO.Location(CoordinateBoToPo(location.CoorLongitude), CoordinateBoToPo(location.CoorLatitude));
        }

        public static BO.Location LocationPOTOBO(PO.Location location)
        {
            return new BO.Location(CoordinatePoToBo(location.CoorLongitude), CoordinatePoToBo(location.CoorLatitude));
        }


       

    }
}
