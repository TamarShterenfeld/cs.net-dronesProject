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
            /// the struct Customer contains all the needed details for a customer.
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
                            throw new FormatException("Id must include exactly 9 digits");
                        }
                        foreach (char letter in value)
                        {
                            if (!Char.IsDigit(letter))
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
                            if (!Char.IsLetter(letter))
                            {
                                throw new FormatException("Name can contain only letters.");
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
                            throw new FormatException("the first digit of a phone number must be '0'");
                        foreach (char digit in value)
                        {
                            if (Char.IsDigit(digit))
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
                        if (value < 0)
                        {
                            throw new FormatException("Longitude must be a positive number.");
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
                            throw new FormatException("Latitude must be a positive number.");
                        }

                        latitude = value;
                    }
                }
            }
        }
    }
}
