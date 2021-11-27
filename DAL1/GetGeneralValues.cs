using System;
using System.Collections.Generic;
using System.Text;
using static DalObject.DataSource;
using IDal.DO;
using System.Reflection;

namespace DalObject
{
    /// <inheritdoc />
    public partial class DalObject : IDal.IDal
    {


        /// <summary>
        ///a static method which increases the static field - 'ParcelId' in each time it is called. 
        /// </summary>
        /// <returns></returns>
        public static int IncreaseParcelIndex()
        {
            return ++Config.ParcelId;
        }

        /// <inheritdoc /> 
        public int AvailableChargeSlots(int baseStationId)
        {
            int caught = 0;
            foreach (DroneCharge droneCharge in DronesChargeList)
            {
                if (droneCharge.StationId == baseStationId)
                {
                    ++caught;
                }
            }
            return caught;
        }

        /// <inheritdoc />
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
    }
}
