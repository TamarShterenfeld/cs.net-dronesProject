using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Reflection;
using Singleton;
using System.Runtime.CompilerServices;
using DO;
using static DalXml.XMLTools;

namespace DalXml
{
    /// <summary>
    ///the class DalXml contains all the needed methods 
    ///which are connected to the data (in xml files) of the program.
    /// </summary>
    sealed partial class DalXml : Singleton<DalXml>, DalApi.IDal
    {
        private readonly string baseStationsPath;
        private readonly string dronesPath;
        private readonly string parcelsPath;
        private readonly string customersPath;
        private readonly string droneChargesPath;

        // a constructor
        DalXml()
        {
            baseStationsPath = "BaseStations.xml";
            dronesPath = "Drones.xml";
            parcelsPath = "Parcels.xml";
            customersPath = "Customers.xml";
            droneChargesPath = "DroneCharges.xml";
        Initialize();
    }
     
    }
}