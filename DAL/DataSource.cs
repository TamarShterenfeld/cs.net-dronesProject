using IDAL.DO;
using System;
using System.Collections.Generic;
using System.Text;
using static IDAL.DO.IDAL;


namespace DalObject
{
    public class DataSource
    {
        //const literals.
        internal const int DRONESAMOUNT = 10;
        internal const int BASESTATIONSAMOUNT = 5;
        internal const int CUSTOMERSAMOUNT = 100;
        internal const int PARCELAMOUNT = 1000;
        internal const int INITALIZBASESTATIONSIZE = 10;

        //internal arrs of different entities.
        internal static Drone[] DronesArr = new Drone[DRONESAMOUNT];
        internal static BaseStation[] BaseStationsArr = new BaseStation[BASESTATIONSAMOUNT];
        internal static Customer[] CustomersArr = new Customer[CUSTOMERSAMOUNT];
        internal static Parcel[] ParcelsArr = new Parcel[PARCELAMOUNT];
        internal static List<DroneCharge> DroneChargeList = new List<DroneCharge>();

        //arrays of different data - for initalizing object of the structures.
        static string[] droneModels = { "FPV COMBO", "COMBO AIR", "MAVIC AIR", "DJI TELLO", "MAVIC MINI 2", "SWING", "PHANTOM", "MATRICE 200", "DJI AGRAS T16", "MAVIC 2 ENTERPRISE", "MATRICE 210 RTK" };
        static string[] baseStationsNames = { "Dubek", "Gan Varsha", "Machon Mor", "Ben Gurion Jabotinsky", "Chazon Ish Desler", "Coca Cola", "Ezra Nechemia", "Migdal Hamaim", "Marom Neve", "Pinat Hazol", "Jabotinsky Sokolov" };
        static string[] customerName = { "Tami", "Gili", "Yael", "Shira", "Michal", "Shulamit", "Reut", "Sarit", "Ruti", "Chani", "Chavi" };


        //a static random field - for general use.
        public static Random rand = new Random();

        /// <summary>
        /// the method Initalize initalizes all the Config class fields.
        /// </summary>
        public static void Initialize()
        {
            int size;
            //initalize at least the two first item in BaseStationArr.
            size = rand.Next(2, BASESTATIONSAMOUNT);
            for (int i = 0; i < size; i++)
            {
                BaseStationsArr[i] = new BaseStation();
                BaseStationsArr[i].Id = Config.IndexOfBaseStation++;
                BaseStationsArr[i].Name = baseStationsNames[rand.Next(0, INITALIZBASESTATIONSIZE - 1)];
                BaseStationsArr[i].ChargeSlots = rand.Next(0, 5);
                //the latitude & longitude values are displayed in degrees.
                BaseStationsArr[i].Latitude = 0.8 * rand.Next(1, 360);
                BaseStationsArr[i].Longitude = 0.9 * rand.Next(1, 360);
            }


            //initalize at least the first five drones in DronesArr
            size = rand.Next(5, DRONESAMOUNT);
            int status, weight;
            for (int i = 0; i < size; i++)
            {
                DronesArr[i] = new Drone();
                DronesArr[i].Id = Config.IndexOfDrone++;
                status = rand.Next(0, Enum.GetNames(typeof(DroneStatuses)).Length - 1);
                DronesArr[i].Status = (DroneStatuses)Enum.GetNames(typeof(DroneStatuses)).GetValue(status);
                DronesArr[i].Model = droneModels[rand.Next(0, INITALIZBASESTATIONSIZE)];
                weight = rand.Next(0, Enum.GetNames(typeof(WeightCategories)).Length);
                DronesArr[i].MaxWeight = (WeightCategories)Enum.GetNames(typeof(WeightCategories)).GetValue(weight);
                DronesArr[i].Battery = 0.6 * rand.Next(0, 100) + 0.4 * rand.Next(0, 100);
            }

            //initalize at least the first tenth customers in CustomerArr.
            size = rand.Next(10, CUSTOMERSAMOUNT);
            for (int i = 0; i < size; i++)
            {
                CustomersArr[i] = new Customer();
                CustomersArr[i].Id = (Config.IndexOfCustomer++).ToString();
                CustomersArr[i].Name = customerName[rand.Next(0, INITALIZBASESTATIONSIZE)];
                CustomersArr[i].Phone = "0" + (rand.Next(100000000, 999999999)).ToString();
                //the latitude & longitude values are displayed in degrees.
                BaseStationsArr[i].Latitude = 0.6 * rand.Next(1, 360);
                BaseStationsArr[i].Longitude = 0.99 * rand.Next(1, 360);
            }

            //initalize at least the first tenth parcels in ParcelArr.
            size = rand.Next(10, PARCELAMOUNT);
            int priority, weight1, senderIndex, targetIndex;
            for (int i = 0; i < size; i++)
            {
                ParcelsArr[i] = new Parcel();
                ParcelsArr[i].Id = Config.IndexOfParcel++;
                senderIndex = rand.Next(0, Config.IndexOfCustomer - 1);
                ParcelsArr[i].SenderId = CustomersArr[senderIndex].Id;
                targetIndex = rand.Next(0, Config.IndexOfCustomer - 1);
                //the man who sends the parcel can't also get it.
                while (targetIndex == senderIndex) { targetIndex = rand.Next(0, Config.IndexOfCustomer - 1); }
                ParcelsArr[i].TargetId = CustomersArr[targetIndex].Id;
                priority = rand.Next(0, Enum.GetNames(typeof(Priorities)).Length - 1);
                ParcelsArr[i].Priority = (Priorities)Enum.GetNames(typeof(Priorities)).GetValue(priority);
                weight1 = rand.Next(0, Enum.GetNames(typeof(WeightCategories)).Length - 1);
                ParcelsArr[i].Weight = (WeightCategories)Enum.GetNames(typeof(WeightCategories)).GetValue(weight1);
                ParcelsArr[i].DroneId = DronesArr[rand.Next(0, Config.IndexOfDrone - 1)].Id;
                //initalize (random) a date of Production & the other DateTime fields are based on it.
                //while assuming that each part of the shipment process maximum takes 14 business days.
                DateTime start = new DateTime(2021, 1, 1);
                int random = (DateTime.Today - start).Days;
                ParcelsArr[i].Production = start.AddDays(rand.Next(random));
                ParcelsArr[i].Association = ParcelsArr[i].Production.AddDays(rand.Next(14));
                ParcelsArr[i].PickingUp = ParcelsArr[i].Association.AddDays(rand.Next(14));
                ParcelsArr[i].Arrival = ParcelsArr[i].PickingUp.AddDays(rand.Next(14));
            }
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

}
