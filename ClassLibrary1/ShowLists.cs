using System;
using System.Collections.Generic;
using System.Text;
using static IDAL.DO.OverloadException;
using static DalObject.DataSource;
using IDAL.DO;

namespace IBL
{
    public partial class BL : IBL
    {
        /// <summary>
        /// The function shows all the base stations in the BaseStations' list
        /// </summary>
        public void ShowBaseStationsList()
        {
            List<BaseStation> baseStationsList = (List<BaseStation>)dal.GetBaseStationsList();
            foreach (BaseStation item in baseStationsList)
            {
                 DisplayBaseStation(item.Id);
            }
            if (baseStationsList.Count == 0) Console.WriteLine("There are no base stations to show");
        }

        /// <summary>
        /// The function shows all the drones in the drones' list
        /// </summary>
        public void ShowDronesList()
        {
            List<Drone> dronesList = (List<Drone>)dal.GetBaseStationsList();
            foreach (Drone item in dronesList)
            {
                GetDrone(item.Id);
            }
            if (dronesList.Count == 0) Console.WriteLine("There are no drones to show");
        }

        /// <summary>
        /// The function shows all the customers in the customers' list
        /// </summary>
        public void ShowCustomersList()
        {
            List<Customer> customersList = (List<Customer>)dal.GetCustomersList();
            foreach (Customer item in customersList)
            {
                DisplayCustomer(item.Id);
            }
            if (customersList.Count == 0) Console.WriteLine("There are no customers to show");
        }

        /// <summary>
        /// The function shows all the parcels in the parcels' list
        /// </summary>
        public void ShowParcelsList()
        {
            List<Parcel> parcelsList = (List<Parcel>)dal.GetParcelsList();
            foreach (Parcel item in parcelsList)
            {
                DisplayParcel(item.Id);
            }
            if (parcelsList.Count == 0) Console.WriteLine("There are no parcels to show");
        }

        /// <summary>
        /// The function shows all the not associated parcels 
        /// </summary>
        public void ShowNotAssociatedParcelsList()
        {
            List<Parcel> notAssociatedParcelsList = (List<Parcel>)dal.NotAssociatedParcels();
            foreach (Parcel item in notAssociatedParcelsList)
            {
                DisplayParcel(item.Id);
            }
            if (notAssociatedParcelsList.Count == 0) Console.WriteLine("There are no not associated parcels to show");
        }

        /// <summary>
        /// The function shows all the available charge slots
        /// </summary>
        public void AvailableChargeSlots()
        {
            List<BaseStation> availableChargeSlots = (List<BaseStation>)dal.AvailableChargeStations();
            foreach (BaseStation item in availableChargeSlots)
            {
                DisplayBaseStation(item.Id);
            }
            if (availableChargeSlots.Count == 0) Console.WriteLine("There are no not available charge slots to show");

        }
    }
    
}



