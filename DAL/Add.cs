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

        public static void AddBaseStation(int id, string name, double longitude, double latitude, int chrgeSlots)
        {
            //can add the base station just if the input id stil doesnt exist in the Base Station's list.
            if(BaseStationsList.FindIndex(item => item.Id == id) == -1)
            {
                //the Base Station List is full totally.
                if (IndexOfBaseStation + 1 >= BASESTATIONSAMOUNT)
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

        public static void AddDrone(int id, string model, string maxWeight, double battery)
        {
            //drones list is fulll already.
            if (IndexOfDrone + 1 >= DRONESAMOUNT)
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

        public static void AddCustomer(string id, string name, string phone, double longitude, double latitude)
        {
            //customers list is full already.
            if (IndexOfCustomer + 1 >= CUSTOMERSAMOUNT)
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

        public static void AddParcel(int id, string senderId, string targetId, int droneId, string Weight, string Priority)
        {

            if (IndexOfParcel + 1 >= BASESTATIONSAMOUNT)
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
