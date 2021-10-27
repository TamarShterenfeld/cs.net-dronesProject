using System;
using System.Collections.Generic;
using System.Text;
using static IDAL.DO.IDAL;
using static DalObject.DataSource;
using static DalObject.DataSource.Config;


namespace DalObject
{
    public partial class DalObject
    {
        /// <summary>
        /// The function shows all the base stations in the BaseStations' list
        /// </summary>
        public static void ShowBaseStationsList()
        {
            foreach (BaseStation item in BaseStationsList)
            {
                DisplayBaseStation(item.Id);
            }
            if (BaseStationsList.Count == 0) Console.WriteLine("There are no base stations to show");
        }

        /// <summary>
        /// The function shows all the drones in the drones' list
        /// </summary>
        public static void ShowDronesList()
        {
            foreach (Drone item in DronesList)
            {
                DisplayDrone(item.Id);
            }
            if (DronesList.Count == 0) Console.WriteLine("There are no drones to show");
        }

        /// <summary>
        /// The function shows all the customers in the customers' list
        /// </summary>
        public static void ShowCustomersList()
        {
            foreach (Customer item in CustomersList)
            {
                DisplayCustomer(item.Id);
            }
            if (CustomersList.Count == 0) Console.WriteLine("There are no customers to show");
        }

        /// <summary>
        /// The function shows all the parcels in the parcels' list
        /// </summary>
        public static void ShowParcelsList()
        {
            foreach (Parcel item in ParcelsList)
            {
                DisplayParcel(item.Id);
            }
            if (ParcelsList.Count == 0) Console.WriteLine("There are no parcels to show");
        }

        /// <summary>
        /// The function shows all the not associated parcels 
        /// </summary>
        public static void ShowNotAssociatedParcelsList()
        {
            Parcel[] notAssociatedParcelsList = GettingNotAssociatedParcels();
            foreach (Parcel item in notAssociatedParcelsList)
            {
                DisplayParcel(item.Id);
            }
            if (notAssociatedParcelsList.Length == 0) Console.WriteLine("There are no not associated parcels to show");
        }

        /// <summary>
        /// The function shows all the available charge slots
        /// </summary>
        public static void AvailableChargeSlots()
        {
            BaseStation[] availableChargeSlots = GettingAvailableChargeSlots();
            foreach (BaseStation item in availableChargeSlots)
            {
                DisplayBaseStation(item.Id);
            }
            if (availableChargeSlots.Length == 0) Console.WriteLine("There are no not available charge slots to show");

        }
    }
}
        


