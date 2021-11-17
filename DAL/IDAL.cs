using System;
using System.Collections.Generic;
using System.Text;
using IDAL.DO;

namespace IDAL
{
    public interface IDal
    {
        //void Add(DO.BaseStation baseStation);
        //void AddBaseStation(BaseStation baseStation);
        //void AddDrone(Drone drone);
        //void AddCustomer(Customer customer);
        //void AddParcel(Parcel parcel);
        //int AddBaseStation(string stationName, int positions);
        //int AddDrone();
        void AssociateParcel(int parcelId, int droneId);
        void PickupParcel(int parcelId);
        void SendDroneToRecharge(int droneId, int baseStationId);
        void ReleaseDroneFromRecharge(int droneId);
        BaseStation GetBaseStation(int requestedId);
        Drone GetDrone(int requestedId);
        Customer GetCustomer(int requestedId);
        Parcel GetParcel(int requestedId);
        IEnumerable<BaseStation> GetBaseStationsList();
        IEnumerable<Drone> GetDronesList();
        IEnumerable<Customer> GetCustomersList();
        IEnumerable<Parcel> GetParcelsList();
        IEnumerable<DroneCharge> GetDronesCharges();
        IEnumerable<Parcel> NotAssociatedParcels();
        IEnumerable<BaseStation> AvailableChargingStations();
        IEnumerable<BaseStation> AvailableChargingSlots();
        IEnumerable<int> GetDronesIdInBaseStation(int requestedId);
    }
}
