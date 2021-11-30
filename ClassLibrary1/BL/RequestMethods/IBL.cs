using System;
using System.Collections.Generic;
using System.Linq;
using IBL.BO;


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

        /// <summary>
        /// The function gives pick up date to the parcel.
        /// </summary>
        /// <param name="parcelId">parcel id</param>
        /// <param name="senderId">sender id</param>
        void PickUpParcel(int droneId);

        /// <summary>
        /// The function gives arrival date to the parcel.
        /// </summary>
        /// <param name="parcelId">parcel id</param>
        /// <param name="targetId">target id</param>
        void SupplyParcel(int droneId);

        /// <summary>
        /// the functuin trys to charge the drone.
        /// </summary>
        /// <param name="droneId">drone's id</param>
        /// <param name="baseStationId">base station's id</param>
        void SendDroneForCharge(int droneId);

        /// <summary>
        /// the function stops the drone from charging
        /// </summary>
        /// <param name="droneId">drone's id</param>

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
        /// /// <param name="baseStationId">the id of the base station</param>
        void Add(BO.Drone drone, int baseStationId);

        IEnumerable<DroneInCharging> GetDronesInMe(int stationId);

        /// <summary>
        /// The function returns all the DronesLIst items 
        /// (by converting the IDal.DO DronesLIst to BO DronesLIst)
        /// </summary>
        IEnumerable<BO.Drone> GetBODronesList();

        IEnumerable<DroneForList> GetDronesForLists();


        /// <summary>
        /// the function returns a droneForList
        /// by converting the BO.Drone to DroneForList type.
        /// </summary>
        /// <returns></returns>
        IEnumerable<DroneForList> InitDroneForList();
        Drone GetBLDroneFromBL(int id);
        /// <summary>
        /// The function displays a drone according to the id.
        /// </summary>
        /// <param name="id">drone's id</param>
        Drone GetBLDrone(int id);
        Drone DroneDOtOBO(IDal.DO.Drone drone);

        /// <summary>
        /// returns the List dronesForList
        /// </summary>
        /// <returns>the List dronesForList</returns>
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
        void UpdateDrone(DroneForList droneForList);
        void UpdateDroneForList(DroneForList droneForList);
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
        /// <summary>
        /// The function returns List of ParcelInCustomer items for sender or target
        /// </summary>
        /// <param name="fromOrTo"> sender or target </param>
        /// <returns> List of ParcelInCustomer items </returns>
        IEnumerable<ParcelInCustomer> GetParcelInCustomerList(FromOrTo fromOrTo, string id);

        /// <summary>
        /// The function displays a parcel according to id.
        /// </summary>
        /// <param name="id">parcel's id</param>
        BO.Parcel GetBLParcel(int id);
        ParcelForList GetParcelForList(int id);

        
        ParcelInPassing GetParcelInPassing(int id);

        /// <summary>
        /// The function displays a parcel in customer according to id.
        /// </summary>
        /// <param name="id">parcel's id</param>

        ParcelInCustomer ParcelInCustomerDOtOBO(IDal.DO.Parcel parcel, FromOrTo fromOrTo);
        ParcelStatuses ParcelStatus(IDal.DO.Parcel parcel);
        void UpdateParcel(Parcel parcel);
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

        /// <summary>
        /// The function returns all the CustomersLIst items 
        /// (by converting the IDal.DO CustomersLIst to BO CustomersLIst)
        /// </summary>
        IEnumerable<BO.Customer> GetBOCustomersList();
       

        /// <summary>
        /// the function returns a customerForLIst list
        /// by converting the BO.customero customerForLIst type.
        /// </summary>
        /// <returns></returns>
        IEnumerable<CustomerForList> GetCustomersList();

        /// <summary>
        /// The function displays a customer according to id.
        /// </summary>
        /// <param name="id">customer's id</param>
        BO.Customer GetBLCustomer(string id);
        BO.Customer CustomerDOtOBO(IDal.DO.Customer customer);
        CustomerForList GetCustomerForList(string id);

        /// <summary>
        /// The function displays a customer in parcel according to id.
        /// </summary>
        /// <param name="id">parcel's id</param>
        CustomerInParcel GetBLCustomrInParcel(string id);

        /// <summary>
        /// the function converts a DO,Customer obj to a customerInParcel obsj
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        CustomerInParcel CustomrInParcelDOtOBO(IDal.DO.Customer customer);

        void UpdateCustomer(string customerId, string name, string phone);

    }
}
