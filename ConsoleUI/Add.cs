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
            id = int.Parse(Console.ReadLine());
            name = Console.ReadLine();
            longitude = int.Parse(Console.ReadLine());
            latitude = int.Parse(Console.ReadLine());
            chrgeSlots = int.Parse(Console.ReadLine());
        }

    }
}
