using System;
using System.Collections.Generic;
using System.Linq;
using IBL.BO;
using static IBL.BL;
using DAL.DO;
using DalObject;


namespace IBL
{
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
    interface IBaseStationBL
    {
        /// <summary>
        /// The function adds a base station 
        /// </summary>
        /// <param name="baseStation">base station</param>
        /// 
        void Add(BO.BaseStation baseStation);

        /// <summary>
        /// The function returns all the baseStationList items 
        /// (by converting the IDal.DO BaseStationList to BO baseStationList)
        /// </summary>
        IEnumerable<BO.BaseStation> GetBOBaseStationsList();

        /// /// <summary>
        /// the function returns a baseStationForList list
        /// by converting the BO.baseStation list to BaseStationForList type.
        /// </summary>
        /// <returns></returns>
        IEnumerable<BaseStationForList> GetAvailableChargeSlots();

        IEnumerable<BaseStationForList> GetBaseStationList();
        BaseStationForList GetBaseStationForList(int id);
        BO.BaseStation GetBLBaseStation(int id);
        BO.BaseStation BaseStationDOtOBO(IDal.DO.BaseStation baseStation);
        public void UpdateBaseStation(int id, string name, string num);

    }

    interface IDroneBL
    {
        /// <summary>
 /// <summary>
        /// The function adds a base station 
        /// </summary>
        /// <param name="baseStation">base station</param>
        /// 
        void Add(BO.BaseStation baseStation);

       
        IEnumerable<DroneInCharging> GetDronesInMe(int stationId);
        IEnumerable<BO.Drone> GetBODronesList();
        IEnumerable<DroneForList> InitDroneForList();
        DroneForList GetDroneForList(IDal.DO.Drone drone);
        BO.Drone GetBLDrone(int id);
        BO.Drone DroneDOtOBO(IDal.DO.Drone drone);
        DroneInParcel GetBLDroneInParcel(int id);
        DroneInParcel DroneInParcelDOtOBO(int id);
        public void UpdateDrone(int id, string model);
    }

    interface IParcelBL
    {
        IEnumerable<BO.Parcel> GetBOParcelsList();
        IEnumerable<ParcelForList> GetParcelsList();
        IEnumerable<ParcelForList> GetNotAssociatedParcelsList();
        void Add(BO.Parcel baseStation);
        ParcelForList GetParcelForList(int id);
        ParcelInPassing GetParcelInPassing(int id);
        BO.Parcel GetBLParcel(int id);
        ParcelInCustomer ParcelInCustomerDOtOBO(IDal.DO.Parcel parcel, FromOrTo fromOrTo);
        ParcelStatuses ParcelStatus(IDal.DO.Parcel parcel);

    }

    interface ICustomerBL
    {
        /// <summary>
        /// The function adds a customer 
        /// </summary>
        /// <param name="customer">customer</param>
        void Add(BO.Customer baseStation);

        /// <summary>
        /// The function adds a parcel 
        /// </summary>
        /// <param name="parcel">parcel</param>
        void Add(BO.Parcel parcel);


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
