using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL
{

    namespace BO
    {
        public class DroneForList
        {
            int id;
            int parcelId;

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

            public int ParcelId
            {
                set
                {
                    if (value < 0)
                    {
                        throw new DateTimeException("Id must contain a positive number");
                    }
                    parcelId = value;
                }
                get { return parcelId; }
            }

            //there's nothing to check for a model - it can hold chars and also digits.
            public string Model { get; set; }

            public WeightCategories MaxWeight { set; get; }

            public double Battery { get; set; }

            public DroneStatuses Status { set; get; }

            public Location Location { get; set; }

            public DroneForList() { }

            public DroneForList(int id, int parcelId, string model, WeightCategories weight, double battery, DroneStatuses status, Location location)
            {
                Id = id; ParcelId = parcelId; Model = model; MaxWeight = weight; Battery = battery; Status = status; Location = location;
            }
            
        }
    }
}
