using System;
using System.Collections.Generic;
using System.Text;
using static DalObject.DataSource;
using IDal.DO;
using System.Linq;

namespace DalObject
{
  public partial class DalObject :IDal.IDal
    {

        public BaseStation GetBaseStation(int baseStationId)
        {
            CheckExistenceOfBaseStation(baseStationId);
            BaseStation baseStation = BaseStationsList.First(item => item.Id == baseStationId);
            return baseStation;
        }


        public Drone GetDrone(int droneId)
        {
            CheckExistenceOfDrone(droneId);
            Drone drone = DronesList.First(item => item.Id == droneId);
            return drone;
        }

        static public DroneCharge GetDroneCharge(int droneId)
        {
            CheckExistenceOfDroneCharge(droneId);
            DroneCharge droneCharge = DronesChargeList.First(item => item.DroneId == droneId);
            return droneCharge;
        }


        public Customer GetCustomer(string customerId)
        {
            CheckExistenceOfCustomer(customerId);
            Customer customer = CustomersList.First(item => item.Id == customerId);
            return customer;
        }


        public Parcel GetParcel(int parcelId)
        {
            CheckExistenceOfParcel(parcelId);
            Parcel parcel = ParcelsList.First(item => item.Id == parcelId);
            return parcel;
        }
    }
}
