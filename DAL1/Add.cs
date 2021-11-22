using System;
using System.Collections.Generic;
using System.Text;
using IDal.DO;
using static IDal.IDal;
using static DalObject.DataSource;
using System.Linq;

namespace DalObject
{
    /// <summary>
    ///the class DalObject contains all the needed methods 
    ///which are connected to the data (in DataSource class) of the program.
    /// </summary>
    public partial class DalObject : IDal.IDal
    {
        public void Add(BaseStation baseStation)
        {
            //can add the base station just if the input id still doesn't exist in the Base Station's list.
            if (BaseStationsList.FindIndex(item => item.Id == baseStation.Id) == -1)
            {
                BaseStationsList.Add(baseStation);
            }
            else
            {
                throw new OverloadException("Id already exists in base station list, it's not possible to add it!");//לטפל בהערות
            }
        }
        public void Add(Customer customer)
        {
            //can add the customer just if the input id stil doesn't exist in the customers' list.
            if (CustomersList.FindIndex(item => item.Id == customer.Id) == -1)
            {
                CustomersList.Add(customer);
            }
            else
            {
                throw new OverloadException("Id already exists in drones list, it's not possible to add it!");//לטפל בזריקות
            }
        }
        public void Add(Drone drone)
        {
            //can add the drone just if the input id stil doesnt exist in the Drones' list.
            if (DronesList.FindIndex(item => item.Id == drone.Id) == -1)
            {
                DronesList.Add(drone);
            }
            else
            {
                throw new OverloadException("Id already exists in drones list, it's not possible to add it!");//לטפל בהערות
            }
           
        }

        public void Add(int droneId, int baseStationId)
        {
            if (BaseStationsList.FindIndex(item => item.Id == baseStationId) == -1)
                throw new OverloadException("baseStation Id doesn't exist!");
            if( BaseStationsList.First(item => item.Id == baseStationId).ChargeSlots > 0)
            {
                DroneCharge droneCharge = new DroneCharge { DroneId = droneId, StationId = baseStationId, EntryTime = DateTime.Now };
                DronesChargeList.Add(droneCharge);
            }
            else
            {
                throw new OverloadException("the chosen baseStation doesn't have enough chargeSlots for charging this drone.");
            }
        }
        public void Add(Parcel parcel)
        {
            if(ParcelsList.FindIndex(item => item.Id == parcel.Id) != -1)
                throw new OverloadException("You try to add a parcel which is already exists!");
            //check if the other ids really exist in the appropriate lists.
            if (CustomersList.FindIndex(item => item.Id == parcel.SenderId) == -1 || CustomersList.FindIndex(item => item.Id == parcel.TargetId) == -1)
                throw new OverloadException("sender's id or target's id don't exist in the customers' list.");
            if (DronesList.FindIndex(item => item.Id == parcel.DroneId) == -1)
                throw new OverloadException("drone's id doesn't exist in the drones' List.");
            ParcelsList.Add(parcel);
        }


    }
}



