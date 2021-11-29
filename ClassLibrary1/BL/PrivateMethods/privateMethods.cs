using IBL.BO;
using System;

namespace IBL
{
    public partial class BL : IBL
    {

        public static int GetParcelIndex()
        {
            return DalObject.DalObject.IncreaseParcelIndex();
        }


        bool DroneReachLocation(DroneForList drone, ILocatable locatable)
        {
            return drone.Battery - ComputeMinBatteryNeeded(drone, locatable) >= 0;
        }


        double ComputeBatteryRemained(DroneForList drone, ILocatable locatable)
        {
            return (drone.Battery - ComputeMinBatteryNeeded(drone, locatable));
        }

        private DroneForList GetOneDroneForList(BO.Drone drone)
        {
            DroneForList droneForList = new()
            {
                Id = drone.Id,
                Model = drone.Model,
                MaxWeight = drone.MaxWeight,
                Status = drone.Status,
                ParcelId = drone.Parcel != null ? drone.Parcel.Id : 0,
                Location = drone.Location,
                Battery = drone.Battery
            };
            return droneForList;
        }

    }
}
