using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
    public partial class IDAL
    {
        /// <summary>
        /// the struct BaseStation contains all the needed details for a base station
        /// </summary>
        public struct BaseStation
        {
            private string id;
            public string Id
            {
                get
                {
                    return id;
                }
                set
                {
                    if (value.Length < 9)
                    {
                        throw new FormatException("Id must include 9 digits");
                    }
                    foreach (char letter in value)
                    {
                        if (letter < '0' || letter > '9')
                        {
                            throw new FormatException("Id must include only digits");

                        }
                    }
                    id = value;
                }
            }


            private string name;
            public string Name
            {
                get
                {
                    return name;
                }
                set
                {
                    if (value == null)
                    {
                        throw new FormatException("you must enter the name.");
                    }
                    foreach (char letter in value)
                    {
                        if(!Char.IsLetter(letter))
                        {
                            throw new FormatException("Name can contain only letters.");
                        }
                    }
                }
            }

            private double longitude;
            public double Longitude
            {
                get
                {
                    return longitude;
                }
                set
                {
                    if (value < 0)
                    {
                        throw (new FormatException("Longitude must be a positive number."));
                    }

                    longitude = value;
                }
            }

            private double latitude;
            public double Latitude
            {
                get
                {
                    return latitude;
                }
                set
                {
                    if (value < 0)
                    {
                        throw (new FormatException("Latitude must be a positive number."));
                    }

                    latitude = value;
                }
            }
            private int chrgeSlots;
            public int ChrgeSlots
            {
                get
                {
                    return chrgeSlots;
                }
                set
                {
                    if (value < 0)
                    {
                        throw (new FormatException("Not valid number of chrgeSlots"));
                    }

                    chrgeSlots = value;
                }
            }
        }
    }
}
