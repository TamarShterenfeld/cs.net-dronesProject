using System;
using System.Collections.Generic;
using System.Text;
using IDal.DO;
using static IDal.IDal;
using static DalObject.DataSource;
using static IDal.DO.OverloadException;
using System.Linq;

namespace DalObject
{
    /// <summary>
    ///the class DalObject contains all the needed methods 
    ///which are connected to the data (in DataSource class) of the program.
    /// </summary>
    public partial class DalObject : IDal.IDal
    {
        // constructor
        public DalObject()
        {
            Initialize();
        }

        

        public BaseStation GetBaseStation(int baseStationId)
        {
            BaseStation baseStation = BaseStationsList.First(item => item.Id == baseStationId);
            return baseStation;
        }

        public Drone GetDrone(int droneId)
        {
            Drone drone = DronesList.First(item => item.Id == droneId);
            return drone;
        }

        public Customer GetCustomer(string customerId)
        {
            Customer customer = CustomersList.First(item => item.Id == customerId);
            return customer;
        }

        

        public Parcel GetParcel(int parcelId)
        {
            Parcel parcel = ParcelsList.First(item => item.Id == parcelId);
            return parcel;
        }

        public void AssociateParcel(int parcelId, int droneId)
        {
            for (int i = 0; i < ParcelsList.Count; i++)
            {
                if (ParcelsList[i].Id == parcelId)
                {
                    Parcel parcel = ParcelsList[i];
                    parcel.DroneId = droneId;
                    ParcelsList[i] = parcel;
                    break;
                }
            }
        }

        public void PickupParcel(int parcelId)
        {
            for (int i = 0; i < ParcelsList.Count; i++)
            {
                if (ParcelsList[i].Id == parcelId)
                {
                    Parcel parcel = ParcelsList[i];
                    parcel.PickUpDate = DateTime.Now;
                    ParcelsList[i] = parcel;
                    break;
                }
            }
        }

        public IEnumerable<BaseStation> GetBaseStationsList()
        {
            return BaseStationsList;
        }

        public IEnumerable<Drone> GetDronesList()
        {
            return DronesList;
        }

        public IEnumerable<Customer> GetCustomersList()
        {
            return CustomersList;
        }

        public IEnumerable<Parcel> GetParcelsList()
        {
            return ParcelsList;
        }

        public IEnumerable<DroneCharge> GetDronesCharge()
        {
            return DronesChargeList;
        }
        public void SendDroneToRecharge(int droneId, int baseStationId)
        {
            DronesChargeList.Add(new DroneCharge() { DroneId = droneId, StationId = baseStationId, EntryTime = DateTime.Now });
        }

        public void ReleaseDroneFromRecharge(int droneId)
        {
            DronesChargeList.RemoveAll(dc => dc.DroneId == droneId);
        }

        public IEnumerable<BaseStation> AvailableChargingStations()
        {
            List<BaseStation> availableChargingSlotsList = new List<BaseStation>();
            for (int i = 0; i < BaseStationsList.Count; i++)
            {
                availableChargingSlotsList[i] = BaseStationsList[i];
                availableChargingSlotsList[i].ChargeSlots -= (DronesChargeList.ToArray()).Count(dc => dc.StationId == availableChargingSlotsList[i].Id);
            }
            return availableChargingSlotsList;
        }

        public static int AvailableChargingSlots(int baseStationId)
        {
            int caught = 0;
            foreach (DroneCharge droneCharge in DronesChargeList)
            {
                if(droneCharge.StationId == baseStationId)
                {
                    ++caught;
                }
            }
            return caught;
        }

        public static IEnumerable<DroneCharge> DronesChargingInMe(int stationId)
        {
            List<DroneCharge> dronesCharge = null;
            foreach (DroneCharge droneCharge in DronesChargeList)
            {
                if (droneCharge.StationId == stationId)
                {
                    dronesCharge.Add(droneCharge);
                }
            }
            return dronesCharge;
        }

        public IEnumerable<int> GetDronesIdInBaseStation(int requestedId)
        {
            return DronesChargeList.FindAll(dc => dc.StationId == requestedId).ConvertAll(dc => dc.DroneId);
        }

        public IEnumerable<Parcel> NotAssociatedParcels()
        {
            List<Parcel> parcels = new List<Parcel>();
            for (int i = 0; i < ParcelsList.Count; i++)
            {
                if (ParcelsList[i].DroneId == -1)
                {
                    parcels.Add(ParcelsList[i]);
                }
            }
            return parcels;
        }
        public static int increaseParcelIndex()
        {
            return ++DataSource.Config.ParcelId;
        }
    }
    
}



