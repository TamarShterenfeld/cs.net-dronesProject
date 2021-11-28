
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL.BO
{
    public class Parcel
    {
        int id;
        DateTime productionDate = new();
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
                    throw new DateTimeException("Id must contain a positive number");
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

        public Parcel(int id, CustomerInParcel sender, CustomerInParcel target, WeightCategories weight, Priorities priority, DroneInParcel drone)
        {
            this.id = id; Sender = sender; Target = target; Weight = weight; Priority = priority; MyDrone = drone;
            //a default value in the creation of the object.
            ProductionDate = AssociationDate = PickUpDate = SupplyDate = new DateTime(01 / 01 / 0001);
        }

        public Parcel() { }

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
                   $"drone: {MyDrone} \n"; 

        }
    }

    
}


