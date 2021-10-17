using System;
using System.Collections.Generic;
using System.Text;
using IDAL.DO;

namespace ConsoleUI
{
    public partial class Program
    {

        /// <summary>
        /// methods of checking the inputed details into the different structs.
        /// </summary>

        public static void InputingBaseStationDetails(ref int id, ref string name, ref double longitude, ref double latitude, ref int chargeSlots)
        {
            Console.WriteLine("Enter base station's details : id, name, longitude, latitude, number of chargeSlots.");
            while (!int.TryParse(Console.ReadLine(), out id))
            {
                Console.WriteLine("Id can contain only digits!");
            }
            name = Console.ReadLine();
            while (!double.TryParse(Console.ReadLine(), out longitude))
            {
                Console.WriteLine("Longitude can contain a numerical value!");
            }
            while (!double.TryParse(Console.ReadLine(), out latitude))
            {
                Console.WriteLine("Latitude can contain only a numerical value!");
            }
            while (!int.TryParse(Console.ReadLine(), out chargeSlots))
            {
                Console.WriteLine("The number of chargeSlots can contain only a numerical value!");
            }
        }


        public static void InputingDroneDetails(ref int id, ref double battery, ref string model, ref string maxWeight, ref string status)
        {
            Console.WriteLine("Enter drone's details :\n id, battery, model, category weight and the status of the drone.");
            while (!int.TryParse(Console.ReadLine(), out id))
            {
                Console.WriteLine("Id can contain only digits!");
            }

            while (!double.TryParse(Console.ReadLine(), out battery))
            {
                Console.WriteLine("Battery can contain only a numerical value!");
            }
            bool isExist1 = false, isExist2 = false;
            string currentEnum;
            model = Console.ReadLine();
            //checking if the inputed category exists in WeightCategories enum
            while (isExist1 == false)
            {
                maxWeight = Console.ReadLine();
                for (int i = 0; i < Enum.GetNames(typeof(WeightCategories)).Length; i++)
                {
                   currentEnum = (string)Enum.GetNames(typeof(WeightCategories)).GetValue(i);
                    if (currentEnum == maxWeight || currentEnum.ToLower() == maxWeight)
                    {
                        //category is assigned to hold the numeic value of the enum type.
                        maxWeight = i.ToString();
                        isExist1 = true;
                        break;
                    }
                }

                if (isExist1 == false)
                {
                    Console.WriteLine("The entered weight category doesn't exist\nPlease enter another category");
                }
            }

            //checking if the inputed status exists in DronesStatuses enum
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

        public static void InputingParcelDetails(ref string id, ref string senderId, ref string getterId, ref string weight, ref string priority)
        {
            Console.WriteLine("Please enter :\n id, sender id, getter id, category weight and the priority of the drone.");
            //the checkings of the different id are implemented within the struct Parcel and another function named "chackingIdentitiesOfParcel"
            id = Console.ReadLine();
            senderId = Console.ReadLine();
            getterId = Console.ReadLine();
            bool isExist1 = false, isExist2 = false;
            string currentEnum;
            //checking if the inputed category exists in WeightCategories enum
            while (isExist1 == false)
            {
                weight = Console.ReadLine();
                for (int i = 0; i < Enum.GetNames(typeof(WeightCategories)).Length; i++)
                {
                    currentEnum = (string)Enum.GetNames(typeof(WeightCategories)).GetValue(i);
                    if (currentEnum == weight || currentEnum.ToLower() == weight)
                    {
                        //category is assigned to hold the numeic value of the enum type.
                        weight = i.ToString();
                        isExist1 = true;
                        break;
                    }
                }

                if (isExist1 == false)
                {
                    Console.WriteLine("The entered weight category doesn't exist\nPlease enter another category");
                }
            }

            //checking if the inputed priority exists in Priorities enum
            while (isExist2 == false)
            {
                priority = Console.ReadLine();
                for (int i = 0; i < Enum.GetNames(typeof(Priorities)).Length; i++)
                {
                    currentEnum = (string)Enum.GetNames(typeof(Priorities)).GetValue(i);
                    if (currentEnum == priority || currentEnum.ToLower() == priority)
                    {
                        //priority is assigned to hold the numeic value of the enum type.
                        priority = i.ToString();
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



        public static void InputingCustomerDetails(ref string id, ref string name, ref string phone, ref double longitude, ref double latitude)
        {
            Console.WriteLine("Enter base customer's details : id, name, phone,  longitude, latitude.");
            //the needed checkings are implemented within the struct Customer
            id = Console.ReadLine();
            name = Console.ReadLine();
            phone = Console.ReadLine();
            while (!double.TryParse(Console.ReadLine(), out longitude))
            {
                Console.WriteLine("Longitude can contain only a numerical value!");
            }
            while (!double.TryParse(Console.ReadLine(), out latitude))
            {
                Console.WriteLine("Latitude can contain only a numerical value!");
            }
        }
    }

}
