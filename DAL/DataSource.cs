using IDAL.DO;
using System;
using System.Collections.Generic;
using System.Text;
using static IDAL.DO.IDAL;
using static DalObject.DataSource.Config;
using static DalObject.DalObject;
using System.Linq;


namespace DalObject
{
    /// <summary>
    /// a class which contains all the data of program.
    /// </summary>
    public class DataSource
    {
        //const literals.
        internal const int DRONESBASEAMOUNT = 10;
        internal const int BASESTATIONSBASEAMOUNT = 5;
        internal const int CUSTOMERSBASEAMOUNT = 100;
        internal const int PARCELBASEAMOUNT = 1000;
        internal const int BASESTATIONARRSIZE = 10;

        //internal lists of different entities.
        internal static List<Drone> DronesList = new List<Drone>(DRONESBASEAMOUNT);
        internal static List<BaseStation> BaseStationsList = new List<BaseStation>(BASESTATIONSBASEAMOUNT);
        internal static List<Customer> CustomersList = new List<Customer>(CUSTOMERSBASEAMOUNT);
        internal static List<Parcel> ParcelsList = new List<Parcel>(PARCELBASEAMOUNT);
        internal static List<DroneCharge> DroneChargeList = new List<DroneCharge>();

        //arrays of different data - for initalizing object of the structures.
        static readonly string[] droneModels = { "FPV COMBO", "COMBO AIR", "MAVIC AIR", "DJI TELLO", "MAVIC MINI 2", "SWING", "PHANTOM", "MATRICE 200", "DJI AGRAS T16", "MAVIC 2 ENTERPRISE", "MATRICE 210 RTK" };
        static readonly string[] baseStationsNames = { "Dubek", "Gan Varsha", "Machon Mor", "Ben Gurion Jabotinsky", "Chazon Ish Desler", "Coca Cola", "Ezra Nechemia", "Migdal Hamaim", "Marom Neve", "Pinat Hazol", "Jabotinsky Sokolov" };
        static readonly string[] customerName = { "Tami", "Gili", "Yael", "Shira", "Michal", "Shulamit", "Reut", "Sarit", "Ruti", "Chani", "Chavi" };


        //a static random field - for general use.
        public static Random rand = new Random();
        //a static field of class Coordinate - for displaying longitude & latitude in sexagesimal base. 
        public static DAL.Coordinate coordinate = new DAL.Coordinate();

        /// <summary>
        /// the method Initalize initalizes all thedifferent lists (besides DroneChargeList).
        /// </summary>
        public static void Initialize()
        {
            //initalize at least the two first item in BaseStationList.        
            randomBaseStation();

            //initalize at least the first five drones in DronesList
            randomDrones();

            //initalize at least the first tenth customers in CustomerList        
            randomCustomers();

            //initalize at least the first tenth parcels in ParcelList.            
            randomParcels();
        }

        /// <summary>
        /// the class Config contains the indexes of the first free place in the different lists.
        /// in addition, it contains ParcelId
        /// </summary>
        internal class Config
        { 
            public static int ParcelId = 0;
        }


        /// <summary>
        /// randoms at least the two first objects in the BaseStationList.
        /// </summary>
        private static void randomBaseStation()
        {
            int size = rand.Next(2, BASESTATIONSBASEAMOUNT);
            for (int i = 0; i < size; i++)
            {
                BaseStation baseStation = new BaseStation();
                baseStation.Id = i;
                baseStation.Name = randomName();
                baseStation.ChargeSlots =randomChargeSlot();
                //the latitude & longitude values are displayed in degrees.
                baseStation.Latitude = randomLatitudeOrLongitude();
                baseStation.Longitude = randomLatitudeOrLongitude();
                BaseStationsList.Add(baseStation);
            }

        }


        /// <summary>
        /// randoms at least the first five object in DronesList.
        /// </summary>
        private static void randomDrones()
        {
            int size = rand.Next(5, DRONESBASEAMOUNT);

            for (int i = 0; i < size; i++)
            {
                Drone drone = new Drone();
                drone.Id = i;
                drone.Status = DroneStatuses.Available;
                drone.Model = randomModel();
                drone.MaxWeight = randomWeight();
                drone.Battery = randomBattery();
                DronesList.Add(drone);
            }

        }

        /// <summary>
        /// randoms at least the tenth first customers in CustomersList.
        /// </summary>
        private static void randomCustomers()
        {
            int size = rand.Next(10, CUSTOMERSBASEAMOUNT);
            for (int i = 0; i < size; i++)
            {
                Customer customer = new Customer();
                customer.Id = randomId();
                customer.Name = randomName();
                customer.Phone = randomPhone();
                //the latitude & longitude values are displayed in degrees.
                customer.Latitude = randomLatitudeOrLongitude();
                customer.Longitude = randomLatitudeOrLongitude();
                CustomersList.Add(customer);
            }

        }

        //random at least the first tenth parcels in ParcelsList.
        private static void randomParcels()
        {
            int size = rand.Next(10, PARCELBASEAMOUNT);
            for (int i = 0; i < size; i++)
            {
                Parcel parcel = new Parcel();
                parcel.Id = IncreaseParcelIndex();
                parcel.SenderId = randomCustomerId();
                parcel.TargetId = randomCustomerId();
                parcel.Priority = randomPriority();
                parcel.Weight = randomWeight();
                parcel.DroneId = -1;
                for (int j = 0; j < DronesList.Count; j++)
                {
                    Drone currdrone = DronesList[j];
                    if (currdrone.Status == DroneStatuses.Available)
                    {
                        parcel.DroneId = currdrone.Id;
                        currdrone.Status = DroneStatuses.Shipment;
                        //from an unknown reason the changes aren't done - so that the changes will be kept we have to remove the object and add it again(including the changes)
                        DronesList.RemoveAt(j);
                        DronesList.Insert(j, currdrone);
                        break;
                    }
                }
                //initalize (random) a date of Production & the other DateTime fields are based on it.
                //while assuming that each part of the shipment process maximum takes 14 business days.
                parcel.Production = DateTime.Now;
                parcel.Association = parcel.Production.AddDays(rand.Next(14)).AddHours(rand.Next(1, 24));
                parcel.PickingUp = parcel.Association.AddDays(rand.Next(14)).AddHours(rand.Next(1, 24)); ;
                parcel.Arrival = parcel.PickingUp.AddDays(rand.Next(14)).AddHours(rand.Next(1, 24)); ;
                //there wasn't an available drone.
                //the date: 01/ 01/ 0001 - is a sign for an unassociated parcel - a default value.
                if (parcel.DroneId == -1)
                {
                    parcel.Association = new DateTime(01 / 01 / 0001);
                }
                ParcelsList.Add(parcel);
            }
        }

        /// <summary>
        /// random a double type latitude or longitude
        /// </summary>
        /// <returns></returns>
        private static double randomLatitudeOrLongitude()
        {
            return 0.7* rand.Next(-180, 0) + 0.3 * rand.Next(0, 180);
        }

        /// <summary>
        /// random a WeightCategory value.
        /// </summary>
        /// <returns></returns>
        private static WeightCategories randomWeight()
        {
            int weightIndex = rand.Next(0, Enum.GetNames(typeof(WeightCategories)).Length);
            return (WeightCategories)Enum.Parse(typeof(WeightCategories), (string)Enum.GetNames(typeof(WeightCategories)).GetValue(weightIndex), true);
        }

        /// <summary>
        /// random a Priority value.
        /// </summary>
        /// <returns></returns>
        private static Priorities randomPriority()
        {
            int priorityIndex = rand.Next(0, Enum.GetNames(typeof(WeightCategories)).Length);
            return (Priorities)Enum.Parse(typeof(Priorities), (string)Enum.GetNames(typeof(Priorities)).GetValue(priorityIndex), true);
        }

        /// <summary>
        /// random a double type of a battery.
        /// </summary>
        /// <returns></returns>
        private static double randomBattery()
        {
            return 0.6 * rand.Next(0, 100) + 0.4 * rand.Next(0, 100);
        }

        /// <summary>
        /// random a name from the names arr.
        /// </summary>
        /// <returns></returns>
        private static string randomName()
        {
            return baseStationsNames[rand.Next(0, BASESTATIONARRSIZE - 1)];
        }

        /// <summary>
        /// random a model from the models arr.
        /// </summary>
        /// <returns></returns>
        private static string randomModel()
        {
            return droneModels[rand.Next(0, BASESTATIONARRSIZE)];
        }

        /// <summary>
        /// random a valid phone number.
        /// </summary>
        /// <returns></returns>
        private static string  randomPhone()
        {
            return "0" + (rand.Next(100000000, 999999999)).ToString();
        }

        /// <summary>
        /// random a valid Id.
        /// </summary>
        /// <returns></returns>
        private static string randomId()
        {
            return rand.Next(100000000, 999999999).ToString();
        }

        /// <summary>
        /// random an Id of one of the customers in CustomersList.
        /// </summary>
        /// <returns></returns>
        private static string randomCustomerId()
        {
           return  CustomersList[rand.Next(0, CustomersList.Count-1)].Id;
        }

        /// <summary>
        /// random a chargeSlot value.
        /// </summary>
        /// <returns></returns>
        private static int randomChargeSlot()
        {
            return rand.Next(0, 5);
        }
    }
}



