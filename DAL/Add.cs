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
    public partial class DalObject : IDAL.IDal
    {
        /// <summary>
        /// The function adds a base station to the Base Stations' list.
        /// </summary>
        /// <param name="id">base station's id</param>
        /// <param name="name">base station's name</param>
        /// <param name="longitude">longitude of the base station</param>
        /// <param name="latitude">latitude of the base station</param>
        /// <param name="chargeSlots"> number of charge slots in the base station</param>
        public static void AddBaseStation(int id, string name, double longitude, double latitude, int chrgeSlots)
        {
            //can add the base station just if the input id still doesn't exist in the Base Station's list.
            if(BaseStationsList.FindIndex(item => item.Id == id) == -1)
            {
                //the Base Station List is full totally.

                if (BaseStationsList.Count == BaseStationsList.Capacity)
                {
                    throw new OverloadException("The amount of base stations objects arrived to its maximum limit");
                }
                BaseStation baseStation = new BaseStation(id, name, longitude, latitude, chrgeSlots);          
                BaseStationsList.Add(baseStation);
            }
            else
            {
                throw new OverloadException("Id already exists in base station list, it's not possible to add it!");
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
            if (DronesList.Count  == DronesList.Capacity)
            {
                throw new OverloadException("The amount of drones objects arrived to its maximum limit");
            }
            //can add the drone just if the input id stil doesnt exist in the Drones' list.
            if (DronesList.FindIndex(item => item.Id == id) == -1)
            {
                //the enum type variables were defined to hold the numerical index of the enum category.
                
                WeightCategories weightCategory = (WeightCategories)int.Parse(maxWeight);
                Drone drone = new Drone(id, model, weightCategory);
                DronesList.Add(drone);
            }
            else
            {
                throw new OverloadException("Id already exists in drones list, it's not possible to add it!");
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
            if (CustomersList.Count >= CustomersList.Capacity)
            {
                throw new OverloadException("The amount of customers objects arrived to its maximum limit");
            }
            //can add the customer just if the input id stil doesn't exist in the customers' list.
            if (CustomersList.FindIndex(item => item.Id == id) == -1){               
                Customer customer = new Customer(id, name, phone, longitude, latitude);
                CustomersList.Add(customer);
            }
            else
            {
                throw new OverloadException("Id already exists in drones list, it's not possible to add it!");
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
        public static void AddParcel(int id, string senderId, string targetId, int droneId, string weight, string priority)
        {

            if (ParcelsList.Count == ParcelsList.Capacity)
            {
                Console.WriteLine("The amount of base station objects arrived to its maximum limit");
                return;
            }
            chackingIdentitiesOfParcel(id, senderId, targetId, droneId);
            WeightCategories weightCategories = (WeightCategories)int.Parse(weight);
            Priorities priorities = (Priorities)int.Parse(priority);
            Parcel parcel = new Parcel(id, senderId, targetId, weightCategories, priorities, droneId);
            IncreaseParcelIndex();
            ParcelsList.Add(parcel);
        }

    }
}
