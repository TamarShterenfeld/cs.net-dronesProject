using System;
using System.Collections.Generic;
using System.Linq;
using BO;


namespace BLApi
{
    /// <summary>
    /// an interface that forces the inherit classes to imlement the methods it contains.
    /// the methods are connected to BL class.
    /// </summary>
    public interface IBL : IBaseStationBL, ICustomerBL, IDroneBL, IParcelBL
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
    public interface IBaseStationBL
    {
        /// <summary>
        /// The function adds a base station to the BaseStationsList.
        /// </summary>
        /// <param name="baseStation">a base station</param>
        void Add(BO.BaseStation baseStation);

        /// <summary>
        /// The function removes (by signing as "deleted" ) a base station.
        /// </summary>
        /// <param name="baseStation">a base station</param>
        void Delete(BaseStation baseStation);

        /// <summary>
        /// The function returns all the baseStationList items 
        /// (by converting the DO BaseStationList to BO baseStationList)
        /// </summary>
        IEnumerable<BaseStation> GetBOBaseStationsList();

        /// /// <summary>
        /// the function returns a baseStationForList list of all the baseStations which have availableChargeSlots.
        /// </summary>
        /// <returns> a baseStationForList list with available chargeSlots</returns>
        IEnumerable<BaseStationForList> GetAvailableChargeSlots();

        /// <summary>
        /// the function returns all the baseStations in format of of BaseStationForList.
        /// </summary>
        /// <returns>the BaseStationFor list</returns>
        IEnumerable<BaseStationForList> GetBaseStationList();

        /// <summary>
        /// the function returns a BaseStationForList object according to the id parameter.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        BaseStationForList GetBaseStationForList(int id);

        /// <summary>
        /// The function returns a base station according to id.
        /// </summary>
        /// <param name="id">base station's id</param>
        BaseStation GetBLBaseStation(int id);

        /// <summary>
        /// the function returns a BO.BaseStation object by converting the DO.BaseStation parameter object.
        /// </summary>
        /// <param name="baseStation">the object to convert</param>
        /// <returns>the BO.BaseStation converted object</returns>
        BaseStation ConvertBaseStationDOtOBO(DO.BaseStation baseStation);
        void UpdateBaseStation(int id, string name, string num);

    }

    /// <summary>
    /// an interface that forces the inherit classes to imlement the methods it contains.
    /// the methods are connected to actoions of drones.
    /// </summary>
    public interface IDroneBL
    {
        /// <summary>
        /// The function adds a drone to the DronesList.
        /// </summary>
        /// <param name="drone">a drone</param>
        /// /// <param name="baseStationId">the id of the base station</param>
        void Add(Drone drone, int baseStationId);

        // <summary>
        /// The function removes (by signing as "deleted" ) a parcel.
        /// </summary>
        /// <param name="baseStation">a base station</param>
        void Delete(Drone drone);

        /// <summary>
        /// the function returns a list of all the droneCharge object
        /// thet are charged in the baseStation with the parameter 'stationId'
        /// </summary>
        /// <param name="stationId">the id for identifying the requested baseStation</param>
        /// <returns></returns>
        IEnumerable<DroneInCharging> GetDronesInMe(int stationId);

        /// <summary>
        /// The function returns all the DronesLIst items 
        /// (by converting the DO DronesLIst to BO DronesLIst)
        /// </summary>
        IEnumerable<Drone> GetBODronesList();

        /// <summary>
        /// the function returns a list of all the drones in DroneForList format.
        /// </summary>
        /// <returns>a DroneForList list</returns>
        IEnumerable<DroneForList> GetDronesForList();


        /// <summary>
        /// the function returns a droneForList
        /// by converting the BO.Drone to DroneForList type.
        /// </summary>
        /// <returns></returns>
        IEnumerable<DroneForList> InitDroneForList();

        /// <summary>
        /// The function displays a drone according to the id.
        /// </summary>
        /// <param name="id">drone's id</param>
        Drone GetBLDrone(int id);

        /// <summary>
        /// the function converts a DO.Drone object to a BO.Drone object.
        /// </summary>
        /// <param name="drone"></param>
        /// <returns>a BO.Drone object</returns>
        Drone ConvertDroneDOtOBO(DO.Drone drone);

        /// <summary>
        /// returns the List dronesForList
        /// </summary>
        /// <returns>the List dronesForList</returns>
        DroneForList GetDroneForList(int id);

        /// <summary>
        /// the function gets a BO.Drone object and converts it to DronForList object.
        /// </summary>
        /// <param name="drone"></param>
        /// <returns>a DroneForList object</returns>
        DroneForList ConvertDroneBoToDroneForList(Drone drone);

        /// <summary>
        /// the function converts a DO.Drone object to DroneForList object
        /// </summary>
        /// <param name="drone">a drone to convert</param>
        /// <returns>the converted object</returns>
        DroneForList ConvertDroneDoToDroneForList(DO.Drone drone);

        /// <summary>
        /// the function returns a filtered dronesForList list by the current parameter of DroneStatus
        /// </summary>
        /// <param name="status">the requested status that the list is filtered by</param>
        /// <returns>a filtered list by the requested status</returns>
        IEnumerable<DroneForList> GetStatusFilteredDroneForList(DroneStatuses status);

        IEnumerable<DroneForList> GetWeightFilteredDroneForList(WeightCategories weight);

        /// <summary>
        /// The function displays a drone in parcel according to id.
        /// </summary>
        /// <param name="id">parcel's id - for identify the parcel</param>
        DroneInParcel GetBLDroneInParcel(int id);

        /// <summary>
        /// the function returns a DroneInParcel obj by the id parameter.
        /// </summary>
        /// <param name="id">drone's id - for identify the drone</param>
        /// <returns></returns>
        DroneInParcel GetDroneInParcel(int id);

        /// <summary>
        /// the function update at least one of the parameters of the drone (those which don't hold a null value)
        /// which its id is like the parameter id.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        void UpdateDrone(int id, string model);

        /// <summary>
        /// the function update a new DroneForList object instead of the prev one - with the same id.
        /// </summary>
        /// <param name="droneForList">the droneForList object to update</param>
        void UpdateDrone(DroneForList droneForList);
    }

    /// <summary>
    /// an interface that forces the inherit classes to imlement the methods it contains.
    /// the methods are connected to actoions of parcels.
    /// </summary>
    public interface IParcelBL
    {
        /// <summary>
        /// The function adds a parcel to the parselsList.
        /// </summary>
        /// <param name="parcel">parcel</param>
        void Add(Parcel parcel);

        // <summary>
        /// The function removes (by signing as "deleted" ) a parcel.
        /// </summary>
        /// <param name="baseStation">a base station</param>
        void Delete(Parcel parcel);

        /// <summary>
        /// The function returns all the ParcelsLIst items 
        /// (by converting the DO ParcelsLIst to BO ParcelsLIst)
        /// </summary>
        IEnumerable<Parcel> GetBOParcelsList();

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
        /// The function returns a parcel according to id.
        /// </summary>
        /// <param name="id">parcel's id</param>
        Parcel GetBLParcel(int id);

        /// <summary>
        /// the function returns a ParcelForList object.
        /// </summary>
        /// <param name="id">for identify the parcelForList object</param>
        /// <returns>the requested ParcelForList object</returns>
        ParcelForList GetParcelForList(int id);

        /// <summary>
        /// the function returns a ParcelInPassing object by its id.
        /// </summary>
        /// <param name="id">for identify the ParcelInPassing object</param>
        /// <returns>the requested ParcelInPassing object</returns>
        ParcelInPassing GetParcelInPassing(int id);

        /// <summary>
        /// the function gets a DO.Parcel object ParcelStatus(by the values of the Dates)
        /// </summary>
        /// <param name="parcel">the Parcel object for checking</param>
        /// <returns>the appropriate ParcelStatus</returns>
        ParcelStatuses ParcelStatus(DO.Parcel parcel);

        /// <summary>
        /// the function updates the parcel object instead of the prev object in the same id.
        /// </summary>
        /// <param name="parcel">the object to convert</param>
        void UpdateParcel(Parcel parcel);
    }

    /// <summary>
    /// an interface that forces the inherit classes to imlement the methods it contains.
    /// the methods are connected to actoions of customers
    /// </summary>
    public interface ICustomerBL
    {
        /// <summary>
        /// The function adds a customer to the customersList.
        /// </summary>
        /// <param name="customer">customer</param>
        void Add(Customer customer);

        /// <summary>
        /// The function returns all the CustomersLIst items 
        /// (by converting the DO CustomersLIst to BO CustomersLIst)
        /// </summary>
        IEnumerable<Customer> GetBOCustomersList();


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
        Customer GetBLCustomer(string id);

        /// <summary>
        /// the function converts a DO.Customer to a BO.Customer.
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        Customer ConvertCustomerDoToBo(DO.Customer customer);

        /// <summary>
        /// the function returns a CustomerForList object - according to the requested id.
        /// </summary>
        /// <param name="id">the requested customer's id</param>
        /// <returns>a BO.Customer object</returns>
        CustomerForList GetCustomerForList(string id);

        /// <summary>
        /// The function displays a customer in parcel according to id.
        /// </summary>
        /// <param name="id">parcel's id</param>
        CustomerInParcel GetCustomrInParcel(string id);

        /// <summary>
        /// the function converts a DO,Customer obj to a customerInParcel obsj
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        CustomerInParcel ConvertCustomerDoToCustomerInParcel(DO.Customer customer);

        /// <summary>
        /// update a Customer object with at least one of the parameters: name / phone (the one that isn't null)
        /// </summary>
        /// <param name="customerId">the requseted customer id (for updating that object)</param>
        /// <param name="name">the name to update (if it's not null)</param>
        /// <param name="phone">the phone to update (if it's not null)</param>
        void UpdateCustomer(string customerId, string name, string phone);

        /// <summary>
        /// The function removes (by signing as "deleted" ) a  customer.
        /// </summary>
        /// <param name="customer">Customer object</param>
        void Delete(Customer customer);

    }
}
