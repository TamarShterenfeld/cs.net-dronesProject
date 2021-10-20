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

        public static void  AddingBaseStation(int myId, string name, double longitude, double latitude, int chrgeSlots)
        {
            if (IndexOfBaseStation >= BASESTATIONSAMOUNT)
            {
                Console.WriteLine("The amount of base stations objects arrived to its maximum limit");
                return;
            }
            if (searchBaseStation(myId) == -1)
            {
                Console.WriteLine("Can not add base station, this station's ID already exists ");
                return;
            }
            BaseStation baseStation = new BaseStation( myId, name, longitude, latitude, chrgeSlots);
            BaseStationsArr[IndexOfBaseStation] = baseStation;
            ++IndexOfBaseStation;
        }

        public static void AddingDrone(int id, string model, string status, string maxWeight, double battery)
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

        public static void AddingCustomer(ref string id, ref string name, ref string phone, ref double longitude, ref double latitude)
        {

            if (IndexOfCustomer >=CUSTOMERSAMOUNT)
            {
                Console.WriteLine("It is impossible to add a customer");
                return;
            }

            if (searchCustomer(id) == -1)
            {
                Console.WriteLine("Can not add customer, this customer ID already exists ");
                return;
            }

            Customer customer = new Customer(id = id, name, phone, longitude, latitude );
            customer.Id = id;
            customer.Phone = phone;
            customer.Name = name;
            customer.Longitude = longitude;
            customer.Latitude = latitude;
            CustomersArr[IndexOfCustomer] = customer;
            ++IndexOfCustomer;
        }

        public static void AddingParcel(int id, string senderId, string targetId, int droneId, string Weight, string Priority, DateTime Production, DateTime Association, DateTime PickingUp, DateTime Arrival)
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

            Parcel parcel = new Parcel();
            parcel.Id = id;
            parcel.SenderId = senderId;
            parcel.TargetId = targetId;
            parcel.DroneId = droneId;
            parcel.Weight = (WeightCategories)int.Parse(Weight);
            parcel.Priority = (Priorities)int.Parse(Priority);
            parcel.Production = Production;
            parcel.Association = Association;
            parcel.PickingUp = PickingUp;
            parcel.Arrival = Arrival;
           ParcelsArr[IndexOfParcel] = parcel;
            ++IndexOfParcel;
        }


        private static bool chackingIdentitiesOfParcel(int id, string senderId, string targetId, int droneId)
        {
            if (searchParcel(id) == -1)
            {
                Console.WriteLine("Can not add parcel, this parcel ID already exists ");
                return false;
            }

            if (searchCustomer(senderId) == -1 || searchCustomer(targetId) == -1)
            {
                Console.WriteLine("Can not add parcel, sender ID or target ID does not exist ");
                return false;
            }

            if (searchDrone(droneId) == -1)
            {
                Console.WriteLine("Can not add parcel, drone ID does not exist ");
                return false;
            }
            return true;
        }

    }
}
