using System;
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
        /// The function returns all the baseStationList items 
        /// (by converting the IDal.DO BaseStationList to BO baseStationList)
        /// </summary>
        public IEnumerable<BO.BaseStation> GetBaseStationsList()
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
        /// The function returns all the ParcelsLIst items 
        /// (by converting the IDal.DO ParcelsLIst to BO ParcelsLIst)
        /// </summary>
        public IEnumerable<BO.Parcel> GetParcelsList()
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
        /// The function returns all the CustomersLIst items 
        /// (by converting the IDal.DO CustomersLIst to BO CustomersLIst)
        /// </summary>
        public IEnumerable<BO.Customer> GetCustomersList()
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
        /// The function returns all the DronesLIst items 
        /// (by converting the IDal.DO DronesLIst to BO DronesLIst)
        /// </summary>
        public IEnumerable<BO.Drone> GetDronesList()
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
    }
}

    

