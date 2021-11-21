using System;
using System.Collections.Generic;
using System.Text;
using static DalObject.DataSource;
using IDal.DO;
using System.Reflection;

namespace DalObject
{
    public partial class DalObject : IDal.IDal
    {
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

        public double[] ElectricityConsuming()
        {
            // p in type.GetFields(System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.NonPublic))
            List<double> electricitiesConsuming = new List<double>(5);
            //initalizing a value for comparing the type
            Type type = typeof(Config);
            foreach (var prop in type.GetFields(BindingFlags.NonPublic))
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
