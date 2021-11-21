﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IBL.BO;
using IDal.DO;



namespace IBL
{
    public partial class BL: IBL
    {
        //---------------------------------BaseStation GetList methods------------------------------------------------
       
        /// <summary>
        /// The function returns all the baseStationList items 
        /// (by converting the IDal.DO BaseStationList to BO baseStationList)
        /// </summary>
        public IEnumerable<BO.BaseStation> GetBOBaseStationsList()
        {
            List<BO.BaseStation> boBaseStationList = new List<BO.BaseStation>();
            List<IDal.DO.BaseStation> doBaseStationList = (List<IDal.DO.BaseStation>)dal.GetBaseStationsList();
            foreach (IDal.DO.BaseStation item in doBaseStationList)
            {
                boBaseStationList.Add(GetBLBaseStation(item.Id));
            }
            return boBaseStationList;
        }

        /// <summary>
        /// The function shows all the available charge slots
        /// </summary>
        public IEnumerable<BO.BaseStation> GetAvailableChargeSlots()
        {
            List<IDal.DO.BaseStation> doAvailableChargeSlots = (List<IDal.DO.BaseStation>)dal.AvailableChargeStations();
            List<BO.BaseStation> boAvailableChargeSlots = new List<BO.BaseStation>();
            foreach (IDal.DO.BaseStation item in doAvailableChargeSlots)
            {
                boAvailableChargeSlots.Add(GetBLBaseStation(item.Id));
            }

            return boAvailableChargeSlots;
        }


        /// <summary>
        /// the function returns a baseStationForList list
        /// by converting the BO.baseStation list to BaseStationForList type.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<BaseStationForList> GetBaseStationList()
        {
            List<BaseStationForList> baseStationForLists = new List<BaseStationForList>();
            List<BO.BaseStation> baseStations = (List<BO.BaseStation>)GetBOBaseStationsList();
            foreach (BO.BaseStation item in baseStations)
            {
                baseStationForLists.Add(GetBaseStationForList(item.Id));
            }
            return baseStationForLists;
        }

        //---------------------------------Drones GetList Methods------------------------------------------------

        public IEnumerable<DroneInCharging> GetDronesInMe(int stationId)
        {
            List<DroneInCharging> droneInCharging = null;
            foreach (DroneCharge droneCharge in dal.DronesChargingInMe(stationId))
            {
                DroneInCharging drone = new DroneInCharging(droneCharge.DroneId, 100);
                // לבדוק שבאמת הבטריה נהית 100
                droneInCharging.Add(drone);
            }
            return droneInCharging;
        }

        /// <summary>
        /// The function returns all the DronesLIst items 
        /// (by converting the IDal.DO DronesLIst to BO DronesLIst)
        /// </summary>
        public IEnumerable<BO.Drone> GetBODronesList()
        {
            List<BO.Drone> boDronesList = new List<BO.Drone>();
            List<IDal.DO.Drone> doDronseList = (List<IDal.DO.Drone>)dal.GetBaseStationsList();
            foreach (IDal.DO.Drone item in doDronseList)
            {
                boDronesList.Add(GetBLDrone(item.Id));
            }
            return boDronesList;
        }


        /// <summary>
        /// the function returns a droneForList
        /// by converting the BO.Drone to DroneForList type.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<DroneForList> GetDronesList()
        {
            List<DroneForList> droneForLists = new List<DroneForList>();
            List<BO.Drone> drones = (List<BO.Drone>)GetBOCustomersList();

            foreach (BO.Drone item in drones)
            {

                droneForLists.Add(GetDroneForList(item.Id));
            }
            return droneForLists;
        }


        // ---------------------------------Parcels GetList Methods------------------------------------------------

        /// <summary>
        /// The function returns all the ParcelsLIst items 
        /// (by converting the IDal.DO ParcelsLIst to BO ParcelsLIst)
        /// </summary>
        public IEnumerable<BO.Parcel> GetBOParcelsList()
        {
            List<BO.Parcel> boParcelList = new List<BO.Parcel>();
            List<IDal.DO.Parcel> doParcelList = (List<IDal.DO.Parcel>)dal.GetBaseStationsList();
            foreach (IDal.DO.Parcel item in doParcelList)
            {
                boParcelList.Add(GetBLParcel(item.Id));
            }
            return boParcelList;
        }

        /// <summary>
        /// the function returns a ParcelForList list
        /// by converting the BO.Parcel PArcelForList  type.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ParcelForList> GetParcelsList()
        {
            List<ParcelForList> parcelsForList = new List<ParcelForList>();
            List<BO.Parcel> parcels = (List<BO.Parcel>)GetBOParcelsList();

            foreach (BO.Parcel item in parcels)
            {
                parcelsForList.Add(GetParcelForList(item.Id));
            }
            return parcelsForList;
        }


        /// <summary>
        /// The function shows all the not associated parcels 
        /// </summary>
        public IEnumerable<BO.Parcel> GetNotAssociatedParcelsList()
        {
            List<BO.Parcel> boNotAssociatedParcelsList = new List<BO.Parcel>();
            List<IDal.DO.Parcel> doNotAccosiatedParcelsList = (List<IDal.DO.Parcel>)dal.NotAssociatedParcels();
            foreach (IDal.DO.Parcel item in doNotAccosiatedParcelsList)
            {
                boNotAssociatedParcelsList.Add(GetBLParcel(item.Id));
            }
            return boNotAssociatedParcelsList;
        }


        //---------------------------------Customers GetList Methods------------------------------------------------

        /// <summary>
        /// The function returns all the CustomersLIst items 
        /// (by converting the IDal.DO CustomersLIst to BO CustomersLIst)
        /// </summary>
        public IEnumerable<BO.Customer> GetBOCustomersList()
        {
            List<BO.Customer> boCustomerList = new List<BO.Customer>();
            List<IDal.DO.Customer> doCustomerList = (List<IDal.DO.Customer>)dal.GetBaseStationsList();
            foreach (IDal.DO.Customer item in doCustomerList)
            {
                boCustomerList.Add(GetBLCustomer(item.Id));
            }
            return boCustomerList;
        }
       
        /// <summary>
        /// The function returns List of ParcelInCustomer items for sender or target
        /// </summary>
        /// <param name="fromOrTo"> sender or target </param>
        /// <returns> List of ParcelInCustomer items </returns>
        public IEnumerable<ParcelInCustomer> GetParcelInCustomerList(FromOrTo fromOrTo , string id)
        {
            List<ParcelInCustomer> parcelInCustomer = new List<ParcelInCustomer>();
            List<IDal.DO.Parcel> ParcelList = (List<IDal.DO.Parcel>)dal.GetParcelsList();
            foreach (IDal.DO.Parcel parcel in ParcelList)
            {
                if((fromOrTo == FromOrTo.From && parcel.SenderId == id)  || (fromOrTo == FromOrTo.To && parcel.TargetId == id))
                {
                    parcelInCustomer.Add(ParcelInCustomerDOtOBO(parcel, fromOrTo));
                }
            }
            return parcelInCustomer;
        }

        /// <summary>
        /// the function returns a customerForLIst list
        /// by converting the BO.customero customerForLIst type.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<CustomerForList> GetCustomersList()
        {
            List<CustomerForList> customersForList = new List<CustomerForList>();
            List<BO.Customer> customers = (List<BO.Customer>)GetBOCustomersList();
            foreach (BO.Customer item in customers)
            {      
                customersForList.Add(GetCustomerForList(item.Id));
            }
            return customersForList;
        }

        
       
    }
}

    

