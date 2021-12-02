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
        /// the class contains all the DroneInParcel's needed details.
        /// </summary>
        public class DroneInParcel 
        {
            int id;
            double battery;
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
                    if (value < 0)
                        throw new BatteryException(value);
                    battery = value;
                }
                get
                {
                    return battery;
                }
            }
            public Location CurrentLocation { get; set; }

            /// <summary>
            /// a constructor with parameters
            /// </summary>
            /// <param name="id"> DroneInPaecel's id </param>
            /// <param name="battery"> DroneInPaecel's battery </param>
            /// <param name="current"> DroneInPaecel's current location </param>
            public DroneInParcel(int id,double battery,Location current)
            {
                this.id = id;
                Id = id; Battery = battery; CurrentLocation = current;
            }

            // default constructor
            public DroneInParcel(){}

            /// <summary>
            /// override ToString function.
            /// </summary>
            /// <returns>description of DroneInPaecel objectreturns>
            public override string ToString()
            {
                return $"id: { Id } \n" +
                       $"battery: { Battery } " + "%" + "\n"+
                       $"current location: { CurrentLocation }" ;
            }
        }
    }
}
