using System;
using System.Collections.Generic;
using System.Text;
using static DalObject.DataSource;
using DO;
using System.Linq;
using System.Runtime.CompilerServices;

namespace DalObject
{
  public partial class DalObject
    {

        [MethodImpl(MethodImplOptions.Synchronized)]
        public BaseStation GetBaseStation(int baseStationId)
        {
            CheckExistenceOfBaseStation(baseStationId);
            return BaseStationsList.First(item => item.Id == baseStationId);
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public Drone GetDrone(int droneId)
        {
            CheckExistenceOfDrone(droneId);
            return DronesList.First(item => item.Id == droneId);
        }
     
        [MethodImpl(MethodImplOptions.Synchronized)]
        public  DroneCharge GetDroneCharge(int droneId)
        {
            CheckExistenceOfDroneCharge(droneId);
            return DronesChargeList.First(item => item.DroneId == droneId);
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public Customer GetCustomer(string customerId)
        {
            CheckExistenceOfCustomer(customerId);
            return CustomersList.First(item => item.Id == customerId);
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public Parcel GetParcel(int parcelId)
        {
            CheckExistenceOfParcel(parcelId);
            return ParcelsList.First(item => item.Id == parcelId);
        }
    }
}
