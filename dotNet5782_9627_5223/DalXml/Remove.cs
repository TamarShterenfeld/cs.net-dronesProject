using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DO;
namespace DalXml
{
    sealed partial class DalXml
    {
        public void Remove(DroneCharge drone)
        {
            List<DroneCharge> droneCharges = Dal.XMLTools.LoadListFromXmlSerializer<DroneCharge>(droneChargesPath);
            List<BaseStation> baseStations = Dal.XMLTools.LoadListFromXmlSerializer<BaseStation>(baseStationsPath);
            CheckExistenceOfDroneCharge(drone.DroneId);
            droneCharges.Remove(GetDroneCharge(drone.DroneId));
            BaseStation baseStation = GetBaseStation(drone.StationId);
            baseStation.ChargeSlots++;
            baseStations.Remove(GetBaseStation(baseStation.Id));
            baseStations.Add(baseStation);
        }
    }
}
