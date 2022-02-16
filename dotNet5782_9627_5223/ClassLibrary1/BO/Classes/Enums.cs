using System;

namespace BO
{
    /// <summary>
    /// an enum which contains all the different directions.
    /// </summary>
    public enum Directions
    {
        NORTH, EAST, WEST, SOUTH
    }

    /// <summary>
    /// 
    /// an enum which contains the two options for a location - longitude / latitude.
    /// </summary>
    public enum Locations
    {
        Latitude, Longitude
    }

    /// <summary>
    /// an enum which contains all the three categories of weight a parcel can weigh.
    /// </summary>
    public enum WeightCategories
    {
        Light = 1, Average, Heavy
    }

    /// <summary>
    /// an enum which contains the three statuses a drone can be in.
    /// </summary>
    public enum DroneStatuses
    {
        Available = 1, Maintenance, Shipment
    }
    public enum Priorities
    {
        Standard = 1, Fast, Emergency
    }

    /// <summary>
    /// an enum which contains the four statuses of passing a parcel can be in.
    /// </summary>
    public enum ParcelStatuses
    {
        Production, Associated, PickedUp, Supplied
    }

    /// <summary>
    /// an enum which signs if the parcel was sended from the customer or was sended to him.
    /// </summary>
    public enum FromOrTo
    {
        From, To
    }

    /// <summary>
    /// an enum which contains the three actions of a parcel (in update option) which have an appropriate Exception.
    /// </summary>
    public enum ParcelActions
    {
        Associate, PickUp, Supply, SendforRecharge,
    }

    /// <summary>
    /// an enum which contains some parameters in order to know the battery rate.
    /// </summary>
    public enum BatteryUsage
    { 
        Available, Light, Average, Heavy, Maintenance
    }

}
