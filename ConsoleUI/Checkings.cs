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
            inputStringValue(ref name);
            inputDoubleValue(ref longitude);
            inputDoubleValue(ref latitude);
            inputIntValue(ref chargeSlots);
        }
        public static void CheckDroneDetails(ref int id, ref double battery, ref string model, ref string maxWeight)
        {
            Console.WriteLine("Enter drone's details :\n id, battery, model, category weight and the status of the drone.");
            inputIntValue(ref id);
            inputDoubleValue(ref battery);
            inputStringValue(ref model);
            inputWeightCategory(ref maxWeight);
        }

        public static void CheckParcelDetails(ref string id, ref string senderId, ref string targetId, ref string weight, ref string priority)
        {
            Console.WriteLine("Please enter :\n id, sender id, getter id, category weight and the priority of the drone.");
            //the checkings of the different (string) id are implemented within the struct Parcel and another function named "chackingIdentitiesOfParcel"
            inputStringId(ref id);
            inputStringId(ref senderId);
            inputStringId(ref targetId);
            inputWeightCategory(ref weight);
            inputPriority(ref priority);
        }
        public static void CheckCustomerDetails(ref string id, ref string name, ref string phone, ref double longitude, ref double latitude)
        {
            Console.WriteLine("Enter base customer's details : id, name, phone,  longitude, latitude.");
            //the needed checkings are implemented within the struct Customer or in DalObject.
            inputStringId(ref id);
            inputStringValue(ref name);
            inputPhone(ref phone);
            inputDoubleValue(ref longitude);
            inputDoubleValue(ref latitude);
        }

        public static void InputAssociatedParcelDetails(ref int parcelId, ref int droneId)
        {
            inputIntValue(ref parcelId);
            inputIntValue(ref droneId);
        }

        public static void InputPickUpParcelDetails(ref int parcelId, ref string senderId)
        {
            inputIntValue(ref parcelId);
            inputStringId(ref senderId);
        }

        public static void InputArrivalDetails(ref int parcelId, ref string targetId)
        {
            inputIntValue(ref parcelId);
            inputStringValue(ref targetId);
        }
    }
}
