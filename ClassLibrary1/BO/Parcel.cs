using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDAL.DO;


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

        public DateTime Production { get; set; }
        public DateTime Association { get; set; }
        public DateTime PickingUp { get; set; }
        public DateTime Supplied { get; set; }

        public Parcel(int id, CustomerInParcel sender, CustomerInParcel target, WeightCategories weight, Priorities priority, DroneInParcel drone)
        {
            this.id = id; Sender = sender;Target = target ; Weight = weight; Priority = priority; Drone = Drone;
            //a default value in the creation of the object.
            Production = Association = PickingUp = Supplied = new DateTime(01 / 01 / 0001);
        }

        public Parcel(){}
    }

    
}
