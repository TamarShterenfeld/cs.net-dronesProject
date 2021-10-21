using IDAL.DO;
using System;
using System.Collections.Generic;
using System.Text;
using static IDAL.DO.IDAL;

namespace DalObject
{
    public partial class DalObject
    {

        public void AddingBaseStation(int id, string name, double longitude, double latitude, int chrgeSlots)
        {

            if (DataSource.Config.IndexOfBaseStation >= DataSource.BASESTATIONSAMOUNT)
            {
                Console.WriteLine("It is impossible to add a base station");
                return;
            }

            if (searchBaseStation(id) == -1)
            {
                Console.WriteLine("Can not add a base station, this station ID already exists ");
                return;
            }

            BaseStation baseStation = new BaseStation()
            {
                Id = id, Name = name,  Longitude = longitude, Latitude = latitude, ChargeSlots = chrgeSlots
            };

            DataSource.BaseStationsArr[DataSource.Config.IndexOfBaseStation] = baseStation;
            ++DataSource.Config.IndexOfBaseStation;
        }

        public void AddingDrone(int id, string model, string maxWeight, double battery)
        {

            if (DataSource.Config.IndexOfDrone >= DataSource.DRONESAMOUNT)
            {
                Console.WriteLine("It is impossible to add a drone");
                return;
            }

            if (searchDrone(id) == -1)
            {
                Console.WriteLine("Can not add a drone, this drone ID already exists ");
                return;
            }

            Drone drone = new Drone()
            {
                Id = id, Model = model, Status = DroneStatuses.Available, MaxWeight = (WeightCategories)int.Parse(maxWeight), Battery = battery
            };
            
            DataSource.DronesArr[DataSource.Config.IndexOfDrone] = drone;
            ++DataSource.Config.IndexOfDrone;
        }

        public  void AddingCustomer(ref string id, ref string name, ref string phone, ref double longitude, ref double latitude)
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

            Customer customer = new Customer()
            {
                Id = id, Phone = phone, Name = name, Longitude = longitude, Latitude = latitude
            };
            
            DataSource.CustomersArr[DataSource.Config.IndexOfCustomer] = customer;
            ++DataSource.Config.IndexOfCustomer;
        }

        public void AddingParcel(int id, string senderId, string targetId, int droneId, string Weight, string Priority)
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

            Parcel parcel = new Parcel()
            {
                Id = id, SenderId = senderId, TargetId = targetId, DroneId = droneId, Weight = (WeightCategories)int.Parse(Weight),
                Priority = (Priorities)int.Parse(Priority),Production = DateTime.Now
            };
            
            DataSource.ParcelsArr[DataSource.Config.IndexOfParcel] = parcel;
            ++DataSource.Config.IndexOfParcel;
        }


        private bool chackingIdentitiesOfParcel(int id, string senderId, string targetId, int droneId)
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
