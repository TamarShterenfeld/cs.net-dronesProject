using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL.PO
{
    public static partial class POConverter
    {
        //-----------------------Station Converting----------------------
        public static BO.BaseStation StationPoToBo(PO.Station station)
        {
            return new BO.BaseStation()
            {
                Id = station.Id,
                Name = station.Name,
                Location = LocationPOTOBO(station.Location),
                ChargeSlots = station.ChargeSlots,
                DroneCharging = DroneInChargingListPoToBo(station.DroneCharging).ToList()
            };
        }

    }
}
