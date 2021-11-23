using System;
using System.Collections.Generic;
using System.Text;


namespace ConsoleUI_BL
{
    
    /// <summary>
    /// enum of the different options of the main navigate function 
    /// (a method of Menu class)
    /// </summary>
    public enum Options
    {
        Add = 1, UpDate, Display, ShowLists, Exit
    }

   /// <summary>
   /// enum of the different add options.
   /// </summary>
    public enum AddOptions
    {
        BaseStation = 1, Drone, Customer, Parcel,
    }

    /// <summary>
    /// enum of the different update options.
    /// </summary>
    public enum UpDateOptions
    {
        UpdateDrone = 1, UpdateBaseStation, UpdateCustomer,  ChargeDrone, StopDroneCharge, AssociationParcel, PickUpParcel, SupplyParcel, 
    }

    /// <summary>
    /// enum of the different display options.
    /// </summary>
    public enum DisplayOptions
    {
        BaseStation = 1, Drone, Customer, Parcel,
    }

    /// <summary>
    /// enum of the different showingList options.
    /// </summary>
    public enum ShowingListsOptions
    {
        BaseStations = 1, Drones, Customers, Parcels, NotAssociatedParcels, AvailableChargeSlots,
    }
}



