using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace BO
{
    /// <summary>
    /// the class Drone contains all the needed details for creating a drone object.
    /// </summary>
    public class Drone:ILocatable
    {
        int id;
        readonly static Random rand = new();
        double battery = rand.NextDouble() * 20 + 20;
        DroneStatuses status = DroneStatuses.Available;
        ParcelInPassing parcel = null;
        Location location = new();
        public int Id
        {
            set
            {
                if (value < 0)
                {
                    throw new BLIntIdException(value);
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
                if (value < 0 || value > 100)
                    throw new BatteryException(value);
                battery = value; 
            }
        }
      
        public DroneStatuses Status { get { return status; } set { status = value; } }         
        public ParcelInPassing Parcel { set { parcel = value; } get { return parcel; } }
        public Location Location
        {
            set
            {
                location = value;
            }
            get
            {
                return location;
            }
        }
        public bool IsDeleted { get; set; }
        /// <summary>
        /// a constructor with parameters.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <param name="weight"></param>
        /// <param name="battery"></param>
        /// <param name="status"></param>
        /// <param name="parcel"></param>
        /// <param name="location"></param>
        public Drone(int id, string model, WeightCategories weight, double battery, DroneStatuses status, ParcelInPassing parcel, Location location , bool isDeleted)
        {
            Id = id; Model = model; MaxWeight = weight; Battery = battery; Status = status; Parcel = parcel; Location = location;IsDeleted = isDeleted;
        }

        /// <summary>
        /// a default contructor.
        /// </summary>
        public Drone() { }
        /// <summary>
        /// override ToString function.
        /// </summary>
        /// <returns>description of Drone object</returns>
        public override string ToString()
        {
            return $"id: {Id} \n" +
                   $"model: {Model} \n" +
                   $"maxWeight:  {MaxWeight}\n" +
                   $"battery: {Battery} " + "%" + "\n" +
                   $"status: {Status} \n" +
                   $"parcel: {Parcel} \n" +
                   $"myLocation: {Location}";
        }
    }
}

