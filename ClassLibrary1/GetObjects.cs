using System;
using System.Collections.Generic;
using System.Text;
using static DalObject.DataSource;
using IBL.BO;
using System.Linq;
using System.Numerics;
using IDal.DO;
using IDal;
using static DalObject.DalObject;



namespace IBL
{
    public partial class BL : IBL
    {
        
        /// <summary>
        /// The function displays a base station according to id.
        /// </summary>
        /// <param name="id">base station's id</param>
        public BO.BaseStation GetBLBaseStation(int id)
        {
            return BaseStationDOtOBO(dal.GetBaseStation(id)); ;
        }

        private BO.BaseStation BaseStationDOtOBO(IDal.DO.BaseStation baseStation)
        {
            BO.BaseStation BOBaseStation = new BO.BaseStation()
            {
                Id = baseStation.Id,
                Name = baseStation.Name,
                MyLocation = new Location(baseStation.Longitude, baseStation.Latitude),
                ChargeSlots = baseStation.ChargeSlots - AvailableChargingSlots(baseStation.Id),
                DroneCharging = (List<DroneInCharging>)GetDronesInMe(baseStation.Id)
            };
            return BOBaseStation;
        }


        /// <summary>
        /// The function displays a drone according to the id.
        /// </summary>
        /// <param name="id">drone's id</param>
        public BO.Drone GetBLDrone(int id)
        {
            return DroneDOtOBO(dal.GetDrone(id));
            
        }

        private BO.Drone DroneDOtOBO(IDal.DO.Drone drone)
        {
            BO.Drone BODrone = new BO.Drone()
            {
                Id = drone.Id,
                Model = drone.Model,
                MaxWeight = (BO.WeightCategories)drone.MaxWeight,
                //Battery = 0,
                //Status = 0,
                //Parcel = 
                Location = new Location(drone.Longitude, drone.Latitude),
            };
            return BODrone;
        }

        /// <summary>
        /// The function displays a customer according to id.
        /// </summary>
        /// <param name="id">customer's id</param>
        public BO.Customer GetBLCustomer(string id)
        {
            return CustomerDOtOBO(dal.GetCustomer(id));

        }

        private BO.Customer CustomerDOtOBO(IDal.DO.Customer customer)
        {
            BO.Customer BOCustomer = new BO.Customer()
            {
                Id = customer.Id,
                Name = customer.Name,
                Phone = customer.Phone,
                Location = new Location(customer.Longitude, customer.Latitude)
                //FromCustomer = null;
                //ToCustomer = null;
            };
            return BOCustomer;
        }

        /// <summary>
        /// The function displays a parcel according to id.
        /// </summary>
        /// <param name="id">parcel's id</param>
        public BO.Parcel GetBLParcel(int id)
        {
            return ParcelDOtOBO(dal.GetParcel(id));

        }

        private BO.Parcel ParcelDOtOBO(IDal.DO.Parcel parcel)
        {
            BO.Parcel BOParcel = new BO.Parcel()
            {
                Id = parcel.Id,
                Sender = GetBLCustomrInParcel(parcel.SenderId),
                Target = GetBLCustomrInParcel(parcel.TargetId),
                Weight = (BO.WeightCategories)parcel.Weight,
                Priority = (BO.Priorities)parcel.Priority,
                MyDrone = GetBLDroneInParcel(parcel.DroneId),
                ProductionDate = parcel.ProductionDate,
                AssociationDate = parcel.AssociationDate,
                PickUpDate = parcel.PickUpDate,
                SupplyDate = parcel.SupplyDate
            };
            return BOParcel;
        }

        /// <summary>
        /// The function displays a customer in parcel according to id.
        /// </summary>
        /// <param name="id">parcel's id</param>
        public CustomerInParcel GetBLCustomrInParcel(string id)
        {
            return CustomrInParcelDOtOBO(dal.GetCustomer(id));

        }

        private CustomerInParcel CustomrInParcelDOtOBO(IDal.DO.Customer customer)
        {
            CustomerInParcel BOCustomrInParcel = new CustomerInParcel()
            {
                Id = customer.Id,
                Name = customer.Name
            };
            return BOCustomrInParcel;
        }

        

        public DroneInParcel GetBLDroneInParcel(int id)
        {
            return DroneInParcelDOtOBO(dal.GetDrone(id));

        }

        private DroneInParcel DroneInParcelDOtOBO(IDal.DO.Drone drone)
        {
            DroneInParcel BOCustomerInParcel = new DroneInParcel()
            {
                Id = drone.Id,
                Battery = drone.Battery,
                CurrentLocation = new Location(drone.Longitude, drone.Latitude),
            };
            return BOCustomerInParcel;
        }

        public int catchAvailableChargeSlots(int stationId)
        {
            int caught = dal.AvailableChargeSlots(stationId);
            return caught;
        }

    }
}

