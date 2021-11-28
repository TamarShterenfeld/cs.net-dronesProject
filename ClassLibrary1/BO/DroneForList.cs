using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL
{

    namespace BO
    {
        /// the class DroneForList contains all the DroneForList's details
        /// that we want to show to the client.
        public class DroneForList:ILocatable
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
            public string Model { get; set; }

            public WeightCategories MaxWeight { set; get; }

            public double Battery { get; set; }

            public DroneStatuses Status { set; get; }

            public Location Location { get; set; }

            /// <summary>
            /// default constructor
            /// </summary>
            public DroneForList() { }

            /// <summary>
            /// constructor
            /// </summary>
            /// <param name="id">DroneForList's id</param>
            /// <param name="parcelId">DroneForList's parcelId</param>
            /// <param name="model">DroneForList's model</param>
            /// <param name="weight">DroneForList's weight</param>
            /// <param name="battery">DroneForList's battery</param>
            /// <param name="status">DroneForList's status</param>
            /// <param name="location">DroneForList's location</param>
            public DroneForList(int id, int parcelId, string model, WeightCategories weight, double battery, DroneStatuses status, Location location)
            {
                Id = id; ParcelId = parcelId; Model = model; MaxWeight = weight; Battery = battery; Status = status; Location = location;
            }
            /// <summary>
            /// override ToString function.
            /// </summary>
            /// <returns>description of DroneForList object</returns>
            public override string ToString()
            {
                return $"id: {Id} \n" +
                       $"parcel id: {ParcelId} \n" +
                       $"model: {Model} \n" +
                       $"weight category: {MaxWeight} \n" +
                       $"battery: {Battery} \n" +
                       $"status: {Status} \n" +
                       $"locaion: {Location} \n";
            }

        }
    }
}
