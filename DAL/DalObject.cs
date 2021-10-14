using System;
using System.Collections.Generic;
using System.Text;
using static IDAL.DO.IDAL;
using IDAL.DO;

namespace DalObject
{

    public class DataSource
    {
        //const literals.
        internal const int DRONESAMOUNT = 10;
        internal const int BASESTATIONSAMOUNT = 5;
        internal const int CUSTOMERSAMOUNT = 100;
        internal const int PARCELAMOUNT = 1000;

        //internal arrs of different entities.
        internal static Drone[] DronesArr = new Drone[DRONESAMOUNT];
        internal static BaseStation[] BaseStationsArr = new BaseStation[BASESTATIONSAMOUNT];
        internal static Customer[] CustomersArr = new Customer[CUSTOMERSAMOUNT];
        internal static Parcel[] ParcelsArr = new Parcel[PARCELAMOUNT];

        //a static random field - for general use.
        public static Random rand = new Random();
        
        /// <summary>
        /// the method Initalize initalizes all the Config class fields.
        /// </summary>
        public static void Initialize()
        {
            int size;
            //initalize at least the two first item in BaseStationArr.
            size =  rand.Next(2, BASESTATIONSAMOUNT);
            for (int i = 0; i < size; i++)
            {
                BaseStationsArr[i] = new BaseStation();
            }
            Config.IndexOfBaseStation = size-1;

            //initalize at least the first five drones in DronesArr
            size = rand.Next(5, DRONESAMOUNT);
            int status;
            for (int i = 0; i<size; i++)
            {
                //initalize a status for each item in DronesArr.
                status = rand.Next(1, Enum.GetNames(typeof(DroneStatuses)).Length);
                DronesArr[i] = new Drone();
                DronesArr[i].Status = (DroneStatuses)Enum.GetNames(typeof(DroneStatuses)).GetValue(status-1);
            }
            Config.IndexOfDrone = size-1;

            //initalize at least the first tenth customers.
            size = rand.Next(10, CUSTOMERSAMOUNT);
            for(int i =0; i<size; i++)
            {
                CustomersArr[i] = new Customer();
            }
            Config.IndexOfCustomer = size-1;

            //initalize at least the first tenth parcels.
            size = rand.Next(10, PARCELAMOUNT);
            int priority;
            for(int i =0;i<size; i++)
            {
                priority = rand.Next(1, Enum.GetNames(typeof(Priorities)).Length);
                ParcelsArr[i] = new Parcel();
                ParcelsArr[i].Priority = (Priorities)Enum.GetNames(typeof(Priorities)).GetValue(priority);
            }
            Config.IndexOfParcel = size;

            //initalize parcelId for the first instance
            Config.ParcelId = rand.Next(100, 200);
        }

        /// <summary>
        /// the class Config contains the indexes of the first free place in the arrays
        /// in addition, it contains ParcelId
        /// </summary>
        internal class Config
        {
            internal static int IndexOfDrone = 0;
            internal static int IndexOfBaseStation = 0;
            internal static int IndexOfCustomer = 0;
            internal static int IndexOfParcel = 0;
            public static int ParcelId;
        }

    }

    public class DalObject
    {
        // constructor
        public DalObject()
        {
            DataSource.Initialize();
        }

        public int AddingParcel()
        {
            return DataSource.Config.ParcelId++;
        }


        public static void CreatingBaseStation(int id, string name, double longitude, double latitude, int chrgeSlots)
        {

            if (DataSource.Config.IndexOfBaseStation >= DataSource.BASESTATIONSAMOUNT)
            {
                Console.WriteLine("It is impossible to add a base station");
                return;
            }

            if (SearchBaseStation(id) == -1)
            {
                Console.WriteLine("Can not add base station, this station ID already exists ");
                return;
            }

            BaseStation baseStation = new BaseStation();
            baseStation.Id = id;
            baseStation.Name = name;
            baseStation.Longitude = longitude;
            baseStation.Latitude = latitude;
            baseStation.ChrgeSlots = chrgeSlots;
            DataSource.BaseStationsArr[DataSource.Config.IndexOfBaseStation] = baseStation;
            ++DataSource.Config.IndexOfBaseStation;
        }

        

        public static void CreatingDrone(int id, string model, DroneStatuses status, WeightCategories maxWeight, double battery)
        {

            if (DataSource.Config.IndexOfDrone >= DataSource.DRONESAMOUNT)
            {
                Console.WriteLine("It is impossible to add a drone");
                return;
            }

            if(SearchDrone(id) == -1)
            {
                Console.WriteLine("Can not add drone, this drone ID already exists ");
                return;
            }

            Drone drone = new Drone();
            drone.Id = id;
            drone.Model = model;
            drone.Status = status;
            drone.MaxWeight = maxWeight;
            drone.Battery = battery;
            DataSource.DronesArr[DataSource.Config.IndexOfDrone] = drone;
            ++DataSource.Config.IndexOfDrone;
        }

        public static void CreatingCustomer(ref string id, ref string name, ref string phone, ref double longitude, ref double latitude)
        {

            if (DataSource.Config.IndexOfCustomer >= DataSource.CUSTOMERSAMOUNT)
            {
                Console.WriteLine("It is impossible to add a customer");
                return;
            }

            if(SearchCustomer(id) == -1)
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

        public static void CreatingParcel(int id ,string senderId , string targetId , int droneId , WeightCategories Weight, Priorities Priority, DateTime Production, DateTime Association, DateTime PickingUp, DateTime Arrival)
        {

            if (DataSource.Config.IndexOfParcel >= DataSource.BASESTATIONSAMOUNT)
            {
                Console.WriteLine("It is impossible to add a parcel");
                return;
            }

            if(!ChackingParcel( id,  senderId,  targetId,  droneId))
            {
                return;
            }

            Parcel parcel = new Parcel();
            parcel.Id = id;
            parcel.SenderId = senderId;
            parcel.TargetId = targetId;
            parcel.DroneId = droneId;
            parcel.Weight = Weight;
            parcel.Priority = Priority;
            parcel.Production = Production;
            parcel.Association = Association;
            parcel.PickingUp = PickingUp;
            parcel.Arrival = Arrival;
            DataSource.ParcelsArr[DataSource.Config.IndexOfParcel] = parcel;
            ++DataSource.Config.IndexOfParcel;
        }

        private static bool ChackingParcel(int id, string senderId, string targetId, int droneId)
        {
            if(SearchParcel(id) == -1)
            {
                Console.WriteLine("Can not add parcel, this parcel ID already exists ");
                return false;
            }

            if(SearchCustomer(senderId) == -1 || SearchCustomer(targetId) == -1)
            {
                Console.WriteLine("Can not add parcel, sender ID or target ID does not exist ");
                return false;
            }

            if(SearchDrone(droneId) == -1)
            {
                Console.WriteLine("Can not add parcel, drone ID does not exist ");
                return false;
            }
            return true;
        }


        public static int SearchBaseStation(int id)
        {
            int index = -1;
            for (int i = 0 ; i < DataSource.Config.IndexOfBaseStation ; ++i)
            {
                if (DataSource.BaseStationsArr[i].Id == id)
                {
                    index = i;
                }
            }
            return index;
        }

        public static int SearchDrone(int id)
        {
            int index = -1;
            for (int i = 0; i < DataSource.Config.IndexOfDrone; ++i)
            {
                if (DataSource.DronesArr[i].Id == id)
                {
                    index = i;
                }
            }
            return index;
        }

        public static int SearchCustomer(string id)
        {
            int index = -1;
            for (int i = 0; i < DataSource.Config.IndexOfCustomer; ++i)
            {
                if (DataSource.CustomersArr[i].Id == id)
                {
                    index = i;
                }
            }
            return index;
        }

        public static int SearchParcel(int id)
        {
            int index = -1;
            for (int i = 0; i < DataSource.Config.IndexOfParcel; ++i)
            {
                if (DataSource.ParcelsArr[i].Id == id)
                {
                    index = i;
                }
            }
            return index;
        }


        public static void DisplayBaseStation(int index)
        {
            if(index == -1)
            {
                Console.WriteLine("Base station ID does not exist ");
                return;
            }
            Console.WriteLine($"id: {DataSource.BaseStationsArr[DataSource.Config.IndexOfBaseStation].Id} \n" +
                              $"name: {DataSource.BaseStationsArr[DataSource.Config.IndexOfBaseStation].Name} \n" +
                              $"longitude: {DataSource.BaseStationsArr[DataSource.Config.IndexOfBaseStation].Longitude}\n" +
                              $"latitude:  {DataSource.BaseStationsArr[DataSource.Config.IndexOfBaseStation].Latitude}\n"+
                              $"number of chrge slots: {DataSource.BaseStationsArr[DataSource.Config.IndexOfBaseStation].ChrgeSlots}\n");
        }

        public static void DisplayDrone(int index)
        {
            if (index == -1)
            {
                Console.WriteLine("Base station ID does not exist ");
                return;
            }
            Console.WriteLine($"id: {DataSource.DronesArr[DataSource.Config.IndexOfDrone].Id} \n" +
                              $"model: {DataSource.DronesArr[DataSource.Config.IndexOfDrone].Model} \n" +
                              $"status: {DataSource.DronesArr[DataSource.Config.IndexOfDrone].Status}\n" +
                              $"maxWeight:  {DataSource.DronesArr[DataSource.Config.IndexOfDrone].MaxWeight}\n" +
                              $"battery: {DataSource.DronesArr[DataSource.Config.IndexOfDrone].Battery}\n");
        }

       
    }
}

