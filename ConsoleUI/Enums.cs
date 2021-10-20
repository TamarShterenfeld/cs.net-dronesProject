using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleUI
{
    public enum Options
    {
        Add = 1, UpDate, Display, ShowingLists, Exit
    }

    public enum AddOptions
    {
        BaseStation = 1, Drone, Customer, Parcel,
    }

    public enum UpDateOptions
    {
        AssociatingParcel = 1, PickingUpParcel, SupplyingParcel, ChargingDrone, StopDroneCharging,
    }

    public enum DisplayOptions
    {
        BaseStation = 1, Drone, Customer, Parcel,
    }

    public enum ShowingListsOptions
    {
        BaseStations = 1, Drones, Customers, Parcels, NotAssociatedParcels, AvailableChargeSlots,
    }
}
