using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.DO;
using IDal.DO;
using static DalObject.DataSource;

namespace DalObject
{
    public partial class DalObject : IDal.IDal
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
