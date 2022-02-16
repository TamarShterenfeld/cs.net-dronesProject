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
                drone.Battery, (BO.DroneStatuses)Enum.Parse(typeof(BO.DroneStatuses), drone.Status.ToString()),
                drone.Parcel != null ? new(drone.Parcel.Id, (BO.Priorities)drone.Parcel.Priority,
                CustomerInParcelPOTOBO(drone.Parcel.Sender),
                CustomerInParcelPOTOBO(drone.Parcel.Target)) : null,
                LocationPOTOBO(drone.Location));
        }

        public static PO.Drone DroneBOToPO(BO.Drone drone, BLApi.IBL bl)
        {
            return new PO.Drone()
            {
                Id = drone.Id,
                Battery = drone.Battery,
                Location = LocationBOTOPO(drone.Location),
                Model = drone.Model,
                Status = (DroneStatuses)Enum.Parse(typeof(DroneStatuses), drone.Status.ToString()),
                Weight = (WeightCategories)Enum.Parse(typeof(WeightCategories), drone.MaxWeight.ToString()),
                Parcel = drone.Parcel != null && drone.Parcel.Id != default ? new ParcelInPassing(drone.Parcel, bl) : null,

            };
        }

    }
}
