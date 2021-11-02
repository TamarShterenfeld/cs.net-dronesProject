using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDAL.DO;
using static IDAL.DO.OverloadException;

namespace IBL
{

    namespace BO
    {
        class ListDrone
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

            //there's nothing to check for a model - it can hold chars and also digits.
            public string Model { get; set; }

            public WeightCategories MaxWeight { set; get; }

            public double Battery { get; set; }

            public DroneStatuses Status { set; get; }

            public Coordinate Longitude { get; set; }

            public Coordinate Latitude { get; set; }


            //o מספר חבילה מועברת(אם יש)
        }
    }
}
