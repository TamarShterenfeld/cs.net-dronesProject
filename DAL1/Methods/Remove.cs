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
            DronesChargeList.Remove(DronesChargeList.First(item => item.DroneId == drone.DroneId));
            BaseStation baseStation = BaseStationsList.First(item => item.Id == drone.StationId);
            baseStation.ChargeSlots++;
            BaseStationsList.Remove(BaseStationsList.First(item => item.Id == drone.StationId));
            BaseStationsList.Add(baseStation);
        }
    }
}
