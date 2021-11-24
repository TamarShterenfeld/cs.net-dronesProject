using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace IBL
{
    namespace BO
    {
        public class ParcelInPassing
        {
            int id;
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

            // if the parcel waits to be picked up: false, else, if the parcel in the way: true.
            public bool ToDestination { get; set; }

            public Priorities Priority { get; set; }
            public WeightCategories Weight { get; set; }

            public CustomerInParcel Sender { get; set; }
            public CustomerInParcel Target { get; set; }

            public Location Collect { get; set; }
            public Location Destination { get; set; }

            public double Distatnce { get; set; }

            /// <summary>
            /// constructor
            /// </summary>
            /// <param name=" ParcelInPassing's id">id </param>
            /// <param name="priority"> ParcelInPassing's priority </param>
            /// <param name="source"> ParcelInPassing's source </param>
            /// <param name="destination"> ParcelInPassing's destination </param>
            public ParcelInPassing(int id, Priorities priority, CustomerInParcel sender, CustomerInParcel target)
            {
                this.id = id;
                Id = id; Priority = priority; Sender = sender; Target = target;
            }

            // default constructor
            public ParcelInPassing(){}

            /// <summary>
            /// override ToString function.
            /// </summary>
            /// <returns> description of the ParcelInPassing object </returns>
            public override string ToString()
            {
                return $"id: {Id} \n" +
                       $"priority: {Priority}\n" +
                       $"source: {Sender}\n" +
                       $"destination: {Target}\n";
            }
        }

    }
}
