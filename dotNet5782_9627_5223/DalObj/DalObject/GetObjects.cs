using System;
using System.Collections.Generic;
using System.Text;
using static DalObject.DataSource;
using DO;
using System.Linq;

namespace DalObject
{
  public partial class DalObject
    {

        public BaseStation GetBaseStation(int baseStationId)
        {
            CheckExistenceOfBaseStation(baseStationId);
            return BaseStationsList.First(item => item.Id == baseStationId);
        }


        public Drone GetDrone(int droneId)
        {
            CheckExistenceOfDrone(droneId);
            return DronesList.First(item => item.Id == droneId);
        }

        public  DroneCharge GetDroneCharge(int droneId)
        {
            CheckExistenceOfDroneCharge(droneId);
            return DronesChargeList.First(item => item.DroneId == droneId);
        }


        public Customer GetCustomer(string customerId)
        {
            CheckExistenceOfCustomer(customerId);
            return CustomersList.First(item => item.Id == customerId);
        }


        public Parcel GetParcel(int parcelId)
        {
            CheckExistenceOfParcel(parcelId);
            return ParcelsList.First(item => item.Id == parcelId);
        }
    }
}
