using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDAL.DO;
using static IDAL.DO.OverloadException;
using static IBL.BO.DalObject;

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
                        throw new OverloadException("Id must contain a positive number");
                    }
                    id = value;
                }
            }
            public bool ToDestination { get; set; }

            public Priorities Priority { get; set; }
            public WeightCategories Weight { get; set; }

            public CustomerInParcel Sender { get; set; }
            public CustomerInParcel Target { get; set; }

            public Locations Collect { get; set; }
            public Locations Destination { get; set; }

            public double Far { get; set; }

            /// <summary>
            /// constructor
            /// </summary>
            /// <param name=" ParcelInPassing's id">id </param>
            /// <param name="priority"> ParcelInPassing's priority </param>
            /// <param name="source"> ParcelInPassing's source </param>
            /// <param name="destination"> ParcelInPassing's destination </param>
            public ParcelInPassing(int id, Priorities priority, CustomerInParcel source, CustomerInParcel destination)
            {
                this.id = id;
                Id = id; Priority = priority; Source = source; Destination = destination;
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
                       $"source: {Source}\n" +
                       $"destination: {Destination}\n";
            }
        }

    }
}
