using System;

namespace IDAL
{
    namespace DO
    {
        
        public partial class IDAL
        {
            /// <summary>
            /// the struct BaseStation has id, name, longitude, latitude, chargeSolts
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
                        if(value.Length < 9 )
                        {
                            throw new FormatException("ID must include 9 digits");
                        }
                        foreach(char letter in value)
                        {
                            if(letter<'0' || letter>'9')
                            {
                                throw new FormatException("ID must include only digits");

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
                        if (value == null )
                        {
                            throw (new FormatException("you must enter the name"));
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
                            throw (new FormatException("not valid longitude"));
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
                            throw (new FormatException("not valid latitude"));
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
                            throw (new FormatException("not valid number of chrgeSlots"));
                        }

                        chrgeSlots = value;
                    }
                }
            }
        }
    }
   
}
