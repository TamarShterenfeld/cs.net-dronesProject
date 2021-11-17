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
        IEnumerable<Parcel> GetNotAssociatedParcels();
        IEnumerable<BaseStation> GetAvailableChargeSlots();
        IEnumerable<Parcel> GetParcelsList();
        IEnumerable<Customer> GetCustomersList();
        IEnumerable<BaseStation> GetBaseStationsList();
        IEnumerable<Drone> GetDronesList();
        Drone GetDrone(int id);
        Customer GetCustomer(string id);
        BaseStation GetBaseStation(int id);
        Parcel GetParcel(int id);
        BaseStation AvailableChargeSlots();
        void AssociateParcel(int parcelId, int droneId);
        void PickUpParcel(int parcelId, string senderId);
        void SupplyParcel(int parcelId, string targetId);
        void ChargingDrone(int droneId, int baseStationId);
        void StopDroneCharging(int droneId);
        double[] Electricity();
    }
}
