using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.DO;

namespace IBL
{
    namespace BO
    {

        /// <summary>
        /// the class CustomerForList contains all the baseStation's details
        /// that we want to show to the client.
        /// </summary>
        public class CustomerForList
        {

            private string id;
            private string name;
            private string phone;

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
                            throw new DateTimeException("Id must include only digits");

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
                    foreach (char letter in value)
                    {
                        if (letter != ' ')
                        {
                            if (!Char.IsLetter(letter))
                            {
                                throw new DateTimeException("Name can contain only letters.");
                            }
                        }
                    }
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
                    if (value[0] != '0')
                        throw new DateTimeException("The first digit of a phone number must be '0'");
                    foreach (char digit in value)
                    {
                        if (!Char.IsDigit(digit))
                        {
                            throw new DateTimeException("Phone must include only digits");

                        }
                    }
                    phone = value;
                }
            }

            public int AmountOfSendAndSuppliedParcels { set; get; }
            public int AmountOfSendAndNotSuppliedParcels { set; get; }
            public int AmountOfGetParcels { set; get; }
            public int AmountOfInPassingParcels { set; get; }

            /// <summary>
            /// default constructor
            /// </summary>
            public CustomerForList() { }

            /// <summary>
            /// a constructor
            /// </summary>
            /// <param name="id">customer's id</param>
            /// <param name="name">customer's name</param>
            /// <param name="phone">customer's phone</param>
            /// <param name="amountOfSendAndSuppliedParcels">customer's amountOfSendAndSuppliedParcels</param>
            /// <param name="amountOfSendAndNotSuppliedParcels">customer's amountOfSendAndNotSuppliedParcels</param>
            /// <param name="amountOfGetParcels">customer's amountOfGetParcels</param>
            /// <param name="amountOfInPassingParcels">customer's amountOfInPassingParcels</param>
            public CustomerForList(string id, string name, string phone, int amountOfSendAndSuppliedParcels, int amountOfSendAndNotSuppliedParcels, int amountOfGetParcels, int amountOfInPassingParcels)
            {
                Id = id; Name = name; Phone = phone; AmountOfSendAndNotSuppliedParcels = amountOfSendAndSuppliedParcels;
                AmountOfSendAndNotSuppliedParcels = amountOfSendAndNotSuppliedParcels; AmountOfGetParcels = amountOfGetParcels;
                AmountOfInPassingParcels = amountOfInPassingParcels;
            }

            /// <summary>
            /// override ToString function.
            /// </summary>
            /// <returns></returns>
            public override string ToString()
            {
                return $"id: {Id} \n" +
                       $"name: {Name} \n" +
                       $"phone:  {Phone}\n" +
                       $"amount of 'send & supplied' parcels: {AmountOfSendAndSuppliedParcels}\n" +
                       $"amount of 'send & not supplied' parcels: {AmountOfSendAndNotSuppliedParcels}\n" +
                       $"amount of 'get' parcels: { AmountOfGetParcels}\n" +
                       $"amount of 'In Passing' parcels: {AmountOfInPassingParcels}\n";
            }

        }
    }
}
