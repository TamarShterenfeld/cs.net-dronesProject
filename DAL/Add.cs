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

            BaseStationsList.First(item => item.Id == id);
            BaseStation baseStation = new BaseStation(id, name, longitude, latitude, chrgeSlots);
            BaseStationsList[IndexOfBaseStation] = baseStation;
            if (IndexOfBaseStation + 1 >= BASESTATIONSAMOUNT)
            {
                throw new Exception("The amount of base stations objects arrived to its maximum limit");
            }
            ++IndexOfBaseStation;
            BaseStationsList.Add(baseStation);
        }

        public static void AddDrone(int id, string model, string status, string maxWeight, double battery)
        {

            foreach (Drone item in DronesList)
            {
                if (item.Id == id)
                    throw new Exception("This drone already exists!");
            }
            DroneStatuses droneStatuses = (DroneStatuses)int.Parse(status);
            WeightCategories weightCategory = (WeightCategories)int.Parse(maxWeight);
            if (IndexOfDrone + 1 >= DRONESAMOUNT)
            {
                throw new Exception("The amount of drones objects arrived to its maximum limit");
            }
            ++IndexOfDrone;
            Drone drone = new Drone(id, battery, model, droneStatuses, weightCategory);
            DronesList.Add(drone);
        }

        public static void AddCustomer(string id, string name, string phone, double longitude, double latitude)
        {
            foreach (Customer item in CustomersList)
            {
                if (item.Id == id)
                    throw new Exception("This customer already exists!");
            }
            if (IndexOfCustomer + 1 >= CUSTOMERSAMOUNT)
            {
                throw new Exception("The amount of customers objects arrived to its maximum limit");
            }
            Customer customer = new Customer(id, name, phone, longitude, latitude);
            ++IndexOfCustomer;
            CustomersList.Add(customer);
        }

        public static void AddParcel(int id, string senderId, string targetId, int droneId, string Weight, string Priority)
        {

            if (IndexOfParcel + 1 >= BASESTATIONSAMOUNT)
            {
                Console.WriteLine("It is impossible to add a parcel");
                return;
            }
            chackingIdentitiesOfParcel(id, senderId, targetId, droneId);
            WeightCategories weightCategories = (WeightCategories)int.Parse(Weight);
            Priorities priorities = (Priorities)int.Parse(Priority);
            Parcel parcel = new Parcel(id, senderId, targetId, droneId, weightCategories, priorities);
            ParcelsList[IndexOfParcel] = parcel;
            ++IndexOfParcel;
        }

    }
}
