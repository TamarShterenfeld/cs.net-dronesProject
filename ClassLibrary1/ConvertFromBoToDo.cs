using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IBL.BO;

namespace IBL
{
    public partial class BL : IBL
    {
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
            IDal.DO.WeightCategories weight1 = (IDal.DO.WeightCategories)weight;
            return weight1;
        }
        IDal.DO.Priorities ConvertToDoPriority(BO.Priorities priority)
        {

            IDal.DO.Priorities priority1 = (IDal.DO.Priorities)priority;
            return priority1;
        }

        
    }
}
