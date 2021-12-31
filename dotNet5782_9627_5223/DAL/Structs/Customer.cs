using System;
using System.Collections.Generic;
using System.Text;
using static DalObject.DataSource;
using DAL.DO;

namespace IDal
{
    namespace DO
    {

        /// <summary>
        /// the struct Customer contains the following details: id, name, phone, longitude, latitude.
        /// actually, these are all the basic details for creating a customer.
        /// </summary>
        public struct Customer
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
                        throw new IntIdException(value);
                    }
                    foreach (char digit in value)
                    {
                        if (!Char.IsDigit(digit))
                        {
                            throw new StringException(value);

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
                                throw new StringException(value);
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
                        throw new PhoneException(value);
                    foreach (char digit in value)
                    {
                        if (!Char.IsDigit(digit))
                        {
                            throw new PhoneException(value);

                        }
                    }
                    phone = value;
                }
            }

            public Coordinate Longitude { get; set; }
            public Coordinate Latitude { get; set; }

            /// <summary>
            /// override ToString function.
            /// </summary>
            /// <returns>description of the Customer object</returns>
            public override string ToString()
            {
                return $"id: {Id} \n" +
                       $"name: {Name} \n" +
                       $"phone: {Phone}\n" +
                       $"longitude: {Longitude} \n" +
                       $"latitude: {Latitude} \n";
            }
        }

    }
}
