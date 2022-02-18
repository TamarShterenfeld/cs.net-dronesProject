using System;
using System.Collections.Generic;
using System.Text;
using static DalObject.DataSource;
using DO;
using System.Linq;
using System.Runtime.CompilerServices;


namespace DalObject
{
    public partial class DalObject
    {
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void UpDate(Drone drone, int id)
        {
            CheckExistenceOfDrone(id);
            DronesList.Remove(DronesList.Find( item => item.Id == id));
            DronesList.Add(drone);
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void UpDate(BaseStation baseStation, int id)
        {
            CheckExistenceOfBaseStation(id);
            BaseStationsList.Remove(BaseStationsList.First( item => item.Id == id));
            BaseStationsList.Add(baseStation);
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void UpDate(Customer customer, string id)
        {
            CheckExistenceOfCustomer(id);
            CustomersList.Remove(CustomersList.First( item => item.Id == id));
            CustomersList.Add(customer);
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void UpDate(Parcel parcel, int id)
        {
            CheckExistenceOfParcel(id);
            ParcelsList.Remove(ParcelsList.First(item => item.Id == id));
            ParcelsList.Add(parcel);
        }


        [MethodImpl(MethodImplOptions.Synchronized)]
        public void SendDroneToRecharge(int droneId, int baseStationId)
        {
            CheckNotExistenceOfDroneCharge(droneId);
            DroneCharge droneCharge = new() { DroneId = droneId, StationId = baseStationId, EntryTime = DateTime.Now };
            Add(droneCharge);
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public BaseStation ReleaseDroneFromRecharge(int droneId)
        {
            CheckExistenceOfDroneCharge(droneId);
            DroneCharge droneCharge = GetDroneCharge(droneId);
            return Remove(droneCharge);
        }
    }
}

