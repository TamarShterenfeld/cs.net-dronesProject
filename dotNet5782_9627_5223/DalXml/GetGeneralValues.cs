using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using DO;

namespace DalXml
{
    sealed partial class DalXml
    {


        /// <summary>
        ///a static method which increases the static field - 'ParcelId' in each time it is called. 
        /// </summary>
        /// <returns></returns>
        public static int IncreaseParcelIndex()
        {
            return 0;
        }

        public int CaughtChargeSlots(int baseStationId)
        {
            List<BaseStation> baseStations = Dal.XMLTools.LoadListFromXmlSerializer<DO.BaseStation>(baseStationsPath);
            return ((List<int>)GetDronesIdInBaseStation(baseStationId)).Count;
        }

        public double[] ElectricityConsuming()
        {
            //List<Config> baseStations = Dal.XMLTools.LoadListFromXmlSerializer<DO.BaseStation>(baseStationsPath);
            //const int DOUBLE_VARIABLES_IN_CONFIG_CLASS = 5;
            ////copies all the needed values from Config class.
            //double[] electricitiesConsuming = new double[DOUBLE_VARIABLES_IN_CONFIG_CLASS];
            //electricitiesConsuming[0] = Config.ElectricityConsumingOfAvailable;
            //electricitiesConsuming[1] = Config.ElectricityConsumingOfLightWeight;
            //electricitiesConsuming[2] = Config.ElectricityConsumingOfAverageWeight;
            //electricitiesConsuming[3] = Config.ElectricityConsumingOfHeavyWeight;
            //electricitiesConsuming[4] = Config.ChargeRate;
            //return electricitiesConsuming;
            return new double[1];
        }

        public int GetLastParcelId()
        {
            // return Config.ParcelId;
            return 0;
        }
    }
}
