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

        //----------------------------------BaseStation GetObject Methods---------------------------------
        public BaseStationForList GetBaseStationForList(int id)
        {
            BO.BaseStation item = GetBLBaseStation(id);
            BaseStationForList current = new()
            {
                Id = item.Id,
                CaughtChargeSlots = item.ChargeSlots - CatchAvailableChargeSlots(item.Id),
                Name = item.Name,
                FreeChargeSlots = CatchAvailableChargeSlots(item.Id)
            };
            return current;
        }

        /// <summary>
        /// The function displays a base station according to id.
        /// </summary>
        /// <param name="id">base station's id</param>
        public BO.BaseStation GetBLBaseStation(int id)
        {
            return BaseStationDOtOBO(dal.GetBaseStation(id)); ;
        }

        public BO.BaseStation BaseStationDOtOBO(IDal.DO.BaseStation baseStation)
        {
            BO.BaseStation BOBaseStation = new()
            {
                Id = baseStation.Id,
                Name = baseStation.Name,
                Location = new Location(baseStation.Longitude, baseStation.Latitude),
                ChargeSlots = baseStation.ChargeSlots - CatchAvailableChargeSlots(baseStation.Id),
                DroneCharging = (List<DroneInCharging>)GetDronesInMe(baseStation.Id)
            };
            return BOBaseStation;
        }


        

        //----------------------------------Drone GetObject Methods---------------------------------

        public DroneForList GetDroneForList(int id)
        {
            BO.Drone item = GetBLDrone(id);
            DroneForList current = new()
            {
                Id = item.Id,
                Battery = item.Battery,
                Location = item.Location,
                MaxWeight = item.MaxWeight,
                Model = item.Model,
                ParcelId = item.Parcel.Id,
                Status = item.Status
            };
            return current;
        }

        /// <summary>
        /// The function displays a drone according to the id.
        /// </summary>
        /// <param name="id">drone's id</param>
        public BO.Drone GetBLDrone(int id)
        {
            return DroneDOtOBO(dal.GetDrone(id));

        }

        public BO.Drone DroneDOtOBO(IDal.DO.Drone drone)
        {
            BO.Drone BODrone = new ()
            {
                Id = drone.Id,
                Model = drone.Model,
                MaxWeight = (BO.WeightCategories)drone.MaxWeight,
                Battery = GetDroneBattery(drone.Id),
                Status = (BO.DroneStatuses)drone.Status,
                Parcel = (GetDroneParcelId(drone.Id) != 0) ? GetParcelInPassing(GetDroneParcelId(drone.Id)) : null,
                Location = GetDroneLocation(drone.Id)
            };
            return BODrone;
        }

        /// <summary>
        /// The function displays a drone in parcel according to id.
        /// </summary>
        /// <param name="id">drone's id</param>
        public DroneInParcel GetBLDroneInParcel(int id)
        {
            return DroneInParcelDOtOBO(dal.GetDrone(id));

        }

        public DroneInParcel DroneInParcelDOtOBO(IDal.DO.Drone drone)
        {
            DroneInParcel BOCustomerInParcel = new()
            {
                Id = drone.Id,
                Battery = drone.Battery,
                //CurrentLocation =,
            };
            return BOCustomerInParcel;
        }


        //----------------------------------Parcel GetObject Methods---------------------------------

        public ParcelForList GetParcelForList(int id)
        {
            BO.Parcel item = GetBLParcel(id);
            ParcelForList current = new()
            {
                DroneId = item.MyDrone.Id,
                ParcelId = item.Id,
                Priority = item.Priority,
                SenderId = item.Sender.Id,
                Status = ParcelStatuses.Production,
                TargetId = item.Target.Id,
                Weight = item.Weight
            };
            return current;
        }

        public ParcelInPassing GetParcelInPassing(int id)
        {
            BO.Parcel parcel = GetBLParcel(id);
            ParcelInPassing parcelInPassing = new()
            {
                Id = parcel.Id,
                Weight = parcel.Weight,
                Priority = parcel.Priority,
                ToDestination = parcel.PickUpDate == new DateTime(),
                Sender = parcel.Sender,
                Target = parcel.Target,
                Collect = GetBLCustomer(parcel.Sender.Id).Location,
                Destination = GetBLCustomer(parcel.Target.Id).Location,
                Distatnce = Locatable.Distance((ILocatable)parcel.Sender, (ILocatable)parcel.Target)
            };
            return parcelInPassing;
        }

        /// <summary>
        /// The function displays a parcel according to id.
        /// </summary>
        /// <param name="id">parcel's id</param>
        public BO.Parcel GetBLParcel(int id)
        {
            return ParcelDOtOBO(dal.GetParcel(id));

        }

        /// <summary>
        /// The function displays a parcel in customer according to id.
        /// </summary>
        /// <param name="id">parcel's id</param>

        //public ParcelInCustomer GetBLParcelInCustomer(int id, FromOrTo fromOrTo)
        //{
        //    return ParcelInCustomerDOtOBO(dal.GetParcel(id),fromOrTo);

        //}

        public ParcelInCustomer ParcelInCustomerDOtOBO(IDal.DO.Parcel parcel, FromOrTo fromOrTo)
        {
            ParcelInCustomer BOCustomerInParcel = new()
            {
                Id = parcel.Id,
                Weight = (BO.WeightCategories)parcel.Weight,
                Priority = (BO.Priorities)parcel.Priority,
                ParcelStatus = ParcelStatus(parcel),
                SourceOrDest = fromOrTo == FromOrTo.From ? GetBLCustomrInParcel(parcel.SenderId) : GetBLCustomrInParcel(parcel.TargetId)
            };
            return BOCustomerInParcel;
        }

        public ParcelStatuses ParcelStatus(IDal.DO.Parcel parcel)
        {
            DateTime time = new();
            return parcel.AssociationDate == time ? ParcelStatuses.Production :
                    parcel.PickUpDate == time ? ParcelStatuses.Associated :
                    parcel.SupplyDate == time ? ParcelStatuses.PickedUp : ParcelStatuses.Supplied;
        }


        //----------------------------------Customer GetObject Methods---------------------------------

        public CustomerForList GetCustomerForList(string id)
        {
            BO.Customer item = GetBLCustomer(id);
            CustomerForList current = new()
            {
                Id = item.Id,
                Name = item.Name,
                Phone = item.Phone
            };
            return current;

        }


        /// <summary>
        /// The function displays a customer according to id.
        /// </summary>
        /// <param name="id">customer's id</param>
        public BO.Customer GetBLCustomer(string id)
        {
            return CustomerDOtOBO(dal.GetCustomer(id));

        }

        public BO.Customer CustomerDOtOBO(IDal.DO.Customer customer)
        {
            BO.Customer BOCustomer = new()
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
        /// The function displays a customer in parcel according to id.
        /// </summary>
        /// <param name="id">parcel's id</param>
        public CustomerInParcel GetBLCustomrInParcel(string id)
        {

            return CustomrInParcelDOtOBO(dal.GetCustomer(id));
        }

        /// <summary>
        /// the function converts a DO,Customer obj to a customerInParcel obsj
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        public CustomerInParcel CustomrInParcelDOtOBO(IDal.DO.Customer customer)
        {
            CustomerInParcel BOCustomrInParcel = new()
            {
                Id = customer.Id,
                Name = customer.Name
            };
            return BOCustomrInParcel;
        }


    }
}




