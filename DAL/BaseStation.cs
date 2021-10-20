using System;
using System.Collections.Generic;
using System.Text;

namespace IDAL
{
    namespace DO
    {
        public partial class IDAL
        {
            /// <summary>
            /// the struct BaseStation contains all the needed details for a base station
            /// </summary>
            public struct BaseStation
            {
                private int id;
                public int Id
                {
                    get
                    {
                        return id;
                    }
                    set
                    {
                        if (value < 0)
                        {
                            throw new FormatException("Id must contain a positive number");
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
                        foreach (char letter in value)
                        {
                            if (letter != ' ')
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
                private double longitude;
                public double Longitude
                {
                    get
                    {
                        return longitude;
                    }
                    set
                    {
                        if (value < 0 || value > 360)
                        {
                            throw (new FormatException("Longitude must be a positive number and in range of 360 degrees."));
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
                        if (value < 0 || value >360 )
                        {
                            throw (new FormatException("Latitude must be a positive number and in range of 360 degrees."));
                        }

                        latitude = value;
                    }
                }
                private int chargeSlots;
                public int ChargeSlots
                {
                    get
                    {
                        return chargeSlots;
                    }
                    set
                    {
                        if (value < 0)
                        {
                            throw (new FormatException("Not valid number of chargeSlots"));
                        }

                        chargeSlots = value;
                    }
                }
                public BaseStation(int id, string name, double longitude, double latitude, int chargeSlots)
                {         
                    this.id = id; this.name = name; this.latitude = latitude; this.longitude = longitude;  this.chargeSlots = chargeSlots;
                    Id = id; Name = name; Latitude = latitude; Longitude = longitude; ChargeSlots = chargeSlots;
                }
            }
        }
    }
}
