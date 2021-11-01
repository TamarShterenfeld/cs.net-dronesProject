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
                            throw new FormatException("Id must contain a positive number");
                        }
                        id = value;
                    }
                    get { return id; }
                }

                //there's nothing to check for a model - it can hold chars and also digits.
                public string Model { get; set; }

                public DO.WeightCategories MaxWeight { set; get; }

                /// <summary>
                /// a constructor with parameters
                /// </summary>
                /// <param name="id">modify id</param>
                /// <param name="battery">modify battery</param>
                /// <param name="model">modify model</param>
                /// <param name="status">modify status</param>
                /// <param name="maxWeight">modify maxWeight</param>
                public Drone(int id, string model,  WeightCategories maxWeight)
                {
                    this.id = id; Model = model;  MaxWeight = maxWeight;
                }

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
}

