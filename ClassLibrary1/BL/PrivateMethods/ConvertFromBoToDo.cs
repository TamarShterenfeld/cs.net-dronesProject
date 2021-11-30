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
        /// <summary>
        /// the function converts BO.Parcel object to DO.Parcel object.
        /// </summary>
        /// <param name="parcel">the object to convert</param>
        /// <returns>a DO.Parcel converted object</returns>
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

        /// <summary>
        /// the function converts ParcelForList object to BO.Parcel object.
        /// </summary>
        /// <param name="parcel">the object to convert</param>
        /// <returns>a BO.Parcel converted object</returns>
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

        /// <summary>
        /// the function converts a BO.Drone object to a DO.Drone object
        /// </summary>
        /// <param name="boDrone">the object to convert</param>
        /// <returns>a DO.Drone converted object</returns>
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

        /// <summary>
        /// the function converts a DroneForList object to a BO.Drone object.
        /// </summary>
        /// <param name="droneForList">the object to convert</param>
        /// <returns>a BO.Drone converted object</returns>
        Drone ConvertDroneForListToDrone(DroneForList droneForList)
        {
            ParcelInPassing parcel = new() { Id = droneForList.ParcelId };
            Drone drone = new Drone(droneForList.Id, droneForList.Model, droneForList.MaxWeight, droneForList.Battery, droneForList.Status, parcel, droneForList.Location);
            return drone;
        }

    }
}
