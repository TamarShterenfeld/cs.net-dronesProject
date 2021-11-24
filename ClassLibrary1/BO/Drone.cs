using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace IBL.BO
{
    public class Drone:ILocatable
    {
        int id;
        private readonly static Random rand = new();
        private double battery = rand.NextDouble() * 20 + 20;
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

        //there's nothing to check for a model - it can hold chars and also digits.
        public string Model { get; set; }
        public WeightCategories MaxWeight { set; get; } 
        public double Battery { get { return battery; } set { battery = value; } }
        public DroneStatuses Status { get; set; }
        public ParcelInPassing Parcel { set; get; }
        public Location Location { get; set; }

        public Drone() { }

        public Drone(int id, string model, WeightCategories weight, double battery, DroneStatuses status, ParcelInPassing parcel, Location location)
        {
            Id = id; Model = model; MaxWeight = weight; Battery = battery; Status = status; Parcel = parcel; Location = location;
        }
        /// <summary>
        /// override ToString function.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"id: {Id} \n" +
                   $"model: {Model} \n" +
                   $"maxWeight:  {MaxWeight}\n" +
                   $"battery: {Battery} \n" +
                   $"status: {Status} \n" +
                   $"parcel: {Parcel} \n" +
                   $"myLocation: {Location} \n";
        }
    }
}

