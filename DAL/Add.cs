using IDAL.DO;
using System;
using System.Collections.Generic;
using System.Text;
using static IDAL.DO.IDAL;
using static DalObject.DataSource.Config;
using static DalObject.DataSource;

namespace DalObject
{
    public partial class DalObject
    {

        public static void AddBaseStation(int id, string name, double longitude, double latitude, int chrgeSlots)
        {
            if (IndexOfBaseStation >= BASESTATIONSAMOUNT)
            {
                Console.WriteLine("The amount of base stations objects arrived to its maximum limit");
                return;
            }
            if (searchBaseStation(id) == -1)
            {
                Console.WriteLine("Can not add base station, this station's ID already exists ");
                return;
            }
            BaseStation baseStation = new BaseStation(id, name, longitude, latitude, chrgeSlots);
            BaseStationsArr[IndexOfBaseStation] = baseStation;
            ++IndexOfBaseStation;
        }

        public static void AddDrone(int id, string model, string status, string maxWeight, double battery)
        {
            if (IndexOfDrone >= DRONESAMOUNT)
            {
                Console.WriteLine("The amount of drones objects arrived to its maximum limit");
                return;
            }
            if (searchDrone(id) == -1)
            {
                Console.WriteLine("Can not add drone, this drone's ID already exists ");
                return;
            }
            DroneStatuses droneStatuses = (DroneStatuses)int.Parse(status);
            WeightCategories weightCategory = (WeightCategories)int.Parse(maxWeight); ;
            Drone drone = new Drone(id, battery, model, droneStatuses, weightCategory);
            DronesArr[IndexOfDrone] = drone;
            ++IndexOfDrone;
        }

        public static void AddCustomer( string id,  string name,  string phone,  double longitude, double latitude)
        {

            if (IndexOfCustomer >= CUSTOMERSAMOUNT)
            {
                Console.WriteLine("It is impossible to add a customer");
                return;
            }
            if (searchCustomer(id) == -1)
            {
                Console.WriteLine("Can not add customer, this customer ID already exists ");
                return;
            }
            Customer customer = new Customer(id, name, phone, longitude, latitude);
            CustomersArr[IndexOfCustomer] = customer;
            ++IndexOfCustomer;
        }

        public static void AddParcel(int id, string senderId, string targetId, int droneId, string Weight, string Priority)
        {

            if (IndexOfParcel >= BASESTATIONSAMOUNT)
            {
                Console.WriteLine("It is impossible to add a parcel");
                return;
            }
            if (!chackingIdentitiesOfParcel(id, senderId, targetId, droneId))
            {
                return;
            }
            WeightCategories weightCategories = (WeightCategories)int.Parse(Weight);
            Priorities priorities = (Priorities)int.Parse(Priority);
            Parcel parcel = new Parcel(id, senderId, targetId, droneId, weightCategories, priorities);
            ParcelsArr[IndexOfParcel] = parcel;
            ++IndexOfParcel;
        }
        
    }
}
