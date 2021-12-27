using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DO;
using static DalObject.DataSource;

namespace DalObject
{
    public partial class DalObject 
    {
        public void Remove(DroneCharge drone)
        {
            CheckExistenceOfDroneCharge(drone.DroneId);
            DronesChargeList.Remove(GetDroneCharge(drone.DroneId));
            BaseStation baseStation = GetBaseStation(drone.StationId);
            baseStation.ChargeSlots++;
            BaseStationsList.Remove(GetBaseStation(baseStation.Id));
            BaseStationsList.Add(baseStation);
        }
    }
}
