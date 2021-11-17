using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDAL.DO;
using static IDAL.DO.OverloadException;
using static IBL.BO.DalObject;

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
                        throw new OverloadException("Id must contain a positive number");
                    }
                    id = value;
                }
                get { return id; }
            }
            public double Battery { get; set; }
            public Location Current { get; set; }

            /// <summary>
            /// constructor
            /// </summary>
            /// <param name="id"> DroneInPaecel's id </param>
            /// <param name="battery"> DroneInPaecel's battery </param>
            /// <param name="current"> DroneInPaecel's current location </param>
            public DroneInParcel(int id,double battery,Location current)
            {
                this.id = id;
                Id = id; Battery = battery; Current = current;
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
                       $"current location: { Current } \n" ;
            }
        }
    }
}
