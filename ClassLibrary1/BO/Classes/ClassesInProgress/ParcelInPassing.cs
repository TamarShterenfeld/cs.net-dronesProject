using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace IBL
{
    namespace BO
    {
        /// <summary>
        /// the class contains all the ParcelInPassing's needed details.
        /// </summary>
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
                        throw new BLIntIdException(value);
                    }
                    id = value;
                }
            }

            // if the parcel waits to be picked up: the value of ToDestination is false,
            //else - if the parcel in the way:  the value of ToDestination is true.
            public bool ToDestination { get; set; }

            public Priorities Priority { get; set; }
            public WeightCategories Weight { get; set; }

            public CustomerInParcel Sender { get; set; }
            public CustomerInParcel Target { get; set; }

            public Location Collect { get; set; }
            public Location Destination { get; set; }

            public double Distatnce { get; set; }

            /// <summary>
            /// a constructor with parameters
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
                       $"ToDestination: {ToDestination}\n"+
                       $"priority: {Priority}\n" +
                       $"source: {Sender}\n" +
                       $"weight: {Weight}\n"+
                       $"destination: {Target}\n"+
                       $"collect's location: {Collect}\n"+
                       $"destination's location: {Destination}\n"+
                       $"distatnce: {Distatnce}";

            }
        }

    }
}
