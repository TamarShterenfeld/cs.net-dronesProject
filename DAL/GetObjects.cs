using System;
using System.Collections.Generic;
using System.Text;
using static DalObject.DataSource;
using IDAL.DO;
using System.Linq;

namespace DalObject
{
  public partial class DalObject :IDAL.IDal
    {
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
    }
}
