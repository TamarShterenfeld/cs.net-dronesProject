 using System;
using System.Collections.Generic;
using System.Text;
using DAL.DO;

namespace IDal
{
    namespace DO 
    {
        /// <summary>
        /// the struct Drone contains the following details: id, battery, model, status, maxWeight.
        /// actually, these are all the basic details for creating a drone.
        /// </summary>
        public struct Drone
        {
            int id;
            public int Id
            {
                set
                {
                    if (value < 0)
                    {
                        throw new IntIdException(value);
                    }
                    id = value;
                }
                get { return id; }
            }
            public string Model { get; set; }

            public WeightCategories MaxWeight { set; get; }



            
            /// <summary>
            /// override ToString function.
            /// </summary>
            /// <returns></returns>
            public override string ToString()
            {
                return $"id: {Id} \n" +
                       $"model: {Model} \n" +
                       $"maxWeight:  {MaxWeight}\n";

            }
        }
    }
}


