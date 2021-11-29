using System;
using System.Collections.Generic;
using System.Linq;
using IBL.BO;
using static IBL.BL;
using DAL.DO;
using DalObject;


namespace IBL
{
    /// <summary>
    /// an interface that forces the inherit classes to imlement the methods it contains.
    /// the methods are connected to BL class.
    /// </summary>
    interface IBL : IBaseStationBL, ICustomerBL, IDroneBL, IParcelBL
    {
        /// <summary>
        /// The function gives associate date to the parcel.
        /// </summary>
        /// <param name="parcelId">parcel id</param>
        /// <param name="droneId">drone id</param>
        void AssociateParcel(int droneId);
        void PickUpParcel(int droneId);
        void SupplyParcel(int droneId);
        void SendDroneForCharge(int droneId);
        void ReleaseDroneFromRecharge(int droneId, double timeCharge);


    }

    /// <summary>
    /// an interface that forces the inherit classes to imlement the methods it contains.
    /// the methods are connected to actoions of baseStations
    /// </summary>
    interface IBaseStationBL
    {
        /// <summary>
        /// The function adds a base station to the BaseStationsList.
        /// </summary>
        /// <param name="baseStation">a base station</param>
        void Add(BO.BaseStation baseStation);

        /// <summary>
        /// The function returns all the baseStationList items 
        /// (by converting the IDal.DO BaseStationList to BO baseStationList)
        /// </summary>
        IEnumerable<BO.BaseStation> GetBOBaseStationsList();

        /// /// <summary>
        /// the function returns a baseStationForList list of all the baseStations which have availableChargeSlots.
        /// </summary>
        /// <returns> a baseStationForList list with available chargeSlots</returns>
        IEnumerable<BaseStationForList> GetAvailableChargeSlots();

        IEnumerable<BaseStationForList> GetBaseStationList();

        BaseStationForList GetBaseStationForList(int id);

        /// <summary>
        /// The function returns a base station according to id.
        /// </summary>
        /// <param name="id">base station's id</param>
        BO.BaseStation GetBLBaseStation(int id);
        BO.BaseStation BaseStationDOtOBO(IDal.DO.BaseStation baseStation);
        void UpdateBaseStation(int id, string name, string num);

    }

    /// <summary>
    /// an interface that forces the inherit classes to imlement the methods it contains.
    /// the methods are connected to actoions of drones.
    /// </summary>
    interface IDroneBL
    {
        /// <summary>
        /// The function adds a drone to the DronesList.
        /// </summary>
        /// <param name="drone">a drone</param>
        void Add(BO.Drone drone);
      
        IEnumerable<DroneInCharging> GetDronesInMe(int stationId);
        IEnumerable<BO.Drone> GetBODronesList();
        IEnumerable<DroneForList> InitDroneForList();
        Drone GetBLDroneFromBL(int id);
        /// <summary>
        /// The function displays a drone according to the id.
        /// </summary>
        /// <param name="id">drone's id</param>
        Drone GetBLDrone(int id);
        Drone DroneDOtOBO(IDal.DO.Drone drone);
        DroneForList GetDroneForList(int id);
        DroneForList GetDroneForList(BO.Drone drone);
        DroneForList GetDroneForList(IDal.DO.Drone drone);

        /// <summary>
        /// The function displays a drone in parcel according to id.
        /// </summary>
        /// <param name="id">drone's id</param>
        DroneInParcel GetBLDroneInParcel(int id);

        DroneInParcel DroneInParcelDOtOBO(int id);
        void UpdateDrone(int id, string model);
    }

    /// <summary>
    /// an interface that forces the inherit classes to imlement the methods it contains.
    /// the methods are connected to actoions of parcels.
    /// </summary>
    interface IParcelBL
    {
        /// <summary>
        /// The function adds a parcel to the parselsList.
        /// </summary>
        /// <param name="parcel">parcel</param>
        void Add(BO.Parcel parcel);

        /// <summary>
        /// The function returns all the ParcelsLIst items 
        /// (by converting the IDal.DO ParcelsLIst to BO ParcelsLIst)
        /// </summary>
        IEnumerable<BO.Parcel> GetBOParcelsList();

        /// <summary>
        /// the function returns a ParcelForList list
        /// by converting the BO.Parcel PArcelForList  type.
        /// </summary>
        /// <returns></returns>
        IEnumerable<ParcelForList> GetParcelsList();

        /// <summary>
        /// The function returns all the not associated parcels 
        /// </summary>
        IEnumerable<ParcelForList> GetNotAssociatedParcelsList();

        BO.Parcel GetBLParcel(int id);
        ParcelForList GetParcelForList(int id);
        ParcelInPassing GetParcelInPassing(int id);      
        ParcelInCustomer ParcelInCustomerDOtOBO(IDal.DO.Parcel parcel, FromOrTo fromOrTo);
        ParcelStatuses ParcelStatus(IDal.DO.Parcel parcel);
    }

    /// <summary>
    /// an interface that forces the inherit classes to imlement the methods it contains.
    /// the methods are connected to actoions of customers
    /// </summary>
    interface ICustomerBL
    {
        /// <summary>
        /// The function adds a customer to the customersList.
        /// </summary>
        /// <param name="customer">customer</param>
        void Add(BO.Customer customer);

        


        IEnumerable<BO.Customer> GetBOCustomersList();
        IEnumerable<ParcelInCustomer> GetParcelInCustomerList(FromOrTo fromOrTo, string id);
        IEnumerable<CustomerForList> GetCustomersList();
        CustomerForList GetCustomerForList(string id);
        BO.Customer GetBLCustomer(string id);
        BO.Customer CustomerDOtOBO(IDal.DO.Customer customer);
        CustomerInParcel GetBLCustomrInParcel(string id);
        CustomerInParcel CustomrInParcelDOtOBO(IDal.DO.Customer customer);
        

    }



}
