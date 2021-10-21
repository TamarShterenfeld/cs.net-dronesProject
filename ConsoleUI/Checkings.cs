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
        
        
    }
}
