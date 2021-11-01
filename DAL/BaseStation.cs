using System;
using System.Collections.Generic;
using static DAL.Locations;
using System.Text;

namespace IDAL
{
    namespace DO
    {
        public partial class IDAL
        {
            /// <summary>
            /// the struct BaseStation contains the following details: id, name, longitude, latitude,  number of chargeSlots.
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
                            throw new OverloadException("Id must contain a positive number");
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
                                    throw new OverloadException("Name can contain only letters.");
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
                        if (value < -180 || value > 180)
                        {
                            throw (new OverloadException("Longitude must be a positive number and in range of - 180º to 180º."));
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
                        if (value < -180 || value >180 )
                        {
                            throw (new OverloadException("Latitude must be a positive number and in range of -180º to 180º."));
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
                            throw (new OverloadException("Not valid number of chargeSlots"));
                        }

                        chargeSlots = value;
                    }
                }

                /// <summary>
                /// a constructor with parameters
                /// </summary>
                /// <param name="id">modify id</param>
                /// <param name="name">modify name</param>
                /// <param name="longitude">modify longitude</param>
                /// <param name="latitude">modify latitude</param>
                /// <param name="chargeSlots">modify chargeSlots</param>
                public BaseStation(int id, string name, double longitude, double latitude, int chargeSlots)
                {         
                    this.id = id; this.name = name; this.latitude = latitude; this.longitude = longitude;  this.chargeSlots = chargeSlots;
                    Id = id; Name = name; Latitude = latitude; Longitude = longitude; ChargeSlots = chargeSlots;
                }

                /// <summary>
                /// ovveride ToString function.
                /// </summary>
                /// <returns></returns>
                public override string ToString()
                {
                    return $"id: {Id} \n" +
                              $"name: {Name} \n" +
                              $"longitude: { DalObject.DataSource.coordinate.CastDoubleToCoordinante(Longitude, LONGITUDE)}\n" +
                              $"latitude:  {DalObject.DataSource.coordinate.CastDoubleToCoordinante(Latitude, LATITUDE)}\n" +
                              $"number of charge slots: {ChargeSlots}\n";
                }
            }
        }
    }
}
