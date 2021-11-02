using System;
using System.Collections.Generic;
using System.Text;
using IDAL.DO;
using static IBL.BO.DalObject;
using static IBL.BO.DataSource;
using System.Linq;
using static IBL.BO.Coordinate;
using static IBL.BO.Drone;
using static IBL.BO.Customer;
using static IBL.BO.DroneCharge;
using static IBL.BO.Parcel;
using static IBL.BO.BaseStation;
using static IDAL.DO.OverloadException;
using IBL.BO;

namespace IBL
{
    namespace BO
    {


        /// <summary>
        ///the class DalObject contains all the needed methods 
        ///which are connected to the data (in DataSource class) of the program.
        /// </summary>
        public partial class DalObject : IDAL.IDal
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
            public static int IncreaseParcelIndex()
            {
                return DataSource.Config.ParcelId++;
            }

            /// <summary>
            /// returns a list of the not associated parcels
            /// this is done by checking the assoviation date - if it was changed from the default value.
            /// </summary>
            /// <returns></returns>
            public static IEnumerable<Parcel> GettingNotAssociatedParcels()
            {
                List<Parcel> notAssociatedDronesList = new List<Parcel>();
                foreach (IBL.BO.Parcel parcel in ParcelsList)
                {
                    //checking if the association value isn't initalized to another value than the default value.
                    if (parcel.Association == new DateTime(01 / 01 / 0001))
                        notAssociatedDronesList.Add(parcel);
                }
                return notAssociatedDronesList;
            }

            /// <summary>
            /// The function creates a list of all the available charge solts
            /// </summary>
            /// <returns>a list of all the available charge solts</returns>
            public static IEnumerable<BaseStation> GettingAvailableChargeSlots()
            {
                List<BaseStation> AvailableChargeSlotsList = new List<BaseStation>(BaseStationsList.Count);
                foreach (BaseStation item in BaseStationsList)
                {
                    if (item.ChargeSlots > 0)
                        AvailableChargeSlotsList.Add(item);
                }
                return AvailableChargeSlotsList;
            }
        }
    }
}

