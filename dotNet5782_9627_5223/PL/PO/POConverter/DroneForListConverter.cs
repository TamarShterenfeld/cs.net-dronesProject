using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL.PO
{
    public static partial class POConverter
    {
        /// <summary>
        /// convert DroneForList from BO to PO.
        /// </summary>
        /// <param name="drone"> BO droneForList</param>
        /// <returns>PO droneForList</returns>
        public static PL.PO.DroneForList DroneForListBOToPO(BO.DroneForList drone)
        {
            return new PL.PO.DroneForList(drone.Id,drone.ParcelId,drone.Model,(WeightCategories)drone.MaxWeight,drone.Battery,(DroneStatuses)drone.Status, LocationBOTOPO(drone.Location));
        }

        /// <summary>
        /// convert DroneForList from PO to BO.
        /// </summary>
        /// <param name="drone">PO droneForList</param>
        /// <returns>BO droneForList</returns>
        public static BO.DroneForList DroneForListPOToBO(PL.PO.DroneForList drone)
        {
            return new BO.DroneForList(drone.Id, drone.ParcelId, drone.Model, (BO.WeightCategories)drone.MaxWeight, drone.Battery, (BO.DroneStatuses)drone.Status, LocationPOTOBO(drone.Location));
        }

        /// <summary>
        /// convert DroneForList list's from BO to PO.
        /// </summary>
        /// <param name="drones">BO droneForList list</param>
        /// <returns>PO droneForList list</returns>
        public static IEnumerable<PL.PO.DroneForList> DroneListBOToPO(IEnumerable<BO.DroneForList> drones)
        {
            return from drone in drones
                   select DroneForListBOToPO(drone);
        }

    }
}
