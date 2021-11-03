using System;
using System.Collections.Generic;
using System.Text;
using static DalObject.DalObject;
using static IBL.BO.Locations;
using IBL.BO;
using IDAL.DO;
using static IDAL.DO.OverloadException;

namespace IBL
{
    namespace BO
    {
        
            /// <summary>
            /// the struct Customer contains the following details: id, name, phone, longitude, latitude.
            /// </summary>
            public class Customer
            {
                private string id;
                private string name;
                private string phone;
                Location location;
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

            private List<Parcel> toCustomer;
            private List<Parcel> fromCustomer;
            public List<Parcel> ToCustomer
            {
                get
                {
                    return toCustomer;
                }
                set
                {
                    toCustomer = value;
                }
            }

            public List<Parcel> FromCustomer
            {
                get
                {
                    return fromCustomer;
                }
                set
                {
                    fromCustomer = value;
                }
            }
            public Customer(string id, string name, string phone, Coordinate longitude, Coordinate latitude)
                {
                    this.id = id; this.name = name; this.phone = phone; this.location.Long = longitude;this.location.Lat = latitude;
                    Id = id; Name = name; Phone = phone; 
                }

            public Customer() { }
                /// <summary>
                /// override ToString function.
                /// </summary>
                /// <returns></returns>
                public override string ToString()
                {
                    return $"id: {Id} \n" +
                           $"name: {Name} \n" +
                           $"phone: {Phone}\n" +
                           $"location: {location}\n" +
                           $"להדפיס רשימות\n";
                }
            }
        
    }
}
