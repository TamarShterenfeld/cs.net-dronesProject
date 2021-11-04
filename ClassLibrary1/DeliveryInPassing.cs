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
        public class DeliveryInPassing
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
            public WeightCategories Weight { get; set; }
            public Priorities Priority;
            public bool ToDestination { get; set; }

            public Location Collect { get; set; }
            public Location Destination { get; set; }
            
            public double Far { get; set; }

            /// <summary>
            /// constructor
            /// </summary>
            /// <param name="id">DeliveryInPassing's id</param>
            /// <param name="weight">DeliveryInPassing's weight</param>
            /// <param name="priority">DeliveryInPassing's priority</param>
            /// <param name="toDestination"> DeliveryInPassing's toDestination</param>
            /// <param name="collect">DeliveryInPassing's collect location</param>
            /// <param name="destination">DeliveryInPassing's destination location</param>
            /// <param name="far">DeliveryInPassing's far</param>
            public DeliveryInPassing(int id, WeightCategories weight, Priorities priority, bool toDestination, Location collect, Location destination, double far)
            {
                this.id = id;  
                Id = id; Weight = weight; Priority = priority; ToDestination = toDestination; Collect = collect; Destination = destination;Far = far;
            }

            // default constructor
            public DeliveryInPassing(){}

            /// <summary>
            /// override ToString function.
            /// </summary>
            /// <returns>description of the DeliveryInPassing object</returns>
            public override string ToString()
            {
                return $"id: {Id} \n" +
                       $"weight: {Weight} \n" +
                       $"priority: {Priority}\n" +
                       $"toDestination: {ToDestination}\n" +
                       $"collect location: {Collect}\n" +
                       $"destination location: {Destination}\n" +
                       $" far: {Far}\n";
            }
        }
    }
}
