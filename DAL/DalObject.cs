using System;
using System.Collections.Generic;
using System.Text;
using IDAL.DO;
using static IDAL.IDal;
using static DalObject.DataSource;
using static IDAL.DO.OverloadException;

namespace DalObject
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
            Initialize();
        }

        //public int AddBaseStation(string stationName, int positions)
        //{
           
        //}

        public BaseStation GetBaseStation(int baseStationId)
        {
            return BaseStationsList[baseStationId];
        }

        public Drone GetDrone(int droneId)
        {
            return DronesList[droneId];
        }

        public Customer GetCustomer(int customerId)
        {
            return CustomersList[customerId];
        }

        public Parcel GetParcel(int parcelId)
        {
            return ParcelsList[parcelId];

        }

        //public int AddDrone(Drone drone)
        //{
        //    Drone drone = drone;
        //    DronesList.Add(drone);
        //    return DronesList.Count - 1;
        //}

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

        public void PickedupParcel(int parcelId)
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

        public IEnumerable<BaseStation> GetBaseStations()
        {
            return BaseStationsList;
        }

        public IEnumerable<Drone> GetDrones()
        {
            return DronesList;
        }

        public IEnumerable<Customer> GetCustomers()
        {
            return CustomersList;
        }

        public IEnumerable<Parcel> GetParcels()
        {
            return ParcelsList;
        }

        public void SendDroneToRecharge(int droneId, int baseStationId)
        {
            DronesChargeList.Add(new DroneCharge() { DroneId = droneId, StationId = baseStationId, ProductionDate = DateTime.Now });
        }

        public void ReleaseDroneFromRecharge(int droneId)
        {
            DataSource.droneCharges.RemoveAll(dc => dc.DroneId == droneId);
        }

        public IEnumerable<BaseStation> AvailableChargingStations()
        {
            BaseStation[] baseStations = new BaseStation[DataSource.Config.newBaseStationId];
            for (int i = 0; i < DataSource.Config.newBaseStationId; i++)
            {
                baseStations[i] = DataSource.baseStations[i];
                baseStations[i].ChargingPorts -= DataSource.droneCharges.Count(dc => dc.StationId == i);
            }
            return baseStations;
        }

        public int AvailableChargingPorts(int baseStationId)
        {
            return DataSource.BaseStationsList[baseStationId].ChargingPorts - DataSource.droneCharges.Count(dc => dc.StationId == baseStationId);
        }

        public IEnumerable<int> GetDronesIdInBaseStation(int requestedId)
        {
            return DataSource.droneCharges.FindAll(dc => dc.StationId == requestedId).ConvertAll(dc => dc.DroneId);
        }

        public IEnumerable<Parcel> UnAssignmentParcels()
        {
            List<Parcel> parcels = new List<Parcel>();
            for (int i = 0; i < DataSource.Config.newParcelId; i++)
            {
                if (DataSource.parcels[i].DroneId == 0)
                {
                    parcels.Add(DataSource.parcels[i]);
                }
            }
            return parcels;
        }
    }
    //double[] Electricity()
    //    {

    //    }
    }
}


