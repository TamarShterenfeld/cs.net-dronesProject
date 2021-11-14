using System;
using System.Collections.Generic;
using System.Text;
using IDAL.DO;

namespace IDAL
{
    public interface IDal
    {
        int IncreaseParcelIndex();
        void Add(DO.BaseStation baseStation);
        void AddBaseStation(int id, string name, double longitude, double latitude, int chrgeSlots);
        void AddDrone(int id, string model, string maxWeight, double battery);
        void AddCustomer(string id, string name, string phone, double longitude, double latitude);
        void AddParcel(int id, string senderId, string targetId, int droneId, string weight, string priority);
        IEnumerable<Parcel> GettingNotAssociatedParcels();
        IEnumerable<BaseStation> GettingAvailableChargeSlots();
        void DisplayBaseStation(int id);
        void GetDrone(int id);
        void DisplayCustomer(string id);
        void DisplayParcel(int id);
        void chackingIdentitiesOfParcel(int id, string senderId, string targetId, int droneId);
        void inputIntValue(ref int id);
        void ShowBaseStationsList();
        void ShowDronesList();
        void ShowCustomersList();
        void ShowParcelsList();
        void ShowNotAssociatedParcelsList();
        void AvailableChargeSlots();
        void AssociateParcel(int parcelId, int droneId);
        void PickUpParcel(int parcelId, string senderId);
        void SupplyParcel(int parcelId, string targetId);
        void ChargingDrone(int droneId, int baseStationId);
        void StopDroneCharging(int droneId);
        double[] Electricity();
    }
}
