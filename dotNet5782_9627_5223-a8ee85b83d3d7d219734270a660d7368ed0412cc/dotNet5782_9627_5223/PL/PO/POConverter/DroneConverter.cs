using BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL.PO
{
    public static partial class POConverter
    {

        public static BO.Drone DronePOToBo(PO.Drone drone)
        {
            return new BO.Drone
            {
                Id = drone.Id,
                Battery = drone.Battery,
                Location = LocationPOTOBO(drone.Location),
                MaxWeight = (BO.WeightCategories)drone.Weight,
                Model = drone.Model,
                Parcel = drone.Parcel != null && drone.Parcel.Id != default ? new(drone.Parcel.Id,drone.Parcel.Priority,CustomerInParcelPOTOBO(drone.Parcel.Sender), CustomerInParcelPOTOBO(drone.Parcel.Target)) : null,
                Status = (BO.DroneStatuses)drone.Status,
            };
        }
        public static PO.Drone DroneBOToPO(BO.Drone drone, BLApi.IBL bl)
        {
            return new PO.Drone()
            {
                Id = drone.Id,
                Battery = drone.Battery,
                Location = LocationBOTOPO(drone.Location),
                Model = drone.Model,
                Status = (DroneStatuses)drone.Status,
                Weight = (WeightCategories)drone.MaxWeight,
                Parcel = drone.Parcel != null && drone.Parcel.Id != default ? new(drone.Parcel,bl) : null,
            };
        }

    }
}
