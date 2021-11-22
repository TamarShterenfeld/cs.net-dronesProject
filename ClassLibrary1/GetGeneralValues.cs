using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IBL.BO;
using IDal.DO;


namespace IBL
{
    public partial class BL 
    {
        //----------------------------------Other GetObject Methods---------------------------------
        private double GetDroneBattery(int droneId)
        {
            return dronesForList.Find(drone => drone.Id == droneId).Battery;
        }

        private Location GetDroneLocation(int droneId)
        {
            return dronesForList.Find(drone => drone.Id == droneId).Location;
        }

        private int GetDroneParcelId(int droneId)
        {
            return dronesForList.Find(drone => drone.Id == droneId).ParcelId;
        }


        private BO.Parcel ParcelDOtOBO(IDal.DO.Parcel parcel)
        {
            BO.Parcel BOParcel = new()
            {
                Id = parcel.Id,
                Sender = GetBLCustomrInParcel(parcel.SenderId),
                Target = GetBLCustomrInParcel(parcel.TargetId),
                Weight = (BO.WeightCategories)parcel.Weight,
                Priority = (BO.Priorities)parcel.Priority,
                MyDrone = GetBLDroneInParcel(parcel.DroneId),
                ProductionDate = parcel.ProductionDate,
                AssociationDate = parcel.AssociationDate,
                PickUpDate = parcel.PickUpDate,
                SupplyDate = parcel.SupplyDate
            };
            return BOParcel;
        }

        int CatchAvailableChargeSlots(int stationId)
        {
            int caught = dal.AvailableChargeSlots(stationId);
            return caught;
        }
    }
}
