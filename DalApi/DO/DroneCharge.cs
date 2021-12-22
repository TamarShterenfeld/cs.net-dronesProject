using System;
using System.Collections.Generic;
using System.Text;

namespace DO
{
    /// <summary>
    /// the struct Drone contains the following details: station id, drone id.
    /// /// actually, these are all the basic details for creating a droneCharge.
    /// </summary>
    public struct DroneCharge
    {
        int stationId;
        int droneId;

        public int StationId
        {
            get { return stationId; }
            set
            {
                if (value < 0)
                {
                    throw new IntIdException(value);
                }
                stationId = value;
            }
        }

        public int DroneId
        {
            get { return droneId; }
            set
            {
                if (value < 0)
                {
                    throw new IntIdException(value);
                }
                droneId = value;
            }
        }

        public DateTime? EntryTime
        {
            set; get;
        }
        /// <summary>
        /// override ToString function.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"drone id:{DroneId }\n" +
                    $"base station id:{StationId}\n"+
                    $"entryTime: {EntryTime}\n";

        }
    }

}

