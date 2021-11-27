using System;
using System.Collections.Generic;
using System.Text;
using static DalObject.DataSource;
using IDal.DO;
using System.Linq;
using DAL.DO;
using IBL.BO;

namespace IBL
{
    public partial class BL : IBL
    {

        public static int GetParcelIndex()
        {
            return DalObject.DalObject.IncreaseParcelIndex();
        }

     
        bool DroneReachLocation(DroneForList drone, ILocatable locatable)
        {
            return drone.Battery - ComputeMinBatteryNeeded(drone, locatable) >= 0;
        }


        double ComputeBatteryRemaining(DroneForList drone, ILocatable locatable)
        {
            return drone.Battery - ComputeMinBatteryNeeded(drone, locatable);
        }
    }
}
