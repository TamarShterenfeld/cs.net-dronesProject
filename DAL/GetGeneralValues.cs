using System;
using System.Collections.Generic;
using System.Text;
using static DalObject.DataSource;
using IDal.DO;

namespace DalObject
{
    public partial class DalObject : IDal.IDal
    {
        public int AvailableChargeSlots(int baseStationId)
        {
            int caught = 0;
            foreach (DroneCharge droneCharge in DronesChargeList)
            {
                if (droneCharge.StationId == baseStationId)
                {
                    ++caught;
                }
            }
            return caught;
        }

    }
}
