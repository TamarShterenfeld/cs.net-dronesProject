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

        public  List<Drone> GettingDronesList()
        {
            List<Drone> dronesList = new List<Drone>(IndexOfDrone);
            foreach (Drone item in DronesArr)
            {
                dronesList.Add(item);
            }
            return dronesList;
        }

        public  List<BaseStation> GettingBaseStationList()
        {
            List<BaseStation> baseStationsList = new List<BaseStation>(IndexOfBaseStation);
            foreach (BaseStation item in BaseStationsArr)
            {
                baseStationsList.Add(item);
            }
            return baseStationsList;
        }

        public  List<Customer> GettingCustomerList()
        {
            List<Customer> customersList = new List<Customer>(IndexOfCustomer);
            foreach (Customer item in CustomersArr)
            {
                customersList.Add(item);
            }
            return customersList;
        }

        public  List<Parcel> GettingParcelList()
        {
            List<Parcel> parcelsList = new List<Parcel>(IndexOfParcel);
            foreach (Parcel item in ParcelsArr)
            {
                parcelsList.Add(item);
            }
            return parcelsList;
        }

        public  List<Parcel> GettingNotAssociatedParcels()
        {
            List<Parcel> notAssociatedDronesList = new List<Parcel>(IndexOfParcel - DroneChargeList.Count);
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

        private  int searchDronesChargeList(int droneId)
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

        public  List<BaseStation> GettingAvailableChageSlots()
        {
            List<BaseStation> AvailableChargeSlotsList = new List<BaseStation>(IndexOfBaseStation);
            foreach (BaseStation item in BaseStationsArr)
            {
                if (item.ChargeSlots > 0)
                    AvailableChargeSlotsList.Add(item);
            }
            return AvailableChargeSlotsList;
        }
    }
}

