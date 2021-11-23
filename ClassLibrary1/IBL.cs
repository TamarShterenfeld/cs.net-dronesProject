using System;
using System.Collections.Generic;
using System.Linq;
using IBL.BO;


namespace IBL
{
    interface IBL : IBaseStationBL, ICustomerBL, IDroneBL, IParcelBL
    {
        void AssociateParcel(int droneId);
        void PickUpParcel(int droneId);
        void SupplyParcel(int droneId);
        void ChargeDrone(int droneId);
        void ReleaseDroneFromRecharge(int droneId);

    }
    interface IBaseStationBL
    {
        void Add(BO.BaseStation baseStation);
        IEnumerable<BO.BaseStation> GetBOBaseStationsList();
        IEnumerable<BO.BaseStation> GetAvailableChargeSlots();
        IEnumerable<BaseStationForList> GetBaseStationList();
        BaseStationForList GetBaseStationForList(int id);
        BO.BaseStation GetBLBaseStation(int id);
        BO.BaseStation BaseStationDOtOBO(IDal.DO.BaseStation baseStation);
        public void UpdateBaseStation(int id, string name, string num);

    }

    interface IDroneBL
    {
        void Add(BO.Drone baseStation, int baseStationId);
        IEnumerable<DroneInCharging> GetDronesInMe(int stationId);
        IEnumerable<BO.Drone> GetBODronesList();
        IEnumerable<DroneForList> GetDronesList();
        DroneForList GetDroneForList(int id);
        BO.Drone GetBLDrone(int id);
        BO.Drone DroneDOtOBO(IDal.DO.Drone drone);
        DroneInParcel GetBLDroneInParcel(int id);
        DroneInParcel DroneInParcelDOtOBO(IDal.DO.Drone drone);
        public void UpdateDrone(int id, string model);
    }

    interface IParcelBL
    {
        IEnumerable<BO.Parcel> GetBOParcelsList();
        IEnumerable<ParcelForList> GetParcelsList();
        IEnumerable<BO.Parcel> GetNotAssociatedParcelsList();
        void Add(BO.Parcel baseStation);
        ParcelForList GetParcelForList(int id);
        ParcelInPassing GetParcelInPassing(int id);
        BO.Parcel GetBLParcel(int id);
        ParcelInCustomer ParcelInCustomerDOtOBO(IDal.DO.Parcel parcel, FromOrTo fromOrTo);
        ParcelStatuses ParcelStatus(IDal.DO.Parcel parcel);

    }

    interface ICustomerBL
    {
        void Add(BO.Customer baseStation);
        IEnumerable<BO.Customer> GetBOCustomersList();
        IEnumerable<ParcelInCustomer> GetParcelInCustomerList(FromOrTo fromOrTo, string id);
        IEnumerable<CustomerForList> GetCustomersList();
        CustomerForList GetCustomerForList(string id);
        BO.Customer GetBLCustomer(string id);
        BO.Customer CustomerDOtOBO(IDal.DO.Customer customer);
        CustomerInParcel GetBLCustomrInParcel(string id);
        CustomerInParcel CustomrInParcelDOtOBO(IDal.DO.Customer customer);
        public void UpdateBaseStation(string id, string name, string num);


    }



}
