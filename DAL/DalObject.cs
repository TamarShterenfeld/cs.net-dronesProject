using System;
using System.Collections.Generic;
using System.Text;
using IDal.DO;
using static IDal.IDal;
using static DalObject.DataSource;
using static IDal.DO.OverloadException;
using System.Linq;
using System.Reflection;


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

        public double[] ElectricityConsuming()
        {
           // p in type.GetFields(System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.NonPublic))
            List<double> electricitiesConsuming = new List<double>(5);
            //initalizing a value for comparing the type
            Type type = typeof(Config);
            foreach (var prop in type.GetFields(BindingFlags.Static))
            {
                if (prop != typeof(double))
                {
                    var currValue = prop.GetValue(null);
                    electricitiesConsuming.Add(((double)(currValue)));
                }
            }
            return electricitiesConsuming.ToArray();
        }
        
    }

    
}



