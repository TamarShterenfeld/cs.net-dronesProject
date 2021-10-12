using System;
using System.Collections.Generic;
using System.Text;

namespace IDAL
{
    namespace DO
    {
        enum WeightCategories
        {
            Light = 1, Average, Heavy
        }

        enum DroneStatuses
        {
            Available = 1, Maintenance, Shipment
        }
        public partial class IDAL
        {
            /// <summary>
            /// the struct Drone contains all the needed details which are connected to a drone.
            /// </summary>
            public struct Drone
            {
                string id;
                double battery;

                public string Id
                {
                    set
                    {
                        //check validation of the inputed id number.
                        if (value.Length != 9)
                            throw new FormatException("Id number must contain exactly nine digits.");
                        foreach (char item in value)
                        {
                            if (Char.IsNumber(item))
                                throw new FormatException("Id can contain only numbers.");
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
                            throw new FormatException("battery must hold a positive value.");
                        if (value > 100)
                            throw new FormatException("battery can't hold a value more than 100% of charge.");
                        battery = value;
                    }
                }

                public IDAL.DO.DroneStatuses Status { get; set; }
                public IDAL.DO.WeightCategories MaxWeight { set; get; }
            }
        }
    }
    
}
