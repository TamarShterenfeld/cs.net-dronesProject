using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DO;
using static DalObject.DataSource;
using System.Runtime.CompilerServices;

namespace DalObject
{
    public partial class DalObject 
    {
        [MethodImpl(MethodImplOptions.Synchronized)]
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
