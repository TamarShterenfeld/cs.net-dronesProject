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
        internal const int DRONESAMOUNT = 10;
        internal const int BASESTATIONSAMOUNT = 5;
        internal const int CUSTOMERSAMOUNT = 100;
        internal const int PARCELAMOUNT = 1000;
        internal const int INITALIZBASESTATIONSIZE = 10;

        //internal lists of different entities.
        internal static List<Drone> DronesList = new List<Drone>(DRONESAMOUNT);
        internal static List<BaseStation> BaseStationsList = new List<BaseStation>(BASESTATIONSAMOUNT);
        internal static List<Customer> CustomersList = new List<Customer>(CUSTOMERSAMOUNT);
        internal static List<Parcel> ParcelsList = new List<Parcel>(PARCELAMOUNT);
        internal static List<DroneCharge> DroneChargeList = new List<DroneCharge>();

        //arrays of different data - for initalizing object of the structures.
        static readonly string[] droneModels = { "FPV COMBO", "COMBO AIR", "MAVIC AIR", "DJI TELLO", "MAVIC MINI 2", "SWING", "PHANTOM", "MATRICE 200", "DJI AGRAS T16", "MAVIC 2 ENTERPRISE", "MATRICE 210 RTK" };
        static readonly string[] baseStationsNames = { "Dubek", "Gan Varsha", "Machon Mor", "Ben Gurion Jabotinsky", "Chazon Ish Desler", "Coca Cola", "Ezra Nechemia", "Migdal Hamaim", "Marom Neve", "Pinat Hazol", "Jabotinsky Sokolov" };
        static readonly string[] customerName = { "Tami", "Gili", "Yael", "Shira", "Michal", "Shulamit", "Reut", "Sarit", "Ruti", "Chani", "Chavi" };


        //a static random field - for general use.
        public static Random rand = new Random();

        /// <summary>
        /// the method Initalize initalizes all the Config class fields.
        /// </summary>
        public static void Initialize()
        {
            int size;
            //initalize at least the two first item in BaseStationList.
            size = rand.Next(2, BASESTATIONSAMOUNT);
            for (int i = 0; i < size; i++)
            {
                BaseStation baseStation = new BaseStation();
                baseStation.Id = IndexOfBaseStation++;
                baseStation.Name = baseStationsNames[rand.Next(0, INITALIZBASESTATIONSIZE - 1)];
                baseStation.ChargeSlots = rand.Next(0, 5);
                //the latitude & longitude values are displayed in degrees.
                baseStation.Latitude = 0.8 * rand.Next(1, 360);
                baseStation.Longitude = 0.9 * rand.Next(1, 360);
                BaseStationsList.Add(baseStation);
            }

            //initalize at least the first five drones in DronesList
            size = rand.Next(5, DRONESAMOUNT);
            int statusIndex, weightIndex;
            for (int i = 0; i < size; i++)
            {
                Drone drone = new Drone();
                drone.Id = IndexOfDrone++;
                statusIndex = rand.Next(0, Enum.GetNames(typeof(DroneStatuses)).Length - 1);
                drone.Status = DroneStatuses.Available;
                drone.Model = droneModels[rand.Next(0, INITALIZBASESTATIONSIZE)];
                weightIndex = rand.Next(0, Enum.GetNames(typeof(WeightCategories)).Length);
                drone.MaxWeight = (WeightCategories)Enum.Parse(typeof(WeightCategories), (string)Enum.GetNames(typeof(WeightCategories)).GetValue(weightIndex), true);
                drone.Battery = 0.6 * rand.Next(0, 100) + 0.4 * rand.Next(0, 100);
                DronesList.Add(drone);
            }

            //initalize at least the first tenth customers in CustomerList
            size = rand.Next(10, CUSTOMERSAMOUNT);
            for (int i = 0; i < size; i++)
            {
                Customer customer = new Customer();
                customer.Id = rand.Next(100000000, 999999999).ToString();
                customer.Name = customerName[rand.Next(0, INITALIZBASESTATIONSIZE)];
                customer.Phone = "0" + (rand.Next(100000000, 999999999)).ToString();
                //the latitude & longitude values are displayed in degrees.
                customer.Latitude = 0.6 * rand.Next(1, 360);
                customer.Longitude = 0.99 * rand.Next(1, 360);
                IndexOfCustomer++;
                CustomersList.Add(customer);
            }

            //initalize at least the first tenth parcels in ParcelList.
            size = rand.Next(10, PARCELAMOUNT);
            int priorityIndex, senderIndex, targetIndex;
            for (int i = 0; i < size; i++)
            {
               
                Parcel parcel = new Parcel();
                parcel.Id = AddParcelIndex();
                senderIndex = rand.Next(0, IndexOfCustomer-1);
                parcel.SenderId = CustomersList[senderIndex].Id;
                targetIndex = rand.Next(0, IndexOfCustomer - 1);
                //a man who sends the parcel can't also get it.
                while (targetIndex == senderIndex) { targetIndex = rand.Next(0, IndexOfCustomer); }
                parcel.TargetId = CustomersList[targetIndex].Id;
                priorityIndex = rand.Next(0, Enum.GetNames(typeof(Priorities)).Length - 1);
                parcel.Priority = (Priorities)Enum.Parse(typeof(Priorities),(string)Enum.GetNames(typeof(Priorities)).GetValue(priorityIndex), true);
                weightIndex = rand.Next(0, Enum.GetNames(typeof(WeightCategories)).Length - 1);
                parcel.Weight = (WeightCategories)Enum.Parse(typeof(WeightCategories), (string)Enum.GetNames(typeof(WeightCategories)).GetValue(weightIndex), true);
                parcel.DroneId = -1;
                for (int j = 0; j < DronesList.Count; j++)
                {
                    Drone currdrone =  DronesList[j];
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
                parcel.PickingUp = parcel.Association.AddDays(rand.Next(14)).AddHours(rand.Next(1, 24));
                parcel.Arrival = parcel.PickingUp.AddDays(rand.Next(14)).AddHours(rand.Next(1, 24));
                //there wasn't an available drone.
                //the date: 01/ 01/ 0001 - is a sign for an unassociated parcel - a default value.
                if (parcel.DroneId == -1)
                {
                    parcel.Association = new DateTime(01 / 01 / 0001)  ;
                }
                ParcelsList.Add(parcel);
            }
        }

        /// <summary>
        /// the class Config contains the indexes of the first free place in the different lists.
        /// in addition, it contains ParcelId
        /// </summary>
        internal class Config
        {
            internal static int IndexOfDrone = 0;
            internal static int IndexOfBaseStation = 0;
            internal static int IndexOfCustomer = 0;
            internal static int IndexOfParcel = 0;
            public static int ParcelId = 0;
        }
    }
}
