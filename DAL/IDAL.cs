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
                private string name;
                private double longitude;
                private double latitude;
                private int chrgeSlots;


                public string Id
                {
                    get
                    {
                        return id;
                    }
                    set 
                    {
                        if(value.Length != 9 )
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
                            throw new FormatException("you must enter the name");
                        }
                        name = value;
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
                        if (value < 0)
                        {
                            throw new FormatException("not valid longitude");
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
                        if (value < 0)
                        {
                            throw new FormatException("not valid latitude");
                        }

                        latitude = value;
                    }
                }
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
                            throw new FormatException("not valid number of chrgeSlots");
                        }

                        chrgeSlots = value;
                    }
                }
            }


            /// <summary>
            /// the struct Customer  has id, name, phone, longitude, latitude
            /// </summary>
            /// 
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
                            throw new FormatException("ID must include 9 digits");
                        }
                        foreach (char letter in value)
                        {
                            if (letter < '0' || letter > '9')
                            {
                                throw new FormatException("ID must include only digits");

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
                        if (value == null)
                        {
                            throw new FormatException("you must enter the name");
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
                        if (value == null)
                        {
                            throw new FormatException("you must enter your phone");
                        }
                        foreach (char digit in value)
                        {
                            if (digit < '0' || digit > '9')
                            {
                                throw new FormatException("phone must include only digits");

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
                        if (value < 0)
                        {
                            throw new FormatException("not valid longitude");
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
                        if (value < 0)
                        {
                            throw new FormatException("not valid latitude");
                        }

                        latitude = value;
                    }
                }
            }
        }
    }
   
}
