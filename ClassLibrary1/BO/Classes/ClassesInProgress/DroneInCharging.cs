using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL
{
    namespace BO
    {
        /// <summary>
        /// the class contains all the DroneInCharging's needed details.
        /// </summary>
        public class DroneInCharging
        {
            int id;
            public int Id
            {
                set
                {
                    if (value < 0)
                    {
                        throw new BLIntIdException(value);
                    }
                    id = value;
                }
                get { return id; }
            }
            public double Battery
            {
                set
                {
                    if (value < 0 || value >100)
                        throw new BatteryException(value);
                    Battery = value;
                }
                get
                {
                    return Battery;
                }
            }

            /// <summary>
            /// a constructor with parameters
            /// </summary>
            /// <param name="id"> DroneInPaecel's id </param>
            /// <param name="battery"> DroneInPaecel's battery </param>
            public DroneInCharging(int id, double battery)
            {
                this.id = id;
                Id = id; Battery = battery;
            }

            // default constructor
            public DroneInCharging(){}

            /// <summary>
            /// override ToString function.
            /// </summary>
            /// <returns>description of DroneInCharging object</returns>
            public override string ToString()
            {
                return $"id: { Id } \n" +
                       $"battery: { Battery }" + "%" + "\n" ;
            }

        }

    }
}
