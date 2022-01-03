using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL.PO
{
    public class BoToPo
    {
        public static PO.Coordinate CoordinateDoToBo(BO.Coordinate coor)
        {
            return new PO.Coordinate() { InputCoorValue = coor.InputCoorValue, Degrees = coor.Degrees, Direction = (PO.Directions)coor.Direction, MyLocation = (PO.Locations)coor.MyLocation, Minutes = coor.Minutes, Seconds = coor.Seconds };
        }

        public static IEnumerable<PO.DroneInCharging> DroneInChargingBOToPO(IEnumerable<BO.DroneInCharging> inCharging)
        {
            var PoInCharging = from drone in inCharging
                               select new PO.DroneInCharging() { Battery = drone.Battery, Id = drone.Id };
            return PoInCharging;
        }

        public static PO.Location LocationBOTOPO(BO.Location location)
        {
            return new PO.Location(CoordinateDoToBo(location.CoorLongitude), CoordinateDoToBo(location.CoorLatitude));
        }

        public static PO.Location ParcelInPassingBOTOPO(BO.ParcelInPassing parcelInPassing)
        {
            PO.ParcelInPassing poParcelInPassing = new();

        }

    }
}
