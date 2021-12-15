using IDal.DO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;



namespace DalObject
{


    /// <summary>
    /// a class which contains all the data of program.
    /// most of them are randomaled by healthy logic.
    /// </summary>
    public class DataSource
    {
        //const literals.
        internal const int DRONES_BASE_AMOUNT = 10;
        internal const int BASESTATIONS_BASE_AMOUNT = 5;
        internal const int CUSTOMERS_BASE_AMOUNT = 100;
        internal const int PARCELS_BASE_AMOUNT = 1000;


        //internal lists of different entities.
        internal static List<Drone> DronesList = new(DRONES_BASE_AMOUNT);
        internal static List<BaseStation> BaseStationsList = new (BASESTATIONS_BASE_AMOUNT);
        internal static List<Customer> CustomersList = new (CUSTOMERS_BASE_AMOUNT);
        internal static List<Parcel> ParcelsList = new (PARCELS_BASE_AMOUNT);
        internal static List<DroneCharge> DronesChargeList = new();

        //arrays of different data - for initalizing object of the structures.
        static readonly string[] droneModels = { "FPV COMBO", "COMBO AIR", "MAVIC AIR", "DJI TELLO", "MAVIC MINI 2", "SWING", "PHANTOM", "MATRICE 200", "DJI AGRAS T16", "MAVIC 2 ENTERPRISE", "MATRICE 210 RTK" };
        static readonly string[] baseStationsNames = { "Dubek", "Gan Varsha", "Machon Mor", "Ben Gurion Jabotinsky", "Chazon Ish Desler", "Coca Cola", "Ezra Nechemia", "Migdal Hamaim", "Marom Neve", "Pinat Hazol", "Jabotinsky Sokolov" };
        static readonly string[] customerName = { "Tami", "Gili", "Yael", "Shira", "Michal", "Shulamit", "Reut", "Sarit", "Ruti", "Chani", "Chavi" };


        //a static random field - for general use.
        public static readonly Random rand = new();

        /// <summary>
        /// the method Initalize initalizes all thedifferent lists (besides DronesChargeList).
        /// </summary>
        public static void Initialize()
        {
            //initalize at least the two first item in BaseStationList.        
            RandomBaseStation();

            //initalize at least the first five drones in DronesList
            RandomDrones();

            //initalize at least the first tenth customers in CustomerList        
            RandomCustomers();

            //initalize at least the first tenth parcels in ParcelList.            
            RandomParcels();
        }

        /// <summary>
        /// the class Config contains the indexes of the first free place in the different lists.
        /// in addition, it contains ParcelId
        /// </summary>
        internal static class Config
        {
            public static int ParcelId = 0;
            public static double ElectricityConsumingOfAvailable = 10;
            public static double ElectricityConsumingOfLightWeight = 20;
            public static double ElectricityConsumingOfAverageWeight = 30;
            public static double ElectricityConsumingOfHeavyWeight = 40;
            public static double ChargeRate = 25;
        }


        /// <summary>
        /// randoms at least the two first objects in the BaseStationList.
        /// </summary>
        private static void RandomBaseStation()
        {
            int size = rand.Next(2, BASESTATIONS_BASE_AMOUNT);
            for (int i = 1; i < size+1; i++)
            {
                BaseStation baseStation = new ();
                baseStation.Id = i;
                baseStation.Name = RandomBaseStationName();
                baseStation.ChargeSlots = RandomChargeSlot();
                baseStation.Longitude = RandomLongitude();
                baseStation.Latitude = RandomLatitude();
                BaseStationsList.Add(baseStation);
            }

        }

        //public delegate void Action(Drone drone);
        /// <summary>
        /// randoms at least the first five object in DronesList.
        /// </summary>
        private static void RandomDrones()
        {
            int size = rand.Next(5, DRONES_BASE_AMOUNT);

            for (int i = 1; i < size+1; i++)
            {
                Drone drone = new();
                drone.Id = i;
                drone.Model = RandomModel();
                drone.MaxWeight = RandomWeight();
                DronesList.Add(drone);


            }

        }

        /// <summary>
        /// randoms at least the tenth first customers in CustomersList.
        /// </summary>
        private static void RandomCustomers()
        {
            int size = rand.Next(10, CUSTOMERS_BASE_AMOUNT);
            for (int i = 0; i < size; i++)
            {
                Customer customer = new();
                customer.Id = RandomId();
                customer.Name = RandomCustomerName();
                customer.Phone = RandomPhone();
                customer.Longitude = RandomLongitude();
                customer.Latitude = RandomLatitude();
                CustomersList.Add(customer);

            }

        }

        //random at least the first tenth parcels in ParcelsList.
        private static void RandomParcels()
        {
            int size = rand.Next(10, PARCELS_BASE_AMOUNT);
            for (int i = 0; i < size; i++)
            {
                Parcel parcel = new();
                parcel.Id = DalObject.IncreaseParcelIndex();
                parcel.SenderId = RandomCustomerId();
                parcel.TargetId = RandomCustomerId();
                parcel.Priority = RandomPriority();
                parcel.Weight = RandomWeight();
                parcel.DroneId = 0;
                for (int j = 0; j < DronesList.Count; j++)
                {
                    Drone currdrone = DronesList[j];
                    parcel.DroneId = currdrone.Id;
                    DronesList[j] = currdrone;
                }
                //initalize (random) a date of ProductionDate & the other DateTime fields are based on it.
                //while assuming that each part of the shipment process maximum takes 14 business days.
                parcel.ProductionDate = DateTime.Now;
                parcel.AssociationDate = parcel.AssociationDate != null? parcel.ProductionDate.Value.AddDays(rand.Next(14)).AddHours(rand.Next(1, 24)) : null;
                parcel.PickUpDate = parcel.PickUpDate != null ? parcel.AssociationDate.Value.AddDays(rand.Next(14)).AddHours(rand.Next(1, 24)) : null;
                parcel.SupplyDate = parcel.SupplyDate!= null? parcel.PickUpDate.Value.AddDays(rand.Next(14)).AddHours(rand.Next(1, 24)) : null; 
                //there wasn't an available drone.
                //the date: 01/ 01/ 0001 - is a sign for an unassociated parcel - a default value.
                if (parcel.DroneId == -1)
                {
                    parcel.AssociationDate = null;
                }
                ParcelsList.Add(parcel);

            }
        }

        /// <summary>
        /// randoms a longitude value of a Coordinate object.
        /// </summary>
        /// <returns>a coordinate object which stores a random longitude .</returns>
        private static Coordinate RandomLongitude()
        {
            double longitude1 = 0.4321 * rand.Next(0, 180) + 0.5679* rand.Next(-180, 0);
            Coordinate longitude = new() { InputCoorValue = longitude1, MyLocation = Locations.Longitude };
            longitude.CastDoubleToCoordinante();
            return longitude;
        }

        /// <summary>
        /// randoms a latitude value of a Coordinate object.
        /// </summary>
        /// <returns>a coordinate object which stores a random latitude.</returns>
        private static Coordinate RandomLatitude()
        {
            double latitude1 = 0.1234 * rand.Next(0, 180) + 0.8766 * rand.Next(-180, 0);
            Coordinate latitude = new () { InputCoorValue = latitude1, MyLocation = Locations.Latitude };
            latitude.CastDoubleToCoordinante();
            return latitude;
        }

        /// <summary>
        /// random a WeightCategory value.
        /// </summary>
        /// <returns>a randomaled WeightCategory value.</returns>
        private static WeightCategories RandomWeight()
        {
            WeightCategories weightIndex = (WeightCategories)rand.Next(1, Enum.GetNames(typeof(WeightCategories)).Length +1);
            return weightIndex;
        }

        /// <summary>
        /// random a Priority value.
        /// </summary>
        /// <returns>a randomaled Priority value.</returns>
        private static Priorities RandomPriority()
        {
            Priorities priority = (Priorities)rand.Next(1, Enum.GetNames(typeof(Priorities)).Length +1);
            return priority;
        }

        /// <summary>
        /// random a name from the baseStations' names array.
        /// </summary>
        /// <returns>a random baseStation's name.</returns>
        private static string RandomBaseStationName()
        {
            return baseStationsNames[rand.Next(0, baseStationsNames.Length - 1)];
        }


        /// <summary>
        /// randoms a customer's name from the customers' names array.
        /// </summary>
        /// <returns>a random customer's name.</returns>
        private static string RandomCustomerName()
        {
            return customerName[rand.Next(0, customerName.Length - 1)];
        }

        /// <summary>
        /// randoms a model from the models arr.
        /// </summary>
        /// <returns>a random drone's model name.</returns>
        private static string RandomModel()
        {
            return droneModels[rand.Next(0, droneModels.Length - 1)];
        }

        /// <summary>
        /// randoms a valid phone number.
        /// </summary>
        /// <returns>a random string of a phone number.</returns>
        private static string RandomPhone()
        {
            return "0" + (rand.Next(100000000, 999999999)).ToString();
        }

        /// <summary>
        /// randoms a valid Id.
        /// </summary>
        /// <returns>a random string id</returns>
        private static string RandomId()
        {
            return rand.Next(100000000, 999999999).ToString();
        }

        /// <summary>
        /// randoms an Id of one of the customers in CustomersList.
        /// </summary>
        /// <returns>a random customer id - from the customersList's names</returns>
        private static string RandomCustomerId()
        {
            return CustomersList[rand.Next(0, CustomersList.Count - 1)].Id;
        }

        /// <summary>
        /// randoms a chargeSlot value.
        /// </summary>
        /// <returns>a randomal chargeSlots in range of (0,10)</returns>
        private static int RandomChargeSlot()
        {
            return rand.Next(0, 10);
        }
    }
}




