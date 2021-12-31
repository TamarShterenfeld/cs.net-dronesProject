using System;
using System.Collections.Generic;
using System.Text;
using BO;



namespace ConsoleUI_BL
{
    public static partial class Program 
    {
        //------------------------------Help Methods------------------------------
        /// <summary>
        /// the function inputs valid values into a Location variable.
        /// </summary>
        /// <returns>the inputed Location value</returns>
        internal static Location InputLocationValue()
        {
            double longitude = InputDoubleValue();
            while (longitude < -180 || longitude > 180)
            {
                Console.WriteLine("the longitude value: " + longitude + " isn't valid" + "\nCoordinante value must be in range of - 180º to 180º");
                Console.WriteLine("Try again!");
                longitude = InputDoubleValue(); 
            }
            double latitude = InputDoubleValue();
            while (latitude < -180 || latitude > 180)
            {
                Console.WriteLine("the latitude value: " + latitude + " isn't valid"+ "\nCoordinante value must be in range of - 180º to 180º");
                Console.WriteLine("Try again!");
                latitude = InputDoubleValue();
            }
            Location location1 = new(new Coordinate(longitude, Locations.Longitude), new Coordinate(latitude, Locations.Latitude));
            return location1;
        }

        /// <summary>
        /// The function inputs a variable while checking if its type is double .
        /// </summary>
        /// <returns>the double type inputed variable</returns>
        internal static double InputDoubleValue()
        {
            double numericalValue;
            while (!double.TryParse(Console.ReadLine(), out numericalValue))
            {
                Console.WriteLine("This field can input only numerical value! Please try again!");
            }
            return numericalValue;
        }

        /// <summary>
        /// The function inputs a variable while checking if its type is int .
        /// </summary>
        /// <returns>the inputed int variable</returns>>
        internal static int InputIntValue()
        {
            int numericalValue;
            while (!int.TryParse(Console.ReadLine(), out numericalValue) )
            {
                Console.WriteLine("This field can input only numerical value! Please try again!");
            }
            return numericalValue;
        }

        /// <summary>
        /// The function inputs a variable while checking its value isn't null.
        /// </summary>
        /// <returns>the inputed str</returns>
        internal static string InputIsNotNull()
        {
            string str = Console.ReadLine();

            while (string.IsNullOrEmpty(str))
            {
                Console.WriteLine("String must hold a value!");
                str = Console.ReadLine();
            }
            return str;
        }

        /// <summary>
        /// The function inputs a variable 
        /// while checking it contains only chars and not null value.
        /// </summary>
        /// <param name="str">the inputed str</param>
        internal static string InputStringValue()
        {
            string str;
            while (true)
            {
                bool isValid = true;
                str = Console.ReadLine();
                if (string.IsNullOrEmpty(str))
                {
                    isValid = false;
                    Console.WriteLine("String must hold a value!");
                }
                foreach (char letter in str)
                {
                    if(letter != ' ')
                    {
                        if ((!Char.IsLetter(letter)))
                        {
                            Console.WriteLine("The string type can hold only alphabetical values!");
                            isValid = false;
                            break;
                        }
                    }                   
                        
                }
                if (!isValid)
                {
                    Console.WriteLine("Try again!");
                }
                else
                    return str;
            }
        }

        /// <summary>
        /// the function inputs a string while giving the option to enter a null value
        /// </summary>
        /// <returns>the inputed string value</returns>
        internal static string InputOptionalStringValue()
        {
            while (true)
            {
                string str;
                bool isValid = true;
                str = Console.ReadLine();
                if (string.IsNullOrEmpty(str))
                {
                    return null;
                }
                foreach (char letter in str)
                {
                    if(letter != ' ')
                    {
                        if ((!Char.IsLetter(letter)))
                        {
                            Console.WriteLine("The string type can hold only alphabetical values!");
                            isValid = false;
                            break;
                        }
                    }
                }
                if (!isValid)
                {
                    Console.WriteLine("Try again!");
                }
                else
                    return str;
            }
        }

       /// <summary>
       /// the function inputs an int value while giving the option to enter a null value.
       /// </summary>
       /// <returns>the inputed int value</returns>
        internal static string InputOptionalIntValue()
        {
            while (true)
            {
                string str;
                bool isValid = true;
                str = Console.ReadLine();
                if (string.IsNullOrEmpty(str))
                {
                    return null;
                }
                foreach (char digit in str)
                {
                    if ((!Char.IsDigit(digit)) || digit<0)
                    {
                        Console.WriteLine("The string type can hold only numerical possitive values!");
                        isValid = false;
                        break;
                    }

                }
                if (!isValid)
                {
                    Console.WriteLine("Try again!");
                }
                else
                    return str;
            }
        }


        /// <summary>
        /// The function inputs an id 
        /// while checking its contains only valid values of id.
        /// </summary>
        /// <returns>the inputed string id</returns>
        internal static string InputStringId()
        {
            string id;
            while (true)
            {
                bool isValid = true;
                id = Console.ReadLine();
                if(id.Length != 9)
                {
                    isValid = false;
                    Console.WriteLine("Id's length must be exactly nine!");
                }
                if (string.IsNullOrEmpty(id))
                {
                    isValid = false;
                    Console.WriteLine("String must hold a value!");
                }
                foreach (char digit in id)
                {
                    if (digit < '0' || digit > '9')
                    {
                        Console.WriteLine("Id can contain only digits!");
                        isValid = false;
                        break;
                    }
                }
                if (!isValid)
                {
                    Console.WriteLine("Try again!");
                }
                else
                    return id;
            }
            
        }

        ///<summary>
        /// The function inputs a phone number 
        /// while checking its contains only valid values of a phone number.
        /// </summary>
        /// <returns>the inputed string id</returns>
        internal static string InputPhone()
        {
           
            string phone;
            while (true)
            {
                bool isValid = true;
                phone = Console.ReadLine();
                if (phone.Length != 10 )
                {
                    Console.WriteLine("The phone length must be of 10 digits!");
                    isValid = false;
                }
                if(phone[0] != '0')
                {
                    Console.WriteLine("The phone number must begin with the digit '0'");
                    isValid = false;
                }
                foreach (char digit in phone)
                {
                    if(!Char.IsDigit(digit))
                    {
                        Console.WriteLine("Phone number can contain only digits!");
                        isValid  = false;
                        break;
                    }
                }
                if (!isValid)
                    Console.WriteLine("Try again!");
                else
                    return phone;
            }
        }

        /// <summary>
        /// getting a string and check its existance in WeightCategories enum.
        /// </summary>
        ///<returns>an appropriate WeightCategory variable to the inputed string variable</returns>
        internal static WeightCategories InputWeightCategory()
        {
            bool isExist1 = false;
            string currentEnum, maxWeight;
            WeightCategories weight = WeightCategories.Average;
            //checking if the inputed category (string) exists in WeightCategories enum
            while (isExist1 == false)
            {
                maxWeight = Console.ReadLine();
                for (int i = 1; i <= Enum.GetNames(typeof(WeightCategories)).Length; i++)
                {
                    currentEnum = (string)Enum.GetNames(typeof(WeightCategories)).GetValue(i-1);
                    if (currentEnum == maxWeight || currentEnum.ToLower() == maxWeight)
                    {
                        weight = (WeightCategories)i;
                        isExist1 = true;
                        break;
                    }
                }
                if (isExist1 == false)
                {
                    Console.WriteLine("The entered weight category doesn't exist\nPlease enter another weight category");
                }
            }
            return weight;
        }

        /// <summary>
        /// getting a string and check its existance in Priorities enum.
        /// </summary>
        ///<returns>an appropriate Priorities variable to the inputed string variable</returns>
        internal static Priorities InputPriority()
        {
            bool isExist3 = false;
            string currentEnum;
            Priorities priorities = Priorities.Emergency;
            while (isExist3 == false)
            {
                string priority = Console.ReadLine();
                for (int i = 1; i <= Enum.GetNames(typeof(Priorities)).Length; i++)
                {
                    currentEnum = (string)Enum.GetNames(typeof(Priorities)).GetValue(i-1);
                    if (currentEnum == priority || currentEnum.ToLower() == priority)
                    {
                        priorities = (Priorities)i;
                        isExist3 = true;
                        break;
                    }
                }
                if (isExist3 == false)
                {
                    Console.WriteLine("The entered priority doesn't exist\nPlease enter another priority");
                }
            }
            return priorities;
        }

        
    }
}
