using System;
using System.Collections.Generic;
using System.Text;
using IDal.DO;

namespace IDal
{
    public interface IDal
    {
        /// <summary>
        /// the function adds a base station to the BaseStationsList.
        /// </summary>
        /// <param name="baseStation">an object of a base station - for adding to the BaseStationsList</param>
        void Add(BaseStation baseStation);

        
        /// <summary>
        /// the function adds a drone to the DronesList.
        /// </summary>
        /// <param name="drone">an object of a drone - for adding to the DronesList</param>
        void Add(Drone drone);

        /// <summary>
        /// the function adds a droneCharge drone to the DronesChargeList.
        /// </summary>
        /// <param name="droneId">an id of a drone</param>
        ///  <param name="baseStationId">an id of a baseStationId</param>
        void Add(int droneCharge, int baseStationId);

        /// <summary>
        /// the function adds a customer to the CustomersList.
        /// </summary>
        /// <param name="customer">an object of a customer - for adding to the CustomersList</param>
        /// 
        void Add(Customer customer);

        /// <summary>
        /// the function adds a parcel to the ParcelsList.
        /// </summary>
        /// <param name="parcel">an object of a parcel - for adding to the ParcelsList</param> 
        void Add(Parcel parcel);

        /// <summary>
        /// associates a parcel thet its id is as parcelId
        /// to the drone that its id is as droneId
        /// </summary>
        /// <param name="parcelId">like that, it's possible to identity the requested parcel </param>
        /// <param name="droneId">like that, it's possible to identity the requested drone </param>
        public void UpdateDrone(Drone drone, int id);
        public void UpdateBaseStation(BaseStation baseStation, int id);
        public void UpdateCustomer(Customer customer, string id);
        void AssociateParcel(int parcelId, int droneId);

        /// <summary>
        /// chnges the requested parcel pickUpDate to DateTime.Now.
        /// </summary>
        /// <param name="parcelId">like that, it's possible to identity the requested parcel</param>
        void PickupParcel(int parcelId);

        /// <summary>
        /// sends the requsted drone to recharge in the requested base station.
        /// </summary>
        /// <param name="droneId">like that, it's possible to identity the requested drone</param>
        /// <param name="baseStationId">like that, it's possible to identity the requested base station</param>
        void SendDroneToRecharge(int droneId, int baseStationId);

        /// <summary>
        /// release the requested drone from the base station it has been charged till now.
        /// </summary>
        /// <param name="droneId">like that, it's possible to identity the requested drone</param>
        void ReleaseDroneFromRecharge(int droneId);

        /// <summary>
        /// getts a specific base station by its id
        /// </summary>
        /// <param name="requestedId">the base station's id</param>
        /// <returns>the found BaseStation object</returns>
        BaseStation GetBaseStation(int requestedId);

        /// <summary>
        /// getts a specific drone by its id
        /// </summary>
        /// <param name="requestedId">the drone's id</param>
        /// <returns>the found Drone object</returns>
        Drone GetDrone(int requestedId);

        /// <summary>
        /// getts a specific customer by its id
        /// </summary>
        /// <param name="requestedId">the customer's id</param>
        /// <returns>the found Customer object</returns>
        Customer GetCustomer(string requestedId);

        /// <summary>
        /// getts a specific parcel by its id
        /// </summary>
        /// <param name="requestedId">the parcel's id</param>
        /// <returns>the found Parcel object</returns>
        Parcel GetParcel(int requestedId);

        /// <summary>
        /// the functions getts the max amount of childrent,                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                 
        /// </summary>
        /// <returns>the baseStationList </returns>
        IEnumerable<BaseStation> GetBaseStationsList();

        /// <summary>
        /// getters the dronesList.
        /// </summary>
        /// <returns>the dronesList</returns>
        IEnumerable<Drone> GetDronesList();

        /// <summary>
        /// getters the customersList
        /// </summary>
        /// <returns>the customersList</returns>
        IEnumerable<Customer> GetCustomersList();

        /// <summary>
        /// getters the parcelsList.
        /// </summary>
        /// <returns>the list - parcelsList.</returns>
        IEnumerable<Parcel> GetParcelsList();

        /// <summary>
        /// getters the DronesChargeList.
        /// </summary>
        /// <returns>the list - DronesChargeList.</returns>
        IEnumerable<DroneCharge> GetDronesCharge();

        /// <summary>
        /// creates a list which contains all the parcels that are not accosiated to any drone.
        /// </summary.
        /// <returns>the list that is build during solving the oroblem.</returns>
        IEnumerable<Parcel> NotAssociatedParcels();

        /// <summary>
        /// builds a base station list containing only the base station which have available chargeSlots
        /// </summary>
        /// <returns>the created list is returned</returns> 
        IEnumerable<BaseStation> AvailableChargeStations();

        /// <summary>
        /// computes the amount of the available chargeSlots in the baseStation with the id like the parameter.
        /// </summary>
        /// <param name="baseStationId">the id of the base sattion we want to check.</param>
        /// <returns></returns>
        int AvailableChargeSlots(int baseStationId);

        /// <summary>
        /// creats a list which contains all the drones that are charged in the current baseStation,
        /// </summary>
        /// <param name="stationId"> an id of the baseStation</param>
        /// <returns>a list that contains all the drones {</returns>
        IEnumerable<int> GetDronesIdInBaseStation(int requestedId);

        /// <summary>
        /// creats a lists that contains all the drones that are charged in the currStation.
        /// </summary>
        /// <param name="stationId"></param>
        /// <returns>a list which contauns all the droneCharge objects 
        /// which are charged in the station that its id is equal to the id we got as a parameter. </returns>
        IEnumerable<DroneCharge> DronesChargingInMe(int stationId);

        /// <summary>
        /// a double array which contains all the electricities consuming by a drone + the charge rate.
        /// </summary>
        /// <returns></returns>
        double[] ElectricityConsuming();
    }
}
