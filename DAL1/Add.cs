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
            //can add the base station just if the input id still doesn't exist in the Base Station's list.
            int findIndex = BaseStationsList.FindIndex(item => item.Id == baseStation.Id);
            if (findIndex == -1)
                BaseStationsList.Add(baseStation);
            else
                throw new IntIdException(BaseStationsList[findIndex].Id);
        }

        /// <inheritdoc />
        public void Add(Customer customer)
        {
            //can add the customer just if the input id stil doesn't exist in the customers' list.
            int findIndex = CustomersList.FindIndex(item => item.Id == customer.Id);
            if (findIndex == -1)
                CustomersList.Add(customer);
            else
                throw new IntIdException(CustomersList[findIndex].Id);
        }

        /// <inheritdoc />
        public void Add(Drone drone)
        {
            //can add the drone just if the input id stil doesnt exist in the Drones' list.
            int findIndex = DronesList.FindIndex(item => item.Id == drone.Id);
            if (findIndex  == -1)
                DronesList.Add(drone);
            else
                throw new IntIdException(DronesList[findIndex].Id);
           
        }

        /// <inheritdoc />
        public void Add(int droneId, int baseStationId)
        {
            int findIndex = BaseStationsList.FindIndex(item => item.Id == baseStationId);
            if (findIndex  == -1)
                throw new IntIdException(baseStationId);
            int chargeSlots = BaseStationsList.First(item => item.Id == baseStationId).ChargeSlots;
            if (chargeSlots > 0)
            {
                DroneCharge droneCharge = new () { DroneId = droneId, StationId = baseStationId, EntryTime = DateTime.Now };
                DronesChargeList.Add(droneCharge);
            }
            else
                throw new IntChargeSlotsException(chargeSlots);
        }

        /// <inheritdoc />
        public void Add(Parcel parcel)
        {
            int findIndex = ParcelsList.FindIndex(item => item.Id == parcel.Id);
            if (findIndex != -1)
                throw new IntIdException(ParcelsList[findIndex].Id);
            //check if the other ids really exist in the appropriate lists.
            int findIndex1 = CustomersList.FindIndex(item => item.Id == parcel.SenderId);
            int findIndex2 = CustomersList.FindIndex(item => item.Id == parcel.TargetId);
            if ( findIndex1 == -1 )
                throw new StringException(parcel.SenderId);
            if(findIndex2 == -1)
                throw new IntIdException(parcel.TargetId);
            int findIndex3 = DronesList.FindIndex(item => item.Id == parcel.DroneId);
            if (findIndex3  == -1)
                throw new IntIdException(parcel.DroneId);
            ParcelsList.Add(parcel);
        }


    }
}



