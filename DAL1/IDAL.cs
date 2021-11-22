using System;
using System.Collections.Generic;
using System.Text;
using IDal.DO;

namespace IDal
{
    public interface IDal
    {
        void Add(BaseStation baseStation);
        void Add(Drone drone);
        void Add(int droneCharge, int baseStationId);
        void Add(Customer customer);
        void Add(Parcel parcel);
        void AssociateParcel(int parcelId, int droneId);
        void PickupParcel(int parcelId);
        void SendDroneToRecharge(int droneId, int baseStationId);
        void ReleaseDroneFromRecharge(int droneId);
        BaseStation GetBaseStation(int requestedId);
        Drone GetDrone(int requestedId);
        Customer GetCustomer(string requestedId);
        Parcel GetParcel(int requestedId);
        IEnumerable<BaseStation> GetBaseStationsList();
        IEnumerable<Drone> GetDronesList();
        IEnumerable<Customer> GetCustomersList();
        IEnumerable<Parcel> GetParcelsList();
        IEnumerable<DroneCharge> GetDronesCharge();
        IEnumerable<Parcel> NotAssociatedParcels();
        IEnumerable<BaseStation> AvailableChargeStations();
        int AvailableChargeSlots(int baseStationId);
        IEnumerable<int> GetDronesIdInBaseStation(int requestedId);
        IEnumerable<DroneCharge> DronesChargingInMe(int stationId);
        double[] ElectricityConsuming();
    }
}
