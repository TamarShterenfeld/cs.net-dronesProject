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
        /// the struct Drone contains the following details: id, battery, model, status, maxWeight.
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
                        throw new OverloadException("Id must contain a positive number");
                    }
                    id = value;
                }
                get { return id; }
            }
            public string Model { get; set; }

            public WeightCategories MaxWeight { set; get; }

            public double Battery { get; set; }

            public DroneStatuses Status { set; get; }


            
            /// <summary>
            /// override ToString function.
            /// </summary>
            /// <returns></returns>
            public override string ToString()
            {
                return $"id: {Id} \n" +
                       $"model: {Model} \n"+
                       $"maxWeight:  {MaxWeight}\n"+
                       $"battery:  {Battery}\n"+
                       $"status:  {Status}\n";

            }
        }
    }
}


