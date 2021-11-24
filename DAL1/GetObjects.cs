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
        /// <inheritdoc />
        public BaseStation GetBaseStation(int baseStationId)
        {
            BaseStation baseStation = BaseStationsList.First(item => item.Id == baseStationId);
            return baseStation;
        }

        /// <inheritdoc />
        public Drone GetDrone(int droneId)
        {
            Drone drone = DronesList.First(item => item.Id == droneId);
            return drone;
        }

        /// <inheritdoc />
        public Customer GetCustomer(string customerId)
        {
            Customer customer = CustomersList.First(item => item.Id == customerId);
            return customer;
        }


        /// <inheritdoc />
        public Parcel GetParcel(int parcelId)
        {
            Parcel parcel = ParcelsList.First(item => item.Id == parcelId);
            return parcel;
        }
    }
}
