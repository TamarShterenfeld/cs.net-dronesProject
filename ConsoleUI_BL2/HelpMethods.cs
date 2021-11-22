using System;
using System.Collections.Generic;
using System.Text;
using IBL.BO;
using static IBL.BO.Location;



namespace ConsoleUI_BL
{
    public partial class Program 
    {

        internal static Location InputLocationValue()
        {
            double longitude = InputDoubleValue();
            double latitude = InputDoubleValue();
            Location location1 = new(longitude, latitude);
            return location1;
        }

        /// <summary>
        /// The function checks if the variable is double type.
        /// </summary>
        /// <param name="numericalValue">a double type variable</param>
        internal static double InputDoubleValue( )
        {
            double numericalValue;
            while (!double.TryParse(Console.ReadLine(), out numericalValue))
            {
                Console.WriteLine("This field can input only numerical value! Please try again!");
            }
            return numericalValue;
        }

        /// <summary>
        /// The function checks if the variable is int type.
        /// </summary>
        /// <param name="numericalValue">a int type variable</param>
        internal static int InputIntValue()
        {
            int numericalValue;
            while (!int.TryParse(Console.ReadLine(), out numericalValue) )
            {
                Console.WriteLine("This field can input only numerical value! Please try again!");
            }
            return numericalValue;
        }

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
        /// The function checks if the variable is string type.
        /// </summary>
        /// <param name="str">a string type variable</param>
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
                    if ((!Char.IsLetter(letter)))
                    {
                        Console.WriteLine("The string type can hold only alphabetical values!");
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
        /// The function checks if the id is string type. 
        /// </summary>
        /// <param name="id">string type id</param>
        internal static string InputStringId( string id)
        {
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

        /// <summary>
        /// The function checks if the phone is valid. 
        /// </summary>
        /// <param name="phone">phone</param>
        internal static string InputPhone()
        {
            bool isValid = true;
            string phone;
            while (true)
            {
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
        /// <param name="maxWeight">WeightCategory: maxWeight</param>
        internal static WeightCategories InputWeightCategory(string maxWeight)
        {
            bool isExist1 = false;
            string currentEnum;
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
        /// <param name="priority">priority</param>
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
