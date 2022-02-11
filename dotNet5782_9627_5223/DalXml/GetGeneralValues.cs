using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using DO;
using static DalXml.XMLTools;
using System.Xml.Linq;
using System.Linq;

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
            int parcelId = (from c in ConfigRoot.Elements()
                                         select int.Parse(c.Element("ParcelId").Value)).FirstOrDefault();
            XElement parcelIdXElement =
                 (from c in ConfigRoot.Elements()
                 select c.Element("ParcelId")).FirstOrDefault();
            parcelIdXElement.Element("parcelId").Value = parcelId + 1.ToString();
            ConfigRoot.Save(ConfigPath);
            return ++parcelId;
        }

        public int CaughtChargeSlots(int baseStationId)
        {
            List<BaseStation> baseStations =  LoadListFromXmlSerializer<DO.BaseStation>(baseStationsPath);
            return ((List<int>)GetDronesIdInBaseStation(baseStationId)).Count;
        }

        public double[] ElectricityConsuming()

        {
            double[] electricitiesConsuming = new double[5];
            electricitiesConsuming[0] = 
                (from c in ConfigRoot.Elements()
                select double.Parse(c.Element("ElectricityConsumingOfAvailable").Value)).FirstOrDefault();
            electricitiesConsuming[1] =
               (from c in ConfigRoot.Elements()
                select double.Parse(c.Element("ElectricityConsumingOfLightWeight").Value)).FirstOrDefault();
            electricitiesConsuming[2] =
               (from c in ConfigRoot.Elements()
                select double.Parse(c.Element("ElectricityConsumingOfAverageWeight").Value)).FirstOrDefault();
            electricitiesConsuming[3] =
               (from c in ConfigRoot.Elements()
                select double.Parse(c.Element("ElectricityConsumingOfHeavyWeight").Value)).FirstOrDefault();
            electricitiesConsuming[4] =
               (from c in ConfigRoot.Elements()
                select double.Parse(c.Element("electricitiesConsuming").Value)).FirstOrDefault();

            return electricitiesConsuming;
        }

        public int GetLastParcelId()
        {
            int parcelId = (from c in ConfigRoot.Elements()
                            select int.Parse(c.Element("ParcelId").Value)).FirstOrDefault();
            return parcelId;
        }
    }
}
