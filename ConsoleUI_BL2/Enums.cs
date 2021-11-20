using System;
using System.Collections.Generic;
using System.Text;


namespace ConsoleUI_BL
{
    /// <summary>
    /// all the different enums that were used in the project are collected here.
    /// </summary>
    public enum Options
    {
        Add = 1, UpDate, Display, ShowLists, Exit
    }

    public enum AddOptions
    {
        BaseStation = 1, Drone, Customer, Parcel,
    }

    public enum UpDateOptions
    {
        AssociationParcel = 1, PickUpParcel, SupplyParcel, ChargeDrone, StopDroneCharge,
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



