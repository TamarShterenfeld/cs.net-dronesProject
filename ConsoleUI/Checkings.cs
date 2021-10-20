using System;
using System.Collections.Generic;
using System.Text;
using IDAL.DO;

namespace ConsoleUI
{
    public partial class Program
    {
        /// <summary>
        /// methods that check a first checking of the inputed details of the different structs.
        /// </summary>
        public static void CheckBaseStationDetails(ref int id, ref string name, ref double longitude, ref double latitude, ref int chargeSlots)
        {
            Console.WriteLine("Enter base station's details : id, name, longitude, latitude, number of chargeSlots.");
            inputIntValue(ref id);
            name = Console.ReadLine();
            inputDoubleValue(ref longitude);
            inputDoubleValue(ref latitude);
            inputIntValue(ref chargeSlots);
        }
        public static void CheckDroneDetails(ref int id, ref double battery, ref string model, ref string maxWeight, ref string status)
        {
            Console.WriteLine("Enter drone's details :\n id, battery, model, category weight and the status of the drone.");
            inputIntValue(ref id);
            inputDoubleValue(ref battery);
            model = Console.ReadLine();
            inputWeightCategory(ref maxWeight);
            inputDroneStatus(ref status);
        }

        public static void CheckParcelDetails(ref string id, ref string senderId, ref string getterId, ref string weight, ref string priority)
        {
            Console.WriteLine("Please enter :\n id, sender id, getter id, category weight and the priority of the drone.");
            //the checkings of the different (string) id are implemented within the struct Parcel and another function named "chackingIdentitiesOfParcel"
            id = Console.ReadLine();
            senderId = Console.ReadLine();
            getterId = Console.ReadLine();
            inputWeightCategory(ref weight);
            inputPriority(ref priority);
        }
        public static void CheckCustomerDetails(ref string id, ref string name, ref string phone, ref double longitude, ref double latitude)
        {
            Console.WriteLine("Enter base customer's details : id, name, phone,  longitude, latitude.");
            //the needed checkings are implemented within the struct Customer or in DalObject.
            id = Console.ReadLine();
            name = Console.ReadLine();
            phone = Console.ReadLine();
            inputDoubleValue(ref longitude);
            inputDoubleValue(ref latitude);
        }
        private static void inputIntValue(ref int numericalValue)
        {
            while (!int.TryParse(Console.ReadLine(), out numericalValue))
            {
                Console.WriteLine("This field can input only numerical value! Please try again!");
            }
        }
        private static void inputDoubleValue(ref double numericalValue)
        {
            while (!double.TryParse(Console.ReadLine(), out numericalValue))
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
        private static void inputDroneStatus(ref string status )
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
