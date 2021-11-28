using System;
using System.Collections.Generic;
using System.Text;
using IDal.DO;
using static IDal.IDal;
using static DalObject.DataSource;
using System.Linq;
using DAL.DO;

namespace DalObject
{
    /// <summary>
    ///the class DalObject contains all the needed methods 
    ///which are connected to the data (in DataSource class) of the program.
    /// </summary>
    public partial class DalObject : IDal.IDal
    {
        /// <inheritdoc />
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

        public void Add(int droneId, int baseStationId)
        {
            CheckExistenceOfBaseStation(baseStationId);
            int chargeSlots = BaseStationsList.First(item => item.Id == baseStationId).ChargeSlots;
            if (chargeSlots > 0)
            {
                CheckExistenceOfDroneCharge(droneId);
                DroneCharge droneCharge = new() { DroneId = droneId, StationId = baseStationId, EntryTime = DateTime.Now };
                Add(droneCharge);
            }
            else
                throw new ChargeSlotsException(chargeSlots);
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
            BaseStation baseStation = GetBaseStation(droneCharge.StationId);
            baseStation.ChargeSlots--;
            UpDate(baseStation, baseStation.Id);
            DronesChargeList.Add(droneCharge);
        }
    }
}



