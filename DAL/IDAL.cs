using System;
using System.Collections.Generic;
using System.Text;
using IDAL.DO;

namespace IDAL
{
    public interface IDal 
    {//
        void Add(IDAL.DO.BaseStation baseStation);
        void Add(IDAL.DO.Customer customer);
        void Add(IDAL.DO.Drone drone);
        void Add(IDAL.DO.Parcel parcel);
        //void AddBaseStation(BaseStation baseStation);
        //void AddDrone(Drone drone);
        //void AddCustomer(Customer customer);
        //void AddParcel(Parcel parcel);
        IEnumerable<Parcel> GettingNotAssociatedParcels();
        IEnumerable<BaseStation> GettingAvailableChargeSlots();
        void GetDrone(int id);
        void AvailableChargeSlots();
        void AssociateParcel(int parcelId, int droneId);
        void PickUpParcel(int parcelId, string senderId);
        void SupplyParcel(int parcelId, string targetId);
        void ChargingDrone(int droneId, int baseStationId);
        void StopDroneCharging(int droneId);
        double[] Electricity();
    }
}
