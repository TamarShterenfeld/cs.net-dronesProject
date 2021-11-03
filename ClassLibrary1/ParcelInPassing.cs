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
        class ParcelInPassing
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
            public Priorities Priority;
            public CustomerInShipment Source { get; set; }
            public CustomerInShipment Destination { get; set; }

            /// <summary>
            /// constructor
            /// </summary>
            /// <param name=" ParcelInPassing's id">id </param>
            /// <param name="priority"> ParcelInPassing's priority </param>
            /// <param name="source"> ParcelInPassing's source </param>
            /// <param name="destination"> ParcelInPassing's destination </param>
            public ParcelInPassing(int id, Priorities priority, CustomerInShipment source, CustomerInShipment destination)
            {
                this.id = id;
                Id = id; Priority = priority; Source = source; Destination = destination;
            }

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
