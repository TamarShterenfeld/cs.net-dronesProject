using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleUI
{
    partial class Program
    {
        public static void BaseStationDetails(ref int  id,ref string name,ref double longitude,ref double latitude,ref int chrgeSlots)
        {
            Console.WriteLine("Enter base station's details : id, name, longitude, latitude, number of chrgeSlots.");
            while (!int.TryParse(Console.ReadLine(), out id))
            {
                Console.WriteLine("id can contain only digits!");
            }
            name = Console.ReadLine();
            while (!double.TryParse(Console.ReadLine(), out longitude))
            {
                Console.WriteLine("longitude can contain only digits!");
            }
            while (!double.TryParse(Console.ReadLine(), out latitude))
            {
                Console.WriteLine("latitude can contain only digits!");
            }
            while (!int.TryParse(Console.ReadLine(), out chrgeSlots))
            {
                Console.WriteLine("the number of chrgeSlots can contain only digits!");
            }
        }

        //public static void DroneDetails(ref int id, ref double battery, ref string model, ref IDAL.DO.DroneStatuses status, ref IDAL.DO.WeightCategories maxWeight)
        //{
        //    Console.WriteLine("Enter drone's details : id, battery, model, status, maxWeight.");
        //    id = int.Parse(Console.ReadLine());
        //    battery = id = int.Parse(Console.ReadLine());
        //    model = Console.ReadLine();
        //    //status = Console.ReadLine();
        //    //maxWeight = Console.ReadLine();
        //}

        public static void CustomerDetails(ref string id, ref string name, ref string phone , ref double longitude, ref double latitude)
        {
            Console.WriteLine("Enter base customer's details : id, name, phone,  longitude, latitude.");
            id = Console.ReadLine();
            name = Console.ReadLine();
            phone = Console.ReadLine();
            while (!double.TryParse(Console.ReadLine(), out longitude))
            {
                Console.WriteLine("longitude can contain only digits!");
            }
            while (!double.TryParse(Console.ReadLine(), out latitude))
            {
                Console.WriteLine("latitude can contain only digits!");
            }
        }

        //public static void PackageDetails(ref int id, ref string name, ref double longitude, ref double latitude, ref int chrgeSlots)
        //{
        //    Console.WriteLine("Enter base station's details : id, name, longitude, latitude, number of chrgeSlots.");
        //    id = int.Parse(Console.ReadLine());
        //    name = Console.ReadLine();
        //    longitude = int.Parse(Console.ReadLine());
        //    latitude = int.Parse(Console.ReadLine());
        //    chrgeSlots = int.Parse(Console.ReadLine());
        //}

    }
}
