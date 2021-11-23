using System;
using System.Collections.Generic;
using System.Text;
using static IDal.DO.OverloadException;
using IDal.DO;

namespace IDal
{
    namespace DO
    {
        /// <summary>
        /// the struct Drone contains the following details: station id, drone id.
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
                        throw new OverloadException("Id must hold a positive number.");
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
                        throw new OverloadException("Id must hold a positive number.");
                    }
                    droneId = value;
                }
            }

            public DateTime EntryTime
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
                    $"base station id:{StationId}";
            }
        }

    }
}
