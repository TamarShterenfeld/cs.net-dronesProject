using IDAL.DO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using static IDAL.DO.OverloadException;
using static IBL.BO.DalObject;
using IBL.BO;
using static IBL.BO.DataSource;


namespace IBL
{
    namespace BO
    {

        /// <summary>
        /// a class which contains all the data of program.
        /// </summary>
        public class DataSource
        {
            //const literals.
            internal const int DRONES_BASE_AMOUNT = 10;
            internal const int BASESTATIONS_BASE_AMOUNT = 5;
            internal const int CUSTOMERS_BASE_AMOUNT = 100;
            internal const int PARCELS_BASE_AMOUNT = 1000;


            //internal lists of different entities.
            internal static List<Drone> DronesList = new List<Drone>(DRONES_BASE_AMOUNT);
            internal static List<BaseStation> BaseStationsList = new List<BaseStation>(BASESTATIONS_BASE_AMOUNT);
            internal static List<Customer> CustomersList = new List<Customer>(CUSTOMERS_BASE_AMOUNT);
            internal static List<Parcel> ParcelsList = new List<Parcel>(PARCELS_BASE_AMOUNT);
            internal static List<DroneCharge> DroneChargeList = new List<DroneCharge>();

            //arrays of different data - for initalizing object of the structures.
            static readonly string[] droneModels = { "FPV COMBO", "COMBO AIR", "MAVIC AIR", "DJI TELLO", "MAVIC MINI 2", "SWING", "PHANTOM", "MATRICE 200", "DJI AGRAS T16", "MAVIC 2 ENTERPRISE", "MATRICE 210 RTK" };
            static readonly string[] baseStationsNames = { "Dubek", "Gan Varsha", "Machon Mor", "Ben Gurion Jabotinsky", "Chazon Ish Desler", "Coca Cola", "Ezra Nechemia", "Migdal Hamaim", "Marom Neve", "Pinat Hazol", "Jabotinsky Sokolov" };
            static readonly string[] customerName = { "Tami", "Gili", "Yael", "Shira", "Michal", "Shulamit", "Reut", "Sarit", "Ruti", "Chani", "Chavi" };


            //a static random field - for general use.
            public static Random rand = new Random();

            /// <summary>
            /// the method Initalize initalizes all thedifferent lists (besides DroneChargeList).
            /// </summary>
            public  void Initialize()
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
                public static double ElectricityConsumingOfAvailable = 0;
                public static double ElectricityConsumingOfLightWeight = 0;
                public static double ElectricityConsumingOfAverageWeight = 0;
                public static double ElectricityConsumingOfHeavyWeight = 0;
                public double ChargingRate = 0;
            }


            /// <summary>
            /// randoms at least the two first objects in the BaseStationList.
            /// </summary>
            private void randomBaseStation()
            {
                int size = rand.Next(2, BASESTATIONS_BASE_AMOUNT);
                for (int i = 0; i < size; i++)
                {
                    BaseStation baseStation = new BaseStation();
                    baseStation.Id = i;
                    baseStation.Name = randomBaseStationName();
                    baseStation.ChargeSlots = randomChargeSlot();
                    baseStation.Location = randomLocation();
                    BaseStationsList.Add(baseStation);
                }

            }


            /// <summary>
            /// randoms at least the first five object in DronesList.
            /// </summary>
            private  void randomDrones()
            {
                int size = rand.Next(5, DRONES_BASE_AMOUNT);

                for (int i = 0; i < size; i++)
                {
                    Drone drone = new Drone();
                    drone.Id = i;
                    drone.Model = randomModel();
                    drone.MaxWeight = randomWeight();
                    DronesList.Add(drone);
                }

            }

            /// <summary>
            /// randoms at least the tenth first customers in CustomersList.
            /// </summary>
            private void randomCustomers()
            {
                int size = rand.Next(10, CUSTOMERS_BASE_AMOUNT);
                for (int i = 0; i < size; i++)
                {
                    Customer customer = new Customer();
                    customer.Id = randomId();
                    customer.Name = randomCustomerName();
                    customer.Phone = randomPhone();
                    //the latitude & longitude values are displayed in degrees.
                    customer.Location = randomLocation();
                    CustomersList.Add(customer);
                }

            }

            //random at least the first tenth parcels in ParcelsList.
            private void randomParcels()
            {
                int size = rand.Next(10, PARCELS_BASE_AMOUNT);
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
                        parcel.DroneId = currdrone.Id;
                        DronesList[j] = currdrone; 
                    }
                    //initalize (random) a date of Production & the other DateTime fields are based on it.
                    //while assuming that each part of the shipment process maximum takes 14 business days.
                    parcel.Production = DateTime.Now;
                    parcel.Association = parcel.Production.AddDays(rand.Next(14)).AddHours(rand.Next(1, 24));
                    parcel.PickingUp = parcel.Association.AddDays(rand.Next(14)).AddHours(rand.Next(1, 24)); ;
                    parcel.Supplied = parcel.PickingUp.AddDays(rand.Next(14)).AddHours(rand.Next(1, 24)); ;
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
            /// random a location object
            /// </summary>
            /// <returns></returns>
            private Location randomLocation()
            {
                Coordinate Longitude = new Coordinate(0.7 * rand.Next(0, 180) + 0.3 * rand.Next(-180, 0), Locations.Longitude);
                Coordinate Latitude = new Coordinate(0.3 * rand.Next(0, 180) + 0.7 * rand.Next(-180, 0), Locations.Latitude);
                return new Location(Longitude, Latitude);
            }
            /// <summary>
            /// random a WeightCategory value.
            /// </summary>
            /// <returns></returns>
            private WeightCategories randomWeight()
            {
                int weightIndex = rand.Next(0, Enum.GetNames(typeof(WeightCategories)).Length);
                return (WeightCategories)Enum.Parse(typeof(WeightCategories), (string)Enum.GetNames(typeof(WeightCategories)).GetValue(weightIndex), true);
            }

            /// <summary>
            /// random a Priority value.
            /// </summary>
            /// <returns></returns>
            private Priorities randomPriority()
            {
                int priorityIndex = rand.Next(0, Enum.GetNames(typeof(WeightCategories)).Length);
                return (Priorities)Enum.Parse(typeof(Priorities), (string)Enum.GetNames(typeof(Priorities)).GetValue(priorityIndex), true);
            }

            /// <summary>
            /// random a double type of a battery.
            /// </summary>
            /// <returns></returns>

            /// <summary>
            /// random a name from the baseStations' names array.
            /// </summary>
            /// <returns></returns>
            private string randomBaseStationName()
            {
                return baseStationsNames[rand.Next(0, baseStationsNames.Length - 1)];
            }


            /// <summary>
            /// random a customer's name from the customers' names array.
            /// </summary>
            /// <returns></returns>
            private string randomCustomerName()
            {
                return customerName[rand.Next(0, customerName.Length - 1)];
            }

            /// <summary>
            /// random a model from the models arr.
            /// </summary>
            /// <returns></returns>
            private string randomModel()
            {
                return droneModels[rand.Next(0, droneModels.Length - 1)];
            }

            /// <summary>
            /// random a valid phone number.
            /// </summary>
            /// <returns></returns>
            private string randomPhone()
            {
                return "0" + (rand.Next(100000000, 999999999)).ToString();
            }

            /// <summary>
            /// random a valid Id.
            /// </summary>
            /// <returns></returns>
            private string randomId()
            {
                return rand.Next(100000000, 999999999).ToString();
            }

            /// <summary>
            /// random an Id of one of the customers in CustomersList.
            /// </summary>
            /// <returns></returns>
            private string randomCustomerId()
            {
                return CustomersList[rand.Next(0, CustomersList.Count - 1)].Id;
            }

            /// <summary>
            /// random a chargeSlot value.
            /// </summary>
            /// <returns></returns>
            private int randomChargeSlot()
            {
                return rand.Next(0, 5);
            }
        }
    }
}



