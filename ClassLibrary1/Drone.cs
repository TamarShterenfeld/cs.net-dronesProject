using System;
using System.Collections.Generic;
using System.Text;
using static IDAL.DO.OverloadException;
using IDAL.DO;

namespace IBL
{
    namespace BO
    {
        /// <summary>
        /// the struct Drone contains the following details: id, battery, model, status, maxWeight.
        /// </summary>
        public class Drone
        {
            int id;
            double battery;
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

            //there's nothing to check for a model - it can hold chars and also digits.
            public string Model { get; set; }
            public double Battery
            {
                get { return battery; }
                set
                {
                    if (value < 0)
                        throw new OverloadException("Battery must hold a positive value.");
                    if (value > 100)
                        throw new OverloadException("Battery can't hold a value more than 100% of charge.");
                    battery = value;
                }
            }

            public IBL.BO.DroneStatuses Status { set; get; }
            public IBL.BO.WeightCategories MaxWeight { set; get; }

            /// <summary>
            /// a constructor with parameters
            /// </summary>
            /// <param name="id">modify id</param>
            /// <param name="battery">modify battery</param>
            /// <param name="model">modify model</param>
            /// <param name="status">modify status</param>
            /// <param name="maxWeight">modify maxWeight</param>
            public Drone(int id, double battery, string model, DroneStatuses status, WeightCategories maxWeight)
            {
                this.id = id; this.battery = battery; Model = model; Status = status; MaxWeight = maxWeight;
                Id = id; Battery = battery;
            }

            public Drone() { }

            /// <summary>
            /// override ToString function.
            /// </summary>
            /// <returns></returns>
            public override string ToString()
            {
                return $"id: {Id} \n" +
                          $"model: {Model} \n" +
                          $"status: {Status}\n" +
                          $"maxWeight:  {MaxWeight}\n" +
                          $"battery: {Battery}\n";
            }
        }
    }
}


