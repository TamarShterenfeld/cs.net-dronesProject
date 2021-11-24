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
            // p in type.GetFields(System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.NonPublic))
            List<object> electricitiesConsuming = new (5);
            //initalizing a value for comparing the type
            Type type = typeof(Config);
            foreach (var prop in type.GetFields(BindingFlags.Static | BindingFlags.Public))
            {
                if (prop != typeof(double))
                {
                    var currValue =  prop.GetValue(null);
                    
                    electricitiesConsuming.Add(currValue);
                }
            }
            List<double> electricities = electricitiesConsuming.ConvertAll(item => (double)item);
            return electricities.ToArray();
        }
    }
}
