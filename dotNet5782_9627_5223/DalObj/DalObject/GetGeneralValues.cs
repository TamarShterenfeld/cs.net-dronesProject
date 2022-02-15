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
        public double[] ElectricityConsuming()
        {
            const int DOUBLE_VARIABLES_IN_CONFIG_CLASS = 5;
            //copies all the needed values from Config class.
            double[] electricitiesConsuming = new double[DOUBLE_VARIABLES_IN_CONFIG_CLASS];
            electricitiesConsuming[0] = Config.ElectricityConsumingOfAvailable;
            electricitiesConsuming[1] = Config.ElectricityConsumingOfLightWeight;
            electricitiesConsuming[2] = Config.ElectricityConsumingOfAverageWeight;
            electricitiesConsuming[3] = Config.ElectricityConsumingOfHeavyWeight;
            electricitiesConsuming[4] = Config.ChargeRate;
            return electricitiesConsuming;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public int GetLastParcelId()
        {
            return Config.ParcelId;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public int GetDroneChargeBaseStationId(int droneId)
        {
            try
            {
                CheckExistenceOfDroneCharge(droneId);
                return DronesChargeList.Find(dc => dc.DroneId == droneId).StationId;
            }
            catch (IntIdException ex) { throw new IntIdException(ex.Data.ToString()); }
        }
    }
}
