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


        double ComputeBatteryRemaining(DroneForList drone, ILocatable locatable)
        {
            return drone.Battery - ComputeMinBatteryNeeded(drone, locatable);
        }

        IDal.DO.Parcel ConvertBoToDoParcel(BO.Parcel parcel)
        {
            IDal.DO.Parcel doParcel = new()
            {
                Id = parcel.Id,
                DroneId = parcel.MyDrone.Id,
                Priority = ConvertToDoPriority(parcel.Priority),
                SenderId = parcel.Sender.Id,
                TargetId = parcel.Target.Id,
                Weight = ConvertToDoWeightCategory(parcel.Weight)
            };
            return doParcel;
        }

        Parcel ConvertParcelForListToParcel(ParcelForList parcelForList)
        {
            Parcel parcel = new()
            {
                Id = parcelForList.ParcelId,
                MyDrone = new()
                {
                    Id = parcelForList.DroneId
                },
                Priority = parcelForList.Priority,
                Sender = new()
                {
                    Id = parcelForList.SenderId
                },
                Target = new()
                {
                    Id = parcelForList.TargetId
                },
                Weight = parcelForList.Weight
            };
            return parcel;
        }
        IDal.DO.Drone ConvertBoToDoDrone(BO.Drone boDrone)
        {
            IDal.DO.Drone doDrone = new()
            {
                Id = boDrone.Id,
                MaxWeight = ConvertToDoWeightCategory(boDrone.MaxWeight),
                Model = boDrone.Model,
            };
            return doDrone;
        }

        Drone ConvertDroneForListToDrone(DroneForList droneForList)
        {
            Drone drone = new()
            {
                Id = droneForList.Id,
                Battery = droneForList.Battery,
                Location = droneForList.Location,
                MaxWeight = droneForList.MaxWeight,
                Model = droneForList.Model,
                Status = droneForList.Status,
                Parcel = new() { Id = droneForList.ParcelId }
            };
            return drone;
        }
        IDal.DO.WeightCategories ConvertToDoWeightCategory(BO.WeightCategories weight)
        {

            IDal.DO.WeightCategories weight1 = (IDal.DO.WeightCategories)Enum.GetNames(typeof(IDal.DO.WeightCategories)).GetValue((int)weight);
            return weight1;
        }
        IDal.DO.Priorities ConvertToDoPriority(BO.Priorities priority)
        {

            IDal.DO.Priorities priority1 = (IDal.DO.Priorities)Enum.GetNames(typeof(IDal.DO.Priorities)).GetValue((int)priority);
            return priority1;
        }
    }
}
