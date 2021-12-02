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
        /// the class contains all the ParcelInCustomer's needed details.
        /// </summary>
        public class ParcelInCustomer
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
            public WeightCategories Weight { get; set; }
            public Priorities Priority;
            public ParcelStatuses ParcelStatus;
            public CustomerInParcel SourceOrDest { get; set; }

            /// <summary>
            /// a constructor with parameters
            /// </summary>
            /// <param name="id"> DeliveryInCustomer's id </param>
            /// <param name="weight"> DeliveryInCustomer's weight </param>
            /// <param name="priority"> DeliveryInCustomer's priority </param>
            /// <param name="parcelStatus"> DeliveryInCustomer's parcelStatus </param>
            /// <param name="sourceOrDest"> DeliveryInCustomer's sourceOrDest </param>
            public ParcelInCustomer(int id, WeightCategories weight, Priorities priority, ParcelStatuses parcelStatus, CustomerInParcel sourceOrDest)
            {
                this.id = id;
                Id = id; Weight = weight; Priority = priority; ParcelStatus = parcelStatus; SourceOrDest = sourceOrDest;
            }

            // default constructor
            public ParcelInCustomer(){}

            /// <summary>
            /// override ToString function.
            /// </summary>
            /// <returns>description of the DeliveryInCustomer object</returns>
            public override string ToString()
            {
                return $"id: {Id} \n" +
                       $"weight: {Weight} \n" +
                       $"priority: {Priority}\n" +
                       $"parcelStatus: {ParcelStatus}\n" +
                       $"sourceOrDest: {SourceOrDest}" ;
            }

        }
    }
}
