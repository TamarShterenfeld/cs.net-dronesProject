using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DO;
using static DalXml.XMLTools;
using System.Runtime.CompilerServices;

namespace DalXml
{
    sealed partial class DalXml
    {
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void Remove(DroneCharge drone)
        {
            List<DroneCharge> droneCharges = LoadListFromXmlSerializer<DroneCharge>(droneChargesPath).ToList();
            List<BaseStation> baseStations = LoadListFromXmlSerializer<BaseStation>(baseStationsPath).ToList();
            CheckExistenceOfDroneCharge(drone.DroneId);
            droneCharges.Remove(GetDroneCharge(drone.DroneId));
            BaseStation baseStation = GetBaseStation(drone.StationId);
            baseStation.ChargeSlots++;
            baseStations.Remove(GetBaseStation(baseStation.Id));
            baseStations.Add(baseStation);
            SaveListToXmlSerializer<BaseStation>(baseStations, baseStationsPath);
        }
    }
}
