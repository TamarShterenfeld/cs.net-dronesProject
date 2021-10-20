using IDAL.DO;
using System;
using System.Collections.Generic;
using System.Text;
using static IDAL.DO.IDAL;

namespace DalObject
{
    public partial class DalObject
    {

        public static void  AddingBaseStation(int id, string name, double longitude, double latitude, int chrgeSlots)
        {

            if (DataSource.Config.IndexOfBaseStation >= DataSource.BASESTATIONSAMOUNT)
            {
                Console.WriteLine("It is impossible to add a base station");
                return;
            }

            if (searchBaseStation(id) == -1)
            {
                Console.WriteLine("Can not add base station, this station ID already exists ");
                return;
            }

            BaseStation baseStation = new BaseStation();
            baseStation.Id = id;
            baseStation.Name = name;
            baseStation.Longitude = longitude;
            baseStation.Latitude = latitude;
            baseStation.ChargeSlots = chrgeSlots;
            DataSource.BaseStationsArr[DataSource.Config.IndexOfBaseStation] = baseStation;
            ++DataSource.Config.IndexOfBaseStation;
        }

        public static void AddingDrone(int id, string model, string status, string maxWeight, double battery)
        {

            if (DataSource.Config.IndexOfDrone >= DataSource.DRONESAMOUNT)
            {
                Console.WriteLine("It is impossible to add a drone");
                return;
            }

            if (searchDrone(id) == -1)
            {
                Console.WriteLine("Can not add drone, this drone ID already exists ");
                return;
            }

            Drone drone = new Drone();
            drone.Id = id;
            drone.Model = model;
            drone.Status = (DroneStatuses)int.Parse(status);
            drone.MaxWeight = (WeightCategories)int.Parse(maxWeight);
            drone.Battery = battery;
            DataSource.DronesArr[DataSource.Config.IndexOfDrone] = drone;
            ++DataSource.Config.IndexOfDrone;
        }

        public static void AddingCustomer(ref string id, ref string name, ref string phone, ref double longitude, ref double latitude)
        {

            if (DataSource.Config.IndexOfCustomer >= DataSource.CUSTOMERSAMOUNT)
            {
                Console.WriteLine("It is impossible to add a customer");
                return;
            }

            if (searchCustomer(id) == -1)
            {
                Console.WriteLine("Can not add customer, this customer ID already exists ");
                return;
            }

            Customer customer = new Customer();
            customer.Id = id;
            customer.Phone = phone;
            customer.Name = name;
            customer.Longitude = longitude;
            customer.Latitude = latitude;
            DataSource.CustomersArr[DataSource.Config.IndexOfCustomer] = customer;
            ++DataSource.Config.IndexOfCustomer;
        }

        public static void AddingParcel(int id, string senderId, string targetId, int droneId, string Weight, string Priority, DateTime Production, DateTime Association, DateTime PickingUp, DateTime Arrival)
        {

            if (DataSource.Config.IndexOfParcel >= DataSource.BASESTATIONSAMOUNT)
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
            DataSource.ParcelsArr[DataSource.Config.IndexOfParcel] = parcel;
            ++DataSource.Config.IndexOfParcel;
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

        //public static void DroneDetails(ref int id, ref double battery, ref string model, ref IDAL.DO.DroneStatuses status, ref IDAL.DO.WeightCategories maxWeight)
        //{
        //    Console.WriteLine("Enter drone's details : id, battery, model, status, maxWeight.");
        //    id = int.Parse(Console.ReadLine());
        //    battery = id = int.Parse(Console.ReadLine());
        //    model = Console.ReadLine();
        //    //status = Console.ReadLine();
        //    //maxWeight = Console.ReadLine();
        //}

        //public static void PackageDetails(ref int id, ref string name, ref double longitude, ref double latitude, ref int chargeSlots)
        //{
        //    Console.WriteLine("Enter base station's details : id, name, longitude, latitude, number of chargeSlots.");
        //    id = int.Parse(Console.ReadLine());
        //    name = Console.ReadLine();
        //    longitude = int.Parse(Console.ReadLine());
        //    latitude = int.Parse(Console.ReadLine());
        //    chargeSlots = int.Parse(Console.ReadLine());
        //}

    }
}
