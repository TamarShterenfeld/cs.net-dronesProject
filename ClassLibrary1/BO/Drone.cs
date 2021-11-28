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

        private DroneStatuses status = DroneStatuses.Maintenance;
        public DroneStatuses Status { get { return status; } set { status = value; } }
        private ParcelInPassing parcel = null;
        public ParcelInPassing Parcel { set { parcel = value; } get { return parcel; } }
        public Location Location { set; get; }

        public override string ToString()
        {
            return $"id: {Id} \n" +
                   $"model: {Model} \n" +
                   $"maxWeight:  {MaxWeight}\n" +
                   $"battery: {Battery} \n" +
                   $"status: {Status} \n" +
                   Parcel != null? $"parcel: {Parcel} \n":"\n" +
                   $"myLocation: {Location} \n";
        }
    }
}

