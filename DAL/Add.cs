using IDAL.DO;
using System;
using System.Collections.Generic;
using System.Text;
using static IDAL.DO.IDAL;
using static DalObject.DataSource.Config;
using static DalObject.DataSource;
using System.Linq;

namespace DalObject
{
    public partial class DalObject
    {
        /// <summary>
        /// The function adds a base station
        /// </summary>
        /// <param name="id">base station's id</param>
        /// <param name="name">base station's name</param>
        /// <param name="longitude">longitude of the base station</param>
        /// <param name="latitude">latitude of the base station</param>
        /// <param name="chargeSlots"> number of charge slots in the base station</param>
        public static void AddBaseStation(int id, string name, double longitude, double latitude, int chrgeSlots)
        {
            //can add the base station just if the input id stil doesnt exist in the Base Station's list.
            if(BaseStationsList.FindIndex(item => item.Id == id) == -1)
            {
                //the Base Station List is full totally.

                if (IndexOfBaseStation >= BASESTATIONSAMOUNT)
                {
                    throw new Exception("The amount of base stations objects arrived to its maximum limit");
                }
                BaseStation baseStation = new BaseStation(id, name, longitude, latitude, chrgeSlots);          
                ++IndexOfBaseStation;
                BaseStationsList.Add(baseStation);
            }
            else
            {
                throw new Exception("Id already exists in base station list, it's not possible to add it!");
            }
        }

        /// <summary>
        /// The function adds a drone
        /// </summary>
        /// <param name="id">drone's id</param>
        /// <param name="battery">drone's battery</param>
        /// <param name="model">drone's model</param>
        /// <param name="maxWeight">drone's max weight</param>
        public static void AddDrone(int id, string model, string maxWeight, double battery)
        {
            //drones list is fulll already.
            if (IndexOfDrone  >= DRONESAMOUNT)
            {
                throw new Exception("The amount of drones objects arrived to its maximum limit");
            }
            //can add the drone just if the input id stil doesnt exist in the Drones' list.
            if (DronesList.FindIndex(item => item.Id == id) == -1)
            {
                //the enum type variables were defined to hold the numerical index of the enum category.
                DroneStatuses droneStatuses = DroneStatuses.Available;
                WeightCategories weightCategory = (WeightCategories)int.Parse(maxWeight);
               
                ++IndexOfDrone;
                Drone drone = new Drone(id, battery, model, droneStatuses, weightCategory);
                DronesList.Add(drone);
            }
            else
            {
                throw new Exception("Id already exists in drones list, it's not possible to add it!");
            }
        }

        /// <summary>
        /// The function adds a customer
        /// </summary>
        /// <param name="id">customer's id</param>
        /// <param name="name">customer's name</param>
        /// <param name="phone">customer's phone</param>
        /// <param name="longitude">customer's longitude</param>
        /// <param name="latitude">customer's latitude</param>
        public static void AddCustomer(string id, string name, string phone, double longitude, double latitude)
        {
            //customers list is full already.
            if (IndexOfCustomer >= CUSTOMERSAMOUNT)
            {
                throw new Exception("The amount of customers objects arrived to its maximum limit");
            }
            //can add the customer just if the input id stil doesnt exist in the customers' list.
            if (CustomersList.FindIndex(item => item.Id == id) == -1){
                
                Customer customer = new Customer(id, name, phone, longitude, latitude);
                ++IndexOfCustomer;
                CustomersList.Add(customer);
            }
        }

        /// <summary>
        /// The function adds a parcel
        /// </summary>
        /// <param name="id">parcel's id</param>
        /// <param name="senderId">parcel's sender id</param>
        /// <param name="targetId">parcel's target id</param>
        /// <param name="droneId">parcel's drone id</param>
        /// <param name="weight">parcel's weight</param>
        /// <param name="priority">parcel's priority </param>
        public static void AddParcel(int id, string senderId, string targetId, int droneId, string Weight, string Priority)
        {

            if (IndexOfParcel >= PARCELAMOUNT)
            {
                Console.WriteLine("The amount of base station objects arrived to its maximum limit");
                return;
            }
            chackingIdentitiesOfParcel(id, senderId, targetId, droneId);
            WeightCategories weightCategories = (WeightCategories)int.Parse(Weight);
            Priorities priorities = (Priorities)int.Parse(Priority);
            Parcel parcel = new Parcel(id, senderId, targetId, weightCategories, priorities, droneId);
            ++IndexOfParcel;
            ParcelsList.Add(parcel);
        }

    }
}
