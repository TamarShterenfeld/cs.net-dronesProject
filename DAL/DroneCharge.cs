﻿using System;
using System.Collections.Generic;
using System.Text;

namespace IDAL
{
    namespace DO
    {
        public partial class IDAL
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
                            throw new FormatException("Id must hold a positive number.");
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
                            throw new FormatException("Id must hold a positive number.");
                        }
                        droneId = value;
                    }
                }

                public DroneCharge(int stationId, int droneId)
                {
                    this.stationId = stationId; this.droneId = droneId;
                    StationId = stationId; DroneId = droneId;
                }

                public override string ToString()
                {
                    return "This is an object of drone charge, its drone's id: "+DroneId +", base station's id: "+StationId;
                }
            }
        }
    }
}
