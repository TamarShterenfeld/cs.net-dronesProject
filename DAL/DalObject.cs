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

        public int AddingParcel()
        {
            return DataSource.Config.ParcelId++;
        }

        public static List<Drone> GettingDronesList()
        {
            List<Drone> dronesList = new List<Drone>(IndexOfDrone+1);
            foreach (Drone item in DronesArr)
            {
                dronesList.Add(item);
            }
            return dronesList;
        }

        public static List<BaseStation> GettingBaseStationList()
        {
            List<BaseStation> baseStationsList = new List<BaseStation>(IndexOfBaseStation+1);
            foreach (BaseStation item in BaseStationsArr)
            {
                baseStationsList.Add(item);
            }
            return baseStationsList;
        }

        public static List<Customer> GettingCustomerList()
        {
            List<Customer> customersList = new List<Customer>(IndexOfCustomer+1);
            foreach (Customer item in CustomersArr)
            {
                customersList.Add(item);
            }
            return customersList;
        }

        public static List<Parcel> GettingParcelList()
        {
            List<Parcel> parcelsList = new List<Parcel>(IndexOfParcel+1);
            foreach (Parcel item in ParcelsArr)
            {
                parcelsList.Add(item);
            }
            return parcelsList;
        }

        public static List<Parcel> GettingNotAssociatedParcels()
        {
            //the needed size of the notAssociatedDronesList is the space between IndexOfParcel and DroneChargeList.Count + 1
            //because we need the amount of parcels which appearin ParcelArr and don't appear in DroneChargeList
            List<Parcel> notAssociatedDronesList = new List<Parcel>(IndexOfParcel - DroneChargeList.Count +1);
            foreach (Parcel item in ParcelsArr)
            {
                //check for each parcel if it related to dronesChargeList
                //add to notAssociatedDronesList the parcels which aren't related to dronesChargeList.
                if (searchDronesChargeList(item.DroneId) == -1)
                {
                    notAssociatedDronesList.Add(item);
                }
            }
            return notAssociatedDronesList;
        }

        private static int searchDronesChargeList(int droneId)
        {
            DroneCharge item;
            for(int i =0; i < DroneChargeList.Count; i++)
            {
                item = DroneChargeList[i];
                if (item.DroneId == droneId)
                    return i;
            }
            return -1;
        }

        public static List<BaseStation> GettingAvailableChageSlots()
        {
            List<BaseStation> AvailableChargeSlotsList = new List<BaseStation>(IndexOfBaseStation+1);
            foreach (BaseStation item in BaseStationsArr)
            {
                if (item.ChargeSlots > 0)
                    AvailableChargeSlotsList.Add(item);
            }
            return AvailableChargeSlotsList;
        }
    }
}

