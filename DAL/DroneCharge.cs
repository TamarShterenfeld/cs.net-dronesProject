using System;
using System.Collections.Generic;
using System.Text;
using static IDAL.DO.OverloadException;
using IDAL.DO;

namespace IDAL
{
    namespace DO
    {
            /// <summary>
            /// the struct Drone contains the following details: station id, drone id.
            /// </summary>
            public class DroneCharge
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

                /// <summary>
                /// a constructor with parameters
                /// </summary>
                /// <param name="stationId">modify stationId</param>
                /// <param name="droneId">modify droneId</param>
                public DroneCharge(int stationId, int droneId)
                {
                    this.stationId = stationId; this.droneId = droneId;
                    StationId = stationId; DroneId = droneId;
                }

                /// <summary>
                /// override ToString function.
                /// </summary>
                /// <returns></returns>
                public override string ToString()
                {
                    return $"drone id:{DroneId }\n"+
                        $"base station id:{StationId}";
                }
            }
        
    }
}
