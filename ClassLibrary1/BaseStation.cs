using System;
using System.Collections.Generic;
using static IBL.BO.Locations;
using System.Text;
using IDAL.DO;
using static IDAL.DO.OverloadException;
using static IBL.BO.DalObject;

namespace IBL
{
    namespace BO
    {

        /// <summary>
        /// the struct BaseStation contains the following details: id, name, longitude, latitude,  number of chargeSlots.
        /// </summary>

        public class BaseStation
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
            private Location location;
            public Location Loc
            {
                get
                {
                    return location;
                }
                set
                {

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
            public BaseStation(int id, string name, Coordinate longitude, Coordinate latitude, int chargeSlots)
            {
                this.id = id; this.name = name; this.latitude = latitude; this.longitude = longitude; this.chargeSlots = chargeSlots;
                Id = id; Name = name; Latitude = latitude; Longitude = longitude; ChargeSlots = chargeSlots;
            }

            public BaseStation() { }

            /// <summary>
            /// ovveride ToString function.
            /// </summary>
            /// <returns></returns>
            public override string ToString()
            {
                return $"id: {Id} \n" +
                          $"name: {Name} \n" +
                          $"longitude: { Longitude}\n" +
                          $"latitude:  {Latitude}\n" +
                          $"number of charge slots: {ChargeSlots}\n";
            }
        }

    }
}
