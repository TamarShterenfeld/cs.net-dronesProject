using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static IDAL.DO.OverloadException;
using IDAL.DO;

namespace IBL
{
    namespace BO
    {

        public class ListCustomer
        {

            private string id;
            private string name;
            private string phone;

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
                        throw new OverloadException("Id must include exactly 9 digits");
                    }
                    foreach (char digit in value)
                    {
                        if (!Char.IsDigit(digit))
                        {
                            throw new OverloadException("Id must include only digits");

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

            public string Phone
            {
                get
                {
                    return phone;
                }
                set
                {
                    if (value[0] != '0')
                        throw new OverloadException("The first digit of a phone number must be '0'");
                    foreach (char digit in value)
                    {
                        if (!Char.IsDigit(digit))
                        {
                            throw new OverloadException("Phone must include only digits");

                        }
                    }
                    phone = value;
                }
            }

            //o	מספר חבילות ששלח וסופקו
            //  o מספר חבילות ששלח אך עוד לא סופקו
            //o מספר חבילות שקיבל
            //o מספר חבילות שבדרך אל הלקוח

        }
    }
}
