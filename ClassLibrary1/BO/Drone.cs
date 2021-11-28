using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace IBL.BO
{
    /// <summary>
    /// the class Drone contains all the needed details for creating a drone object.
    /// </summary>
    public class Drone:ILocatable
    {
        int id;
        readonly static Random rand = new();
        double battery = rand.NextDouble() * 20 + 20;
        DroneStatuses status = DroneStatuses.Maintenance;
        ParcelInPassing parcel = null;
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
        public string Model { get; set; }
        public WeightCategories MaxWeight { set; get; } 
        public double Battery 
        { 
            get { return battery; } 
            set 
            {
                if (value < 0)
                    throw new BatteryException(value);
                battery = value; 
            }
        }
      
        public DroneStatuses Status { get { return status; } set { status = value; } }         
        public ParcelInPassing Parcel { set { parcel = value; } get { return parcel; } }
        public Location Location { set; get; }

        /// <summary>
        /// override ToString function.
        /// </summary>
        /// <returns>description of Drone object</returns>
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

