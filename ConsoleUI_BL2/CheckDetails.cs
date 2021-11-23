using System;
using System.Collections.Generic;
using System.Text;
using IBL.BO;


namespace ConsoleUI_BL
{
    public static partial class Program
    {
        //----------------------------------------Check Methods------------------------------------------------------

        /// <summary>
        /// The function input baseStation's details while checking their validation.
        /// </summary>
        /// <param name="id">base station's id</param>
        /// <param name="name">base station's name</param>
        /// <param name="location">base station's name</param>  
        /// <param name="chargeSlots"> number of charge slots in the base station</param>
        internal static void CheckBaseStationDetails(ref int id, ref string name,ref Location location, ref int chargeSlots)
        {
            Console.WriteLine("Enter base station's details : id, name, longitude, latitude, number of chargeSlots.");
            id = InputIntValue( );
            name = InputStringValue();
            location = InputLocationValue();
            chargeSlots = InputIntValue();
        }

        /// <summary>
        /// The function input drone''s details while checking their validation.
        /// </summary>
        /// <param name="id">drone's id</param>
        /// <param name="battery">drone's battery</param>
        /// <param name="model">drone's model</param>
        /// <param name="maxWeight">drone's max weight</param>
        internal static void CheckDroneDetails(ref int id, ref string model, ref WeightCategories weight, ref int baseStationId)
        {
            Console.WriteLine("Enter drone's details :\n id, model, category weight and base station's id.");
            id = InputIntValue();
            model = InputIsNotNull();
            weight = InputWeightCategory();
            baseStationId = InputIntValue();
        }

        /// <summary>
        // The function input parcel's details while checking their validation.
        /// </summary>
        /// <param name="id">parcel's id</param>
        /// <param name="senderId">parcel's sender id</param>
        /// <param name="targetId">parcel's target id</param>
        /// <param name="weight">parcel's weight</param>
        /// <param name="priority">parcel's priority </param>
        internal static void CheckParcelDetails(ref string senderId, ref string targetId,ref WeightCategories weightCategory,ref Priorities priority1)
        {
            Console.WriteLine("Please enter :\n sender id,getter id, category weight and the priority of the drone.");
            //the checkings of the different (string) id are implemented within the struct Parcel and another function named "chackingIdentitiesOfParcel"
            senderId = InputStringId();
            targetId = InputStringId();
            weightCategory = InputWeightCategory();
            priority1 = InputPriority();
        }

        /// <summary>
       // The function input customer's details while checking their validation.
        /// </summary>
        /// <param name="id">customer's id</param>
        /// <param name="name">customer's name</param>
        /// <param name="phone">customer's phone</param>
        /// <param name="longitude">customer's longitude</param>
        /// <param name="latitude">customer's latitude</param>
        internal static void CheckCustomerDetails(ref string id, ref string name, ref string phone, ref Location location)
        {
            Console.WriteLine("Enter base customer's details : id, name, phone,  longitude, latitude.");
            //the needed checkings are implemented within the struct Customer or in DalObject.
            id = InputStringId();
            name = InputStringValue();
            phone = InputPhone();
            location = InputLocationValue();
        }
    }
}
