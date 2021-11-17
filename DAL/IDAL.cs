using System;
using System.Collections.Generic;
using System.Text;
using IDAL.DO;

namespace IDAL
{
    public interface IDal
    {
        void Add(IDAL.DO.BaseStation baseStation);
        void Add(IDAL.DO.Drone drone);
        void Add(IDAL.DO.Customer customer);
        void Add(IDAL.DO.Parcel parcel);
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
        IEnumerable<DroneCharge> GetDronesCharge();
        IEnumerable<Parcel> NotAssociatedParcels();
        IEnumerable<BaseStation> AvailableChargingStations();
        IEnumerable<BaseStation> AvailableChargingSlots();
        IEnumerable<int> GetDronesIdInBaseStation(int requestedId);
    }
}
