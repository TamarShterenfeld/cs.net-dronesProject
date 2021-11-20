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
        DateTime associationDate = new DateTime();
        DateTime pickUpDate = new DateTime();
        DateTime supplyDate = new DateTime();
        DroneInParcel drone = null;
        public int Id
        {
            get { return id; }
            set
            {
                if (value < 0)
                {
                    throw new OverloadException("Id must contain a positive number");
                }
                id = value;
            }
        }

        public CustomerInParcel Sender { get; set; }
        public CustomerInParcel Target { get; set; }
        public WeightCategories Weight { get; set; }
        public Priorities Priority { get; set; } 
        public DroneInParcel MyDrone { get { return drone; } set { drone = value; } }
        public  DateTime ProductionDate { get { return ProductionDate; } init { ProductionDate = DateTime.Now; } }
        public DateTime AssociationDate { get { return associationDate; } set { associationDate = value; } }  
        public DateTime PickUpDate { get { return pickUpDate; } set { pickUpDate = value; } }
       
        public DateTime SupplyDate { get { return supplyDate; } set { supplyDate = value; } }

        public Parcel(int id, CustomerInParcel sender, CustomerInParcel target, WeightCategories weight, Priorities priority, DroneInParcel drone)
        {
            this.id = id; Sender = sender; Target = target; Weight = weight; Priority = priority; Drone = Drone;
            //a default value in the creation of the object.
            ProductionDate = AssociationDate = PickUpDate = SupplyDate = new DateTime(01 / 01 / 0001);
        }

        public Parcel() { }
    }

    
}
