using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using DO;
using static DalXml.XMLTools;
using System.Xml.Linq;
using System.Linq;
using System.Runtime.CompilerServices;

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
            int parcelId = RescueConfigValueByName<int>("ParcelId");
            ++parcelId;
            XElement parcelIdXElement =
                 (from c in ConfigRoot.Elements()
                  where c.Name == "ParcelId"
                  select c ).FirstOrDefault();
            XElement temp = new XElement("ParcelId", parcelId);
            parcelIdXElement.Remove();
            parcelIdXElement = temp;
            ConfigRoot.Add(parcelIdXElement);
            ConfigRoot.Save(dirPath+ConfigPath);
            return parcelId;
        }


        [MethodImpl(MethodImplOptions.Synchronized)]
        public int CaughtChargeSlots(int baseStationId)
        {
            List<BaseStation> baseStations = LoadListFromXmlSerializer<DO.BaseStation>(baseStationsPath).ToList();
            return ((List<int>)GetDronesIdInBaseStation(baseStationId)).Count;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public double[] BatteryUsages()
        {
            double[] electricitiesConsuming = new double[5];
            electricitiesConsuming[0] = RescueConfigValueByName<double>("ElectricityConsumingOfAvailable");
            electricitiesConsuming[1] = RescueConfigValueByName<double>("ElectricityConsumingOfLightWeight");
            electricitiesConsuming[2] = RescueConfigValueByName<double>("ElectricityConsumingOfAverageWeight");
            electricitiesConsuming[3] = RescueConfigValueByName<double>("ElectricityConsumingOfHeavyWeight"); 
            electricitiesConsuming[4] = RescueConfigValueByName<double>("ChargeRate");
            return electricitiesConsuming;
        }

        static T RescueConfigValueByName<T>(string name)
        {
            XElement ans =
                (from c in ConfigRoot.Elements()
                 where c.Name == name
                 select c).FirstOrDefault();
            if (ans.Value != null)
            {
                Type type = typeof(T);

                T value = default(T);
                var methodInfo = (from m in type.GetMethods(BindingFlags.Public | BindingFlags.Static)
                                  where m.Name == "TryParse"
                                  select m).FirstOrDefault();

                if (methodInfo == null)
                    throw new ApplicationException("Unable to find TryParse method!");

                object result = methodInfo.Invoke(null, new object[] { ans.Value, value });
                if ((result != null) && ((bool)result))
                {
                    methodInfo = (from m in type.GetMethods(BindingFlags.Public | BindingFlags.Static)
                                  where m.Name == "Parse"
                                  select m).FirstOrDefault();

                    if (methodInfo == null)
                        throw new ApplicationException("Unable to find Parse method!");

                    value = (T)methodInfo.Invoke(null, new object[] { ans.Value });

                    return (T)value;
                }
            }

            return default(T);
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public int GetLastParcelId()
        {
            XElement parcelId = (from c in ConfigRoot.Elements()
                            where c.Name == "ParcelId"
                            select c ).FirstOrDefault();
            return int.Parse(parcelId.Value);
        }


        /// <summary>
        /// <param name="droneId"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public int GetDroneChargeBaseStationId(int droneId)
        {
            CheckExistenceOfDroneCharge(droneId);
            List<DroneCharge> droneCharges = LoadListFromXmlSerializer<DroneCharge>(droneChargesPath).ToList();
            return droneCharges.Find(dc => dc.DroneId == droneId).StationId;
        }

    }
}
