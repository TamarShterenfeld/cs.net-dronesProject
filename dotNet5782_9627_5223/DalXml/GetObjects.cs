using System;
using System.Collections.Generic;
using System.Text;
using DO;
using System.Linq;

namespace DalXml
{
  public partial class DalXml
    {

        public BaseStation GetBaseStation(int baseStationId)
        {
            return new BaseStation();
        }


        public Drone GetDrone(int droneId)
        {
            return new Drone();
        }

        public  DroneCharge GetDroneCharge(int droneId)
        {
            return new DroneCharge();
        }


        public Customer GetCustomer(string customerId)
        {
            return new Customer();
        }


        public Parcel GetParcel(int parcelId)
        {
            return new Parcel();
        }
    }
}
