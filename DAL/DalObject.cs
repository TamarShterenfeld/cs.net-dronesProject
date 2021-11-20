using System;
using System.Collections.Generic;
using System.Text;
using IDal.DO;
using static IDal.IDal;
using static DalObject.DataSource;
using static IDal.DO.OverloadException;
using System.Linq;

namespace DalObject
{
    /// <summary>
    ///the class DalObject contains all the needed methods 
    ///which are connected to the data (in DataSource class) of the program.
    /// </summary>
    public partial class DalObject : IDal.IDal
    {
        // constructor
        public DalObject()
        {
            Initialize();
        }
        public IEnumerable<BaseStation> AvailableChargingStations()
        {
            List<BaseStation> availableChargingSlotsList = new List<BaseStation>();
            for (int i = 0; i < BaseStationsList.Count; i++)
            {
                availableChargingSlotsList[i] = BaseStationsList[i];
                availableChargingSlotsList[i].ChargeSlots -= (DronesChargeList.ToArray()).Count(dc => dc.StationId == availableChargingSlotsList[i].Id);
            }
            return availableChargingSlotsList;
        }

        public static int AvailableChargingSlots(int baseStationId)
        {
            int caught = 0;
            foreach (DroneCharge droneCharge in DronesChargeList)
            {
                if(droneCharge.StationId == baseStationId)
                {
                    ++caught;
                }
            }
            return caught;
        }

        public int IncreaseParcelIndex()
        {
            return ++DataSource.Config.ParcelId;
        }
    }
    
}



