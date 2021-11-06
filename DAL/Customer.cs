﻿using System;
using System.Collections.Generic;
using System.Text;
using static DalObject.DataSource;
using IDAL.DO;
using static IDAL.DO.OverloadException;

namespace IDAL
{
    namespace DO
    {

        /// <summary>
        /// the struct Customer contains the following details: id, name, phone, longitude, latitude.
        /// </summary>
        public class Customer
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
                        throw new OverloadException("Id must include exactly 9 digits");
                    }
                    foreach (char digit in value)
                    {
                        if (!Char.IsDigit(digit))
                        {
                            throw new OverloadException("Id must include only digits");

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
                                throw new OverloadException("Name can contain only letters.");
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
                        throw new OverloadException("The first digit of a phone number must be '0'");
                    foreach (char digit in value)
                    {
                        if (!Char.IsDigit(digit))
                        {
                            throw new OverloadException("Phone must include only digits");

                        }
                    }
                    phone = value;
                }
            }

            public Locations Location { set; get; }


            /// <summary>
            /// constructor
            /// </summary>
            /// <param name="id"> Customer's id </param>
            /// <param name="name"> Customer's name </param>
            /// <param name="phone"> Customer's phone </param>
            /// <param name="longitude"> Customer's longitude </param>
            /// <param name="latitude"> Customer's latitude </param>
            public Customer(string id, string name, string phone, Locations location)
            {
                this.id = id; this.name = name; this.phone = phone;
                Id = id; Name = name; Phone = phone; Location = location;
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
                       $"longitude: {Longitude} \n" +
                       $"latitude: {Latitude} \n";
            }
        }

    }
}
