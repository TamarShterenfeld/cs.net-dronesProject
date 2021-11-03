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
            public Location Location
            {
                get
                {
                    return location;
                }
                set
                {
                    location.CoorLongitude = value.CoorLongitude;
                    location.CoorLatitude = value.CoorLatitude;
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

            private List<DroneInCharging> DroneCharging { get; set; }

            /// <summary>
            /// a constructor with parameters
            /// </summary>
            /// <param name="id">modify id</param>
            /// <param name="name">modify name</param>
            /// <param name="longitude">modify longitude</param>
            /// <param name="latitude">modify latitude</param>
            /// <param name="chargeSlots">modify chargeSlots</param>
            public BaseStation(int id, string name, Location location, int chargeSlots)
            {
                this.id = id; this.name = name;this.location = location; this.chargeSlots = chargeSlots;
                Id = id; Name = name; Location = location; ChargeSlots = chargeSlots;
            }

            public BaseStation() { }

            /// <summary>
            /// override ToString function.
            /// </summary>
            /// <returns>description of the BaseStation object</returns>
            public override string ToString()
            {
                return $"id: {Id} \n" +
                          $"name: {Name} \n" +
                          $"location: { location }\n"+
                          $"number of charge slots: {ChargeSlots}\n"+
                          $"הדפסת הרשימה";
            }
        }

    }
}
