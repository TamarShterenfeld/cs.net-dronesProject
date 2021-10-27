using System;
using System.Collections.Generic;
using System.Text;
using static IDAL.DO.IDAL;
using IDAL.DO;
using static DalObject.DataSource.Config;
using static DalObject.DataSource;
using System.Linq;
using static DAL.Coordinate;

namespace DalObject
{
    /// <summary>
    ///the class DalObject contains all the needed methods 
    ///which are connected to the data (in DataSource class) of the program.
    /// </summary>
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
        public static int  IncreaseParcelIndex()
        {
            return DataSource.Config.ParcelId++;
        }
        
        /// <summary>
        /// returns a list of the not associated parcels
        /// this is done by checking the assoviation date - if it was changed from the default value.
        /// </summary>
        /// <returns></returns>
        public static Parcel[] GettingNotAssociatedParcels()
        {
            List<Parcel> notAssociatedDronesList = new List<Parcel>();
            foreach (Parcel parcel in ParcelsList)
            {
                //checking if the association value isn't initalized to another value than the default value.
                if (parcel.Association == new DateTime(01/01/0001))
                    notAssociatedDronesList.Add(parcel);
            }
            return notAssociatedDronesList.ToArray();
        }

        /// <summary>
        /// The function creates a list of all the available charge solts
        /// </summary>
        /// <returns>a list of all the available charge solts</returns>
        public static BaseStation[] GettingAvailableChargeSlots()
        {
            List<BaseStation> AvailableChargeSlotsList = new List<BaseStation>(IndexOfBaseStation+1);
            foreach (BaseStation item in BaseStationsList)
            {
                if (item.ChargeSlots > 0)
                    AvailableChargeSlotsList.Add(item);
            }
            return AvailableChargeSlotsList.ToArray();
        }
    }
}

