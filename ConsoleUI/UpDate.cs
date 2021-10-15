using System;
using System.Collections.Generic;
using System.Text;
using static IDAL.DO.IDAL;
using IDAL.DO;

namespace ConsoleUI
{
    public partial class Program
    {

        public static void InputDetailsOfDrone(ref Drone drone)
        {
            Console.WriteLine("Please enter :\n id, battery, model, category weight and the status of the drone.");
            int id, battery;
            string category, status;
            while (!int.TryParse(Console.ReadLine(), out id))
            {
                Console.WriteLine("id can contain only digits!");
            }

            while (!int.TryParse(Console.ReadLine(), out battery))
            {
                Console.WriteLine("battery can contain only numerical value!");
            }

            drone.Model = Console.ReadLine();
            category = Console.ReadLine();
            status = Console.ReadLine();
            for (int i = 0; i < 3; i++)
            {
                if ((string)Enum.GetNames(typeof(DroneStatuses)).GetValue(i) == status)
                {
                    drone.Status = (DroneStatuses)Enum.GetNames(typeof(DroneStatuses)).GetValue(i);
                    break;
                }
            }

        }
        public static void InputDetailsOfParcel(ref Parcel parcel)
        {
            Console.WriteLine("Please enter :\n id, sender id, getter id, category weight and the priority of the drone.");
            int id;
            string category, priority, senderId, getterId;

            while (!int.TryParse(Console.ReadLine(), out id))
            {
                Console.WriteLine("id can contain only digits!");
            }
            senderId = Console.ReadLine();
            getterId = Console.ReadLine();

            category = Console.ReadLine();
            priority = Console.ReadLine();

            for (int i = 0; i < 3; i++)
            {
                if ((string)Enum.GetNames(typeof(Priorities)).GetValue(i) == priority)
                {
                    parcel.Priority = (Priorities)Enum.GetNames(typeof(Priorities)).GetValue(i);
                    break;
                }
            }

            for (int i = 0; i < 3; i++)
            {
                if ((string)Enum.GetNames(typeof(WeightCategories)).GetValue(i) == category)
                {
                    parcel.Weight = (WeightCategories)Enum.GetNames(typeof(WeightCategories)).GetValue(i);
                    break;
                }
            }
        }
        public static void AssociatingParcel(ref Parcel parcel, ref Drone drone)
        {
            parcel.Association = DateTime.Now;
            parcel.DroneId = drone.Id;
            drone.Status = DroneStatuses.Shipment;
        }

        public static void PickingUpParcel(ref Parcel parcel)
        {
            parcel.PickingUp = DateTime.Now;
        }

        public static void SupplyParcel(ref Parcel parcel)
        {
            parcel.Arrival = DateTime.Now;
        }

        public static void ChargingDrone(ref Drone drone, ref BaseStation baseStation, ref DroneCharge droneCharge)
        {
            drone.Status = DroneStatuses.Maintenance;
            if (baseStation.ChrgeSlots > 0)
            {
                --baseStation.ChrgeSlots;
            }
            droneCharge.DroneId = drone.Id;
            droneCharge.StationId = baseStation.Id;
        }

        public static void StopDroneCharging(ref Drone drone, ref BaseStation baseStation)
        {
            drone.Status = DroneStatuses.Available;
            ++baseStation.ChrgeSlots;
        }
    }
}
    