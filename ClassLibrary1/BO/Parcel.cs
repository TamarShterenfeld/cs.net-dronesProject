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
        public DroneInParcel Drone { get; set; }

        public DateTime ProductionDate { get; set; }
        public DateTime AssociationDate { get; set; }
        public DateTime PickUpDate { get; set; }
        public DateTime SupplyDate { get; set; }

        public Parcel(int id, CustomerInParcel sender, CustomerInParcel target, WeightCategories weight, Priorities priority, DroneInParcel drone)
        {
            this.id = id; Sender = sender;Target = target ; Weight = weight; Priority = priority; Drone = Drone;
            //a default value in the creation of the object.
            ProductionDate = AssociationDate = PickUpDate = SupplyDate = new DateTime(01 / 01 / 0001);
        }

        public Parcel(){}
    }

    
}
