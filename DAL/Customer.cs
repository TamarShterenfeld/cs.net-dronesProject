﻿using System;
using System.Collections.Generic;
using System.Text;
using static DalObject.DalObject;

namespace IDAL
{
    namespace DO
    {
        public partial class IDAL
        {
            /// <summary>
            /// the struct Customer contains the following details: id, name, phone, longitude, latitude.
            /// </summary>
            
            public struct Customer
            {
                private string id;
                private string name;
                private string phone;
                private double longitude;
                private double latitude;
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
                            throw new FormatException("Id must include exactly 9 digits");
                        }
                        foreach (char digit in value)
                        {
                            if (!Char.IsDigit(digit))
                            {
                                throw new FormatException("Id must include only digits");

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
                            if(letter != ' ')
                            {
                                if (!Char.IsLetter(letter))
                                {
                                    throw new FormatException("Name can contain only letters.");
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
                            throw new FormatException("The first digit of a phone number must be '0'");
                        foreach (char digit in value)
                        {
                            if (!Char.IsDigit(digit))
                            {
                                throw new FormatException("Phone must include only digits");

                            }
                        }
                        phone = value;
                    }
                }
                public double Longitude
                {
                    get
                    {
                        return longitude;
                    }
                    set
                    {
                        if (value < -180 || value > 180)
                        {
                            throw new FormatException("Longitude must be a positive number and in range of 360 degrees.");
                        }

                        longitude = value;
                    }
                }
                public double Latitude
                {
                    get
                    {
                        return latitude;
                    }
                    set
                    {
                        if (value < -180 || value > 180)
                        {
                            throw new FormatException("Latitude must be a positive number and in range of 360 degrees.");
                        }
                        latitude = value;
                    }
                }

                /// <summary>
                /// a constructor with parameters
                /// </summary>
                /// <param name="id">modify id</param>
                /// <param name="name">modify name</param>
                /// <param name="phone">modify phone</param>
                /// <param name="longitude">modify longitude</param>
                /// <param name="latitude">modify latitude</param>
                public Customer(string id, string name, string phone, double longitude, double latitude)
                {
                    this.id = id; this.name = name; this.phone = phone; this.longitude = longitude; this.latitude = latitude;
                    Id = id;Name = name; Phone = phone; Longitude = longitude; Latitude = latitude;
                }

                /// <summary>
                /// override ToString function.
                /// </summary>
                /// <returns></returns>
                public override string ToString()
                {
                    return $"id: {Id} \n" +
                              $"name: {Name} \n" +
                              $"phone: {Phone}\n" +
                              $"longitude: { Longitude}\n" +
                              $"longitude: {DalObject.DataSource.coordinate.CastDoubleToCoordinante(Longitude)}\n" +
                              $"latitude:  {DalObject.DataSource.coordinate.CastDoubleToCoordinante(Latitude)}\n";
                }
            }
        }
    }
}
