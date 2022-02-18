using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL.PO
{
    public static partial class POConverter
    {
        //--------------------------DroneInCharging Converting-------------------------
        public static PO.DroneInCharging DroneInChargingBoToPo(BO.DroneInCharging drone)
        {
            return new PO.DroneInCharging() { Id = drone.Id, Battery = drone.Battery };
        }

        public static BO.DroneInCharging DroneInChargingPoToBo(PO.DroneInCharging drone)
        {
            return new BO.DroneInCharging() { Id = drone.Id, Battery = drone.Battery };
        }

        public static IEnumerable<BO.DroneInCharging> DroneInChargingListPoToBo(IEnumerable<PO.DroneInCharging> droneList)
        {
            if (droneList == null)
                return Enumerable.Empty<BO.DroneInCharging>();
            else
                return droneList.Select(drone => DroneInChargingPoToBo(drone));
        }

        public static IEnumerable<PO.DroneInCharging> DroneInChargingListBOToPO(IEnumerable<BO.DroneInCharging> inCharging)
        {
            var PoInCharging = from drone in inCharging
                               select new PO.DroneInCharging() { Battery = drone.Battery, Id = drone.Id };
            return PoInCharging;
        }

        public static IEnumerable<PO.DroneInCharging> DroneInChargingListBoToPo(IEnumerable<BO.DroneInCharging> droneList)
        {
            List<PO.DroneInCharging> boDroneList = new();
            foreach (var drone in droneList)
            {
                boDroneList.Add(DroneInChargingBoToPo(drone));
            }
            return boDroneList;
        }


    }
}
