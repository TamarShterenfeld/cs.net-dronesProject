using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.DO;
using IDal.DO;


namespace IBL
{
    namespace BO
    {
        /// <summary>
        /// the class Customer contains the following details: id, name, phone, longitude, latitude.
        /// </summary>
        public class Customer:ILocatable
        {
            string id;
            string name;
            string phone;
            public string Id
            {
                get
                {
                    return id;
                }
                set
                {
                    if (value.Length != 9)
                    {
                        throw new BLStringIdException(value);
                    }
                    foreach (char digit in value)
                    {
                        if (!Char.IsDigit(digit))
                        {
                            throw new BLStringIdException(value);

                        }
                    }
                    id = value;
                }
            }
            public string Name
            {
                get
                {
                    return name;
                }
                set
                {
                    name = value;
                }
            }
            public string Phone
            {
                get
                {
                    return phone;
                }
                set
                {
                    phone = value;
                }
            }

            public Location MyLocation { get; set; }

            public List<ParcelInCustomer> FromCustomer { get; set; }
            public List<ParcelInCustomer> ToCustomer { get; set; }
            public Location Location { get; set; }



            /// <summary>
            /// constructor
            /// </summary>
            /// <param name="id"> Customer's id </param>
            /// <param name="name"> Customer's name </param>
            /// <param name="phone"> Customer's phone </param>
            /// <param name="location"> Customer's location </param>
            /// <param name="fromCustomer"> Customer's fromCustomer </param>
            /// <param name="toCustomer"> Customer's toCustomer </param>
            public Customer(string id, string name, string phone, Location location, List<ParcelInCustomer> fromCustomer, List<ParcelInCustomer> toCustomer)
            {
                this.id = id; this.name = name; this.phone = phone;
                Id = id; Name = name; Phone = phone; MyLocation = location; FromCustomer = fromCustomer; ToCustomer = toCustomer;
            }

            //default constructor
            public Customer() { }

            /// <summary>
            /// override ToString function.
            /// </summary>
            /// <returns>description of the Customer object</returns>
            public override string ToString()
            {
                return $"id: {Id} \n" +
                       $"name: {Name} \n" +
                       $"phone: {Phone}\n" +
                       $"location: {Location}\n" +
                       $"FromCustomer: {DeliveryInCustomerDetails(FromCustomer)}\n" +
                       $"ToCustomer: {DeliveryInCustomerDetails(ToCustomer)}\n";
            }

            /// <summary>
            /// collect all the details about the delivery in customer
            /// </summary>
            /// <returns> the details about the delivery in customer </returns>
            private static string DeliveryInCustomerDetails(List<ParcelInCustomer> DroneIC)
            {
                string deliveryDetails = "";
                foreach (ParcelInCustomer drone in DroneIC)
                {
                    deliveryDetails += drone.ToString();
                }
                return deliveryDetails;
            }
        }
    }
}
