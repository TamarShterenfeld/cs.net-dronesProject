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
                Priority = (IDal.DO.Priorities)(parcel.Priority),
                SenderId = parcel.Sender.Id,
                TargetId = parcel.Target.Id,
                Weight = (IDal.DO.WeightCategories)(parcel.Weight)
            };
            return doParcel;
        }

        Parcel  ConvertParcelForListToParcel(ParcelForList parcelForList)
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
                MaxWeight = (IDal.DO.WeightCategories)(boDrone.MaxWeight),
                Model = boDrone.Model,
            };
            return doDrone;
        }

        Drone ConvertDroneForListToDrone(DroneForList droneForList)
        {
            ParcelInPassing parcel = new() { Id = droneForList.ParcelId };
            Drone drone = new Drone(droneForList.Id, droneForList.Model, droneForList.MaxWeight, droneForList.Battery, droneForList.Status, parcel, droneForList.Location);
            return drone;
        }

    }
}
