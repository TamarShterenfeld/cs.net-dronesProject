using System;
using System.Collections.Generic;
using System.Text;
using static DalObject.DataSource;
using System.Reflection;
using System.Runtime.CompilerServices;
using DO;

namespace DalObject
{
    public partial class DalObject
    {
        /// <summary>
        ///a static method which increases the static field - 'ParcelId' in each time it is called. 
        /// </summary>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public static int IncreaseParcelIndex()
        { 
            return ++Config.ParcelId;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public int CaughtChargeSlots(int baseStationId)
        {
            return ((List<int>)GetDronesIdInBaseStation(baseStationId)).Count;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public int GetLastParcelId()
        {
            return Config.ParcelId;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public int GetDroneChargeBaseStationId(int droneId)
        {
                CheckExistenceOfDroneCharge(droneId);
                return DronesChargeList.Find(dc => dc.DroneId == droneId).StationId;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public double[] BatteryUsages() => new[]
      {
            Config.ElectricityConsumingOfAvailable,
            Config.ElectricityConsumingOfLightWeight,
            Config.ElectricityConsumingOfAverageWeight,
            Config.ElectricityConsumingOfHeavyWeight,
            Config.ChargeRate
       };
    }
}
