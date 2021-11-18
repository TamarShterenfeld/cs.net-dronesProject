using System;
using System.Collections.Generic;
using System.Text;


namespace ConsoleUI_BL
{
    ///// <summary>
    ///// all the different enums that were used in the project
    ///// are collected here - just because they may belong to more than one structure.
    ///// </summary>
    //public enum WeightCategories
    //{
    //    Light = 1, Average, Heavy
    //}

    //public enum DroneStatuses
    //{
    //    Available = 1, Maintenance, Shipment
    //}
    //public enum Priorities
    //{
    //    Standard = 1, Fast, Emergency
    //}

    //{
    /// <summary>
    /// all the different enums that were used in the project are collected here.
    /// </summary>
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



