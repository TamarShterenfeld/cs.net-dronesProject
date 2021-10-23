using System;
using System.Collections.Generic;
using System.Text;
using static IDAL.DO.IDAL;
using IDAL.DO;
using static DalObject.DataSource.Config;
using static DalObject.DataSource;
using System.Linq;

namespace DalObject
{
    public partial class DalObject
    {
        // constructor
        public DalObject()
        {
            DataSource.Initialize();
        }

        /// <summary>
        /// The function calculates the parcel's id
        /// </summary>
        /// <returns>parcel's id</returns>
        public int AddParcel()
        {
            return DataSource.Config.ParcelId++;
        }
        public static List<Parcel> GettingNotAssociatedParcels()
        {
            List<Parcel> notAssociatedDronesList = new List<Parcel>();
            foreach (Parcel parcel in ParcelsList)
            {
                //checking if the association value isn't initalized to another value than the default value.
                if (parcel.Association == new DateTime(01/01/0001))
                    notAssociatedDronesList.Add(parcel);
            }
            return notAssociatedDronesList;
        }

        /// <summary>
        /// The function creates a list of all the available charge solts
        /// </summary>
        /// <returns>a list of all the available charge solts</returns>
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

