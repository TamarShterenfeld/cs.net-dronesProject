
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL.BO
{
   /// <summary>
   /// the class contains all the parcel's needed  details.
   /// </summary>
    public class Parcel
    {
        int id;
        DateTime productionDate = DateTime.Now;
        DateTime associationDate = new();
        DateTime pickUpDate = new();
        DateTime supplyDate = new();        
        DroneInParcel drone = null;
        public int Id
        {
            get { return id; }
            set
            {
                if (value < 0)
                {
                    throw new BLIntIdException(value);
                }
                id = value;
            }
        }

        public CustomerInParcel Sender { get; set; }
        public CustomerInParcel Target { get; set; }
        public WeightCategories Weight { get; set; }
        public Priorities Priority { get; set; } 
        public DroneInParcel MyDrone { get { return drone; } set { drone = value; } }
        public  DateTime ProductionDate { get { return productionDate ; } init { productionDate = DateTime.Now; } }
        public DateTime AssociationDate { get { return associationDate; } set { associationDate = value; } }  
        public DateTime PickUpDate { get { return pickUpDate; } set { pickUpDate = value; } }       
        public DateTime SupplyDate { get { return supplyDate; } set { supplyDate = value; } }

        /// <summary>
        /// a contructor with parameters
        /// </summary>
        /// <param name="id">the parcel's id</param>
        /// <param name="sender">the parcel's sender</param>
        /// <param name="target">the parcel's target</param>
        /// <param name="weight">the parcel's weight</param>
        /// <param name="priority">the parcel's priority</param>
        /// <param name="drone">the drone that has to pass the parcel</param>
        public Parcel(int id, CustomerInParcel sender, CustomerInParcel target, WeightCategories weight, Priorities priority, DroneInParcel drone)
        {
            this.id = id; Sender = sender; Target = target; Weight = weight; Priority = priority; MyDrone = drone;
            //a default value in the creation of the object.
            ProductionDate = AssociationDate = PickUpDate = SupplyDate = new DateTime(01 / 01 / 0001);
        }

        /// <summary>
        /// default constructor
        /// </summary>
        public Parcel() { }

        /// <summary>
        /// override ToString function.
        /// </summary>
        /// <returns>description of the Parcel object</returns>
        public override string ToString()
        {
            return $"id: {Id} \n" +
                   $"productionDate: {ProductionDate} \n" +
                   $"associationDate:  {AssociationDate}\n" +
                   $"pickUpDate: {PickUpDate} \n" +
                   $"supplyDate: {SupplyDate} \n" +
                   $"sender: {Sender} \n" +
                   $"target: {Target} \n" +
                   $"weight: {Weight} \n" +
                   $"priority: {Priority} \n" +
                   $"drone: {MyDrone}"; 
        }
    }

    
}


