using System;
using System.Collections.Generic;
using System.Text;
using static DalObject.DataSource;
using System.Linq;
using DO;

namespace DalObject
{
    /// <summary>
    ///the class DalObject contains all the needed methods 
    ///which are connected to the data (in DataSource class) of the program.
    /// </summary>
    public partial class DalObject
    {
        public void Add(BaseStation baseStation)
        {
            CheckNotExistenceOfBaseStation(baseStation.Id);
            BaseStationsList.Add(baseStation);
        }

        public void Add(Customer customer)
        {
            CheckNotExistenceOfCustomer(customer.Id);
            CustomersList.Add(customer);
        }


        public void Add(Drone drone)
        {
            CheckNotExistenceOfDrone(drone.Id);
            DronesList.Add(drone);
        }

        public void Add(Parcel parcel)
        {
            CheckNotExistenceOfParcel(parcel.Id);
            //check if the other ids really exist in the appropriate lists.
            CheckExistenceOfCustomer(parcel.SenderId);
            CheckExistenceOfCustomer(parcel.TargetId);
            if (parcel.DroneId != 0)
            { CheckExistenceOfDrone(parcel.DroneId); }
            ParcelsList.Add(parcel);
        }

        public void Add(DroneCharge droneCharge)
        {
            CheckNotExistenceOfDroneCharge(droneCharge.DroneId);
            CheckExistenceOfDrone(droneCharge.DroneId);
            BaseStation baseStation = GetBaseStation(droneCharge.StationId);
            baseStation.ChargeSlots--;
            UpDate(baseStation, baseStation.Id);
            DronesChargeList.Add(droneCharge);
        }
    }
}



