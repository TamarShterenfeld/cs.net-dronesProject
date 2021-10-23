using IDAL.DO;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleUI
{
    public partial class Program 
    {
        /// <summary>
        /// The function checks if the variable is double type.
        /// </summary>
        /// <param name="numericalValue">a double type variable</param>
        private static void inputDoubleValue(ref double numericalValue)
        {
            while (!double.TryParse(Console.ReadLine(), out numericalValue))
            {
                Console.WriteLine("This field can input only numerical value! Please try again!");
            }
        }

        /// <summary>
        /// The function checks if the variable is int type.
        /// </summary>
        /// <param name="numericalValue">a int type variable</param>
        private static void inputIntValue(ref int numericalValue)
        {
            while (!int.TryParse(Console.ReadLine(), out numericalValue))
            {
                Console.WriteLine("This field can input only numerical value! Please try again!");
            }
        }

        /// <summary>
        /// The function checks if the variable is string type.
        /// </summary>
        /// <param name="str">a string type variable</param>
        private static void inputStringValue(ref string str)
        {
            bool isValid = true;
            while (true)
            {
                str = Console.ReadLine();
                if (string.IsNullOrEmpty(str))
                {
                    Console.WriteLine("String must hold a value!");
                }
                foreach (char letter in str)
                {
                    if ((letter < 'a' || letter > 'Z') && (letter < '0' || letter > '9'))
                        isValid = false;
                }
                if (!isValid)
                {
                    Console.WriteLine("The string type can hold only alphabetical values! Try again");
                }
                else
                    return;
            }
        }

        /// <summary>
        /// The function checks if the id is string type. 
        /// </summary>
        /// <param name="id">string type id</param>
        public static void inputStringId(ref string id)
        {
            bool isValid = true;
            while (true)
            {
                id = Console.ReadLine();
                if(id.Length != 9)
                {
                    isValid = false;
                    Console.WriteLine("Id's length must be exactly nine! Try again!");
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
                    return;
            }
            
        }

        /// <summary>
        /// The function checks if the phone is valid. 
        /// </summary>
        /// <param name="phone">phone</param>
        public static void inputPhone(ref string phone)
        {
            bool isValid = true;
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
                    }
                }
                if (!isValid)
                    Console.WriteLine("Try again!");
                else
                    return;
            }
        }

        /// <summary>
        /// getting a string and check its existance in WeightCategories enum.
        /// </summary>
        /// <param name="maxWeight">WeightCategory: maxWeight</param>
        private static void inputWeightCategory(ref string maxWeight)
        {
            bool isExist1 = false;
            string currentEnum;
            //checking if the inputed category (string) exists in WeightCategories enum
            while (isExist1 == false)
            {
                maxWeight = Console.ReadLine();
                for (int i = 0; i < Enum.GetNames(typeof(WeightCategories)).Length; i++)
                {
                    currentEnum = (string)Enum.GetNames(typeof(WeightCategories)).GetValue(i);
                    if (currentEnum == maxWeight || currentEnum.ToLower() == maxWeight)
                    {
                        //category is assigned to hold the numeric indx of the enum type.
                        maxWeight = i.ToString();
                        isExist1 = true;
                        break;
                    }
                }
                if (isExist1 == false)
                {
                    Console.WriteLine("The entered weight category doesn't exist\nPlease enter another weight category");
                }
            }
        }

        /// <summary>
        /// getting a string and check its existance in DronesStatuses enum.
        /// </summary>
        /// <param name="status">DronesStatuses: status</param>
        private static void inputDroneStatus(ref string status)
        {
            bool isExist2 = false;
            string currentEnum;
            while (isExist2 == false)
            {
                status = Console.ReadLine();
                for (int i = 0; i < Enum.GetNames(typeof(DroneStatuses)).Length; i++)
                {
                    currentEnum = (string)Enum.GetNames(typeof(DroneStatuses)).GetValue(i);
                    if (currentEnum == status || currentEnum.ToLower() == status)
                    {
                        //status is assigned to hold the numeic index of the enum type.
                        status = i.ToString();
                        isExist2 = true;
                        break;
                    }
                }
                if (isExist2 == false)
                {
                    Console.WriteLine("The entered status doesn't exist\nPlease enter another status");
                }
            }
        }

        /// <summary>
        /// getting a string and check its existance in Priorities enum.
        /// </summary>
        /// <param name="priority">priority</param>
        private static void inputPriority(ref string priority)
        {
            bool isExist3 = false;
            string currentEnum;
            while (isExist3 == false)
            {
                priority = Console.ReadLine();
                for (int i = 0; i < Enum.GetNames(typeof(Priorities)).Length; i++)
                {
                    currentEnum = (string)Enum.GetNames(typeof(Priorities)).GetValue(i);
                    if (currentEnum == priority || currentEnum.ToLower() == priority)
                    {
                        //priority is assigned to hold the numeic index of the enum type.
                        priority = i.ToString();
                        isExist3 = true;
                        break;
                    }
                }
                if (isExist3 == false)
                {
                    Console.WriteLine("The entered priority doesn't exist\nPlease enter another priority");
                }
            }
        }

    }
}
