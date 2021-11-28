using System;
using System.Collections.Generic;
using System.Text;
using static DalObject.DataSource;
using IDal.DO;
using System.Linq;


namespace DalObject
{
    public partial class DalObject: IDal.IDal
    {
        
        public void UpDate(Drone drone, int id)
        {
            CheckExistenceOfDrone(id);
            DronesList.Remove(DronesList.Find( item => item.Id == id));
            DronesList.Add(drone);
        }

       
        public void UpDate(BaseStation baseStation, int id)
        {
            CheckExistenceOfBaseStation(id);
            BaseStationsList.Remove(BaseStationsList.First( item => item.Id == id));
            BaseStationsList.Add(baseStation);
        }

        public void UpDate(Customer customer, string id)
        {
            CheckExistenceOfCustomer(id);
            CustomersList.Remove(CustomersList.First( item => item.Id == id));
            CustomersList.Add(customer);
        }
 
        public void UpDate(Parcel parcel, int id)
        {
            CheckExistenceOfParcel(id);
            ParcelsList.Remove(ParcelsList.First(item => item.Id == id));
            ParcelsList.Add(parcel);
        }
        //public void AssociateParcel(int parcelId, int droneId)
        //{
        //    for (int i = 0; i < ParcelsList.Count; i++)
        //    {
        //        if (ParcelsList[i].Id == parcelId)
        //        {
        //            Parcel parcel = ParcelsList[i];
        //            parcel.DroneId = droneId;
        //            ParcelsList[i] = parcel;
        //            break;
        //        }
        //    }
        //}

        //public void PickupParcel(int parcelId)
        //{
        //    for (int i = 0; i < ParcelsList.Count; i++)
        //    {
        //        if (ParcelsList[i].Id == parcelId)
        //        {
        //            Parcel parcel = ParcelsList[i];
        //            parcel.PickUpDate = DateTime.Now;
        //            ParcelsList[i] = parcel;
        //            break;
        //        }
        //    }
        //}


        public void SendDroneToRecharge(int droneId, int baseStationId)
        {
            DronesChargeList.Add(new DroneCharge() { DroneId = droneId, StationId = baseStationId, EntryTime = DateTime.Now });
        }

        public void ReleaseDroneFromRecharge(int droneId)
        {
            DronesChargeList.RemoveAll(dc => dc.DroneId == droneId);
        }
    }
}
