using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace IBL
{
    namespace BO
    {

        public class DroneInParcel 
        {
            int id;
            public int Id
            {
                set
                {
                    if (value < 0)
                    {
                        throw new DateTimeException("Id must contain a positive number");
                    }
                    id = value;
                }
                get { return id; }
            }
            public double Battery { get; set; }
            public Location CurrentLocation { get; set; }

            /// <summary>
            /// constructor
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
                       $"name: { Battery } \n" +
                       $"current location: { CurrentLocation } \n" ;
            }
        }
    }
}
