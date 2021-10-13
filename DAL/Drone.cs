using System;
using System.Collections.Generic;
using System.Text;

namespace IDAL
{
    namespace DO
    {
       
        public partial class IDAL
        {
            /// <summary>
            /// the struct Drone contains all the needed details which are connected to a drone.
            /// </summary>
            public struct Drone
            {
                int id;
                double battery;
                public int Id
                {
                    set
                    {
                        if (value < 0)
                        {
                            throw new FormatException("Id must contain a positive number");
                        }
                        id = value;
                    }
                    get { return id; }
                }

                public string Model { get; set; }
                public double Battery
                {
                    get { return battery; }
                    set
                    {
                        if (value < 0)
                            throw new FormatException("Battery must hold a positive value.");
                        if (value > 100)
                            throw new FormatException("Battery can't hold a value more than 100% of charge.");
                        battery = value;
                    }
                }

                public DO.DroneStatuses Status { set; get; }
                public DO.WeightCategories MaxWeight { set; get; }
            }
        }
    }
}

