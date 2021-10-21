using IDAL.DO;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleUI
{
    public partial class Program 
    {
        private static void inputDoubleValue(ref double numericalValue)
        {
            while (!double.TryParse(Console.ReadLine(), out numericalValue))
            {
                Console.WriteLine("This field can input only numerical value! Please try again!");
            }
        }

        private static void inputIntValue(ref int numericalValue)
        {
            while (!int.TryParse(Console.ReadLine(), out numericalValue))
            {
                Console.WriteLine("This field can input only numerical value! Please try again!");
            }
        }

        /// <summary>
        /// getting a string and check its existance in WeightCategories enum.
        /// </summary>
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
                        //category is assigned to hold the numeric value of the enum type.
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
                        //status is assigned to hold the numeic value of the enum type.
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
                        //priority is assigned to hold the numeic value of the enum type.
                        priority = i.ToString();
                        isExist3 = true;
                        break;
                    }
                }
                if (isExist3 == false)
                {
                    Console.WriteLine("The entered status doesn't exist\nPlease enter another status");
                }
            }
        }

    }
}
