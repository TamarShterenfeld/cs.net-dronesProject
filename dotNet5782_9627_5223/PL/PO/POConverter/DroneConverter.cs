using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL.PO
{
    public static partial class POConverter
    {
        public static BO.Drone DronePOToBo(PL.PO.Drone drone)
        {
            return new BO.Drone(drone.Id, drone.Model, (BO.WeightCategories)drone.Weight,
                drone.Battery, (BO.DroneStatuses)drone.Status,
                drone.Parcel != null ?  new(drone.Parcel.Id, (BO.Priorities)drone.Parcel.Priority,
                CustomerInParcelPOTOBO(drone.Parcel.Sender),
                CustomerInParcelPOTOBO(drone.Parcel.Target)):null,
                LocationPOTOBO(drone.Location));
        }
    }
}
