using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL
{
    namespace BO
    {

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
                        throw new DateTimeException("Id must include exactly 9 digits");
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

            public CustomerForList() { }

            public CustomerForList(string id, string name, string phone, int amountOfSendAndSuppliedParcels, int amountOfSendAndNotSuppliedParcels, int amountOfGetParcels, int amountOfInPassingParcels)
            {
                Id = id; Name = name; Phone = phone; AmountOfSendAndNotSuppliedParcels = amountOfSendAndSuppliedParcels;
                AmountOfSendAndNotSuppliedParcels = amountOfSendAndNotSuppliedParcels; AmountOfGetParcels = amountOfGetParcels;
                AmountOfInPassingParcels = amountOfInPassingParcels;
            }

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
