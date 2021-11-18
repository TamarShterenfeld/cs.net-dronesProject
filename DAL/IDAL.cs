using System;
using System.Collections.Generic;
using System.Text;
using IDAL.DO;

namespace IDAL
{
    public interface IDal
    {
        void Add(BaseStation baseStation);
        void Add(Drone drone);
        void Add(Customer customer);
        void Add(Parcel parcel);
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
