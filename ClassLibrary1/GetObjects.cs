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
using static IBL.BO.Locatable;



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
                Battery = getDroneBattery(drone.Id),
                Status = (BO.DroneStatuses)drone.Status,
                Parcel = (getDroneParcelId(drone.Id) !=  0)? GetParcelInPassing(getDroneParcelId(drone.Id)) : null,
                Location = getDroneLocation(drone.Id)
            };
            return BODrone;
        }

        private double getDroneBattery(int droneId)
        {
            return dronesForList.Find(drone => drone.Id == droneId).Battery;
        }

        private Location getDroneLocation(int droneId)
        {
            return dronesForList.Find(drone => drone.Id == droneId).Location;
        }

        private int getDroneParcelId(int droneId)
        {
            return dronesForList.Find(drone => drone.Id == droneId).ParcelId;
        }

        private ParcelInPassing GetParcelInPassing(int id)
        {
            BO.Parcel parcel = GetBLParcel(id);
            ParcelInPassing parcelInPassing = new ParcelInPassing()
            {
                Id = parcel.Id,
                Weight = parcel.Weight,
                Priority = parcel.Priority,
                ToDestination = parcel.PickUpDate == new DateTime() ? false : true,
                Sender = parcel.Sender,
                Target = parcel.Target,
                Collect = GetBLCustomer(parcel.Sender.Id).Location,
                Destination = GetBLCustomer(parcel.Target.Id).Location,
                Distatnce = Locatable.Distance((ILocatable)parcel.Sender, (ILocatable)parcel.Target)
            };
            return parcelInPassing;
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
                Location = new Location(customer.Longitude, customer.Latitude),
                FromCustomer = (List<ParcelInCustomer>)GetParcelInCustomerList(FromOrTo.From, customer.Id),
                ToCustomer = (List<ParcelInCustomer>)GetParcelInCustomerList(FromOrTo.To, customer.Id),
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


        /// <summary>
        /// The function displays a drone in parcel according to id.
        /// </summary>
        /// <param name="id">drone's id</param>
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
                //CurrentLocation =,
            };
            return BOCustomerInParcel;
        }

        /// <summary>
        /// The function displays a parcel in customer according to id.
        /// </summary>
        /// <param name="id">parcel's id</param>

        //public ParcelInCustomer GetBLParcelInCustomer(int id, FromOrTo fromOrTo)
        //{
        //    return ParcelInCustomerDOtOBO(dal.GetParcel(id),fromOrTo);

        //}

        private ParcelInCustomer ParcelInCustomerDOtOBO(IDal.DO.Parcel parcel, FromOrTo fromOrTo)
        {
            ParcelInCustomer BOCustomerInParcel = new ParcelInCustomer()
            {
                Id = parcel.Id,
                Weight = (BO.WeightCategories)parcel.Weight,
                Priority = (BO.Priorities)parcel.Priority,
                ParcelStatus = ParcelStatus(parcel),
                SourceOrDest = fromOrTo == FromOrTo.From ? GetBLCustomrInParcel(parcel.SenderId) : GetBLCustomrInParcel(parcel.TargetId)
            };
            return BOCustomerInParcel;
        }

        private ParcelStatuses ParcelStatus(IDal.DO.Parcel parcel)
        {
            DateTime time = new DateTime();
            return parcel.AssociationDate == time ? ParcelStatuses.Production :
                    parcel.PickUpDate == time ? ParcelStatuses.Associated :
                    parcel.SupplyDate == time ? ParcelStatuses.PickedUp : ParcelStatuses.Supplied;
        }

        public int catchAvailableChargeSlots(int stationId)
        {
            int caught = dal.AvailableChargeSlots(stationId);
            return caught;
        }

    }
}

