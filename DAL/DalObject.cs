using System;
using System.Collections.Generic;
using System.Text;
using static IDAL.DO.IDAL;
using IDAL.DO;
using static DalObject.DataSource.Config;
using static DalObject.DataSource;

namespace DalObject
{
    public partial class DalObject
    {
        // constructor
        public DalObject()
        {
            DataSource.Initialize();
        }
        public int AddParcel()
        {
            return DataSource.Config.ParcelId++;
        }
        public static List<Parcel> GettingNotAssociatedParcels()
        {
            List<Parcel> notAssociatedDronesList = new List<Parcel>();
            foreach (Parcel parcel in ParcelsList)
            {
                //the current parcel's drone's id isn't associated to any parcel
                if (parcel.DroneId == 0)
                {
                    //if there's no a drone id = 0 or that there is one' but it's available.
                    if (searchDrone(parcel.DroneId) == -1 || DronesList[searchDrone(parcel.DroneId)].Status == DroneStatuses.Available)
                        notAssociatedDronesList.Add(parcel);
                }
            }
            return notAssociatedDronesList;
        }
        
        public static List<BaseStation> GettingAvailableChargeSlots()
        {
            List<BaseStation> AvailableChargeSlotsList = new List<BaseStation>(IndexOfBaseStation+1);
            foreach (BaseStation item in BaseStationsList)
            {
                if (item.ChargeSlots > 0)
                    AvailableChargeSlotsList.Add(item);
            }
            return AvailableChargeSlotsList;
        }
    }
}

