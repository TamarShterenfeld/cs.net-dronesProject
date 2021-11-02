using System;
using System.Collections.Generic;
using System.Text;


namespace IDAL
{
    public interface IDAL
    {
        static int IncreaseParcelIndex() { return 1; }
        static void AddBaseStation(int id, string name, double longitude, double latitude, int chrgeSlots) { }
        static void AddDrone(int id, string model, string maxWeight, double battery) { }
        static void AddCustomer(string id, string name, string phone, double longitude, double latitude) { }
        static void AddParcel(int id, string senderId, string targetId, int droneId, string weight, string priority) { }
        static IEnumerable<Parcel> GettingNotAssociatedParcels() { return null;  }
        static IEnumerable<BaseStation> GettingAvailableChargeSlots() { return null;  }
        static void DisplayBaseStation(int id) { }
        static void DisplayDrone(int id) { }
        static void DisplayCustomer(string id) { }
        static void DisplayParcel(int id) { }
        static void chackingIdentitiesOfParcel(int id, string senderId, string targetId, int droneId) { }
        static void inputIntValue(ref int id) { }
        static void ShowBaseStationsList() { }
        static void ShowDronesList() { }
        static void ShowCustomersList() { }
        static void ShowParcelsList() { }
        static void ShowNotAssociatedParcelsList() { }
        static void AvailableChargeSlots() { }
        static void AssociateParcel(int parcelId, int droneId) { }
        static void PickUpParcel(int parcelId, string senderId) { }
        static void SupplyParcel(int parcelId, string targetId) { }
        static void ChargingDrone(int droneId, int baseStationId) { }
        static void StopDroneCharging(int droneId) { }
        static double[] Electricity() { return null; }
    }
}
