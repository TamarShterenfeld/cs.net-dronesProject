using System;
using System.Collections.Generic;
using static IDAL.DO.Locations;
using System.Text;
using IDAL.DO;
using static IDAL.DO.OverloadException;
using static DalObject.DataSource;

namespace IDAL
{
    namespace DO
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

            public Coordinate Longitude { get; set; }
            public Coordinate Latitude { get; set; }

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
            /// override ToString function.
            /// </summary>
            /// <returns>description of the BaseStation object</returns>
            public override string ToString()
            {
                return  $"id: {Id} \n" +
                        $"name: {Name} \n" +
                        $"longitude: {Longitude} \n" +
                        $"latitude: {Latitude} \n" +
                        $"number of charge slots: {ChargeSlots}\n";
            }

            
        }

    }
}
