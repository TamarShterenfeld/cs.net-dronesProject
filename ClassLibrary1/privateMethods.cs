using IBL.BO;
using System;

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


        double ComputeBatteryRemained(DroneForList drone, ILocatable locatable)
        {
            return drone.Battery - ComputeMinBatteryNeeded(drone, locatable);
        }

        

        
    }
}
