using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DO;
using static DalXml.XMLTools;

namespace DalXml
{
    public sealed partial class DalXml
    {
        public void Remove(DroneCharge drone)
        {
            List<DroneCharge> droneCharges = LoadListFromXmlSerializer<DroneCharge>(droneChargesPath);
            List<BaseStation> baseStations = LoadListFromXmlSerializer<BaseStation>(baseStationsPath);
            CheckExistenceOfDroneCharge(drone.DroneId);
            droneCharges.Remove(GetDroneCharge(drone.DroneId));
            BaseStation baseStation = GetBaseStation(drone.StationId);
            baseStation.ChargeSlots++;
            baseStations.Remove(GetBaseStation(baseStation.Id));
            baseStations.Add(baseStation);
        }
    }
}
