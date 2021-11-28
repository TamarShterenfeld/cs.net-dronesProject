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
                Location = new Location(CoordinateDoToBo(baseStation.Longitude), CoordinateDoToBo(baseStation.Latitude)),
                ChargeSlots = baseStation.ChargeSlots - dal.CaughtChargeSlots(baseStation.Id),
                DroneCharging = (List<DroneInCharging>)GetDronesInMe(baseStation.Id)
            };
            return BOBaseStation;
        }

        public BaseStationForList GetBaseStationForList(int id)
        {
            BO.BaseStation item = GetBLBaseStation(id);
            BaseStationForList current = new()
            {
                Id = item.Id,
                CaughtChargeSlots = dal.CaughtChargeSlots(item.Id),
                Name = item.Name,
                AvailableChargeSlots = item.ChargeSlots - dal.CaughtChargeSlots(item.Id),
            };
            return current;
        }



        //----------------------------------Drone GetObject Methods---------------------------------


        public BO.Drone GetBLDroneFromBL(int id)
        {
            DroneForList droneForList = dronesForList.First(drone => drone.Id == id);
            BO.Drone drone = new()
            {
                Id = droneForList.Id,
                Status = droneForList.Status,
                Battery = droneForList.Battery,
                Location = droneForList.Location,
                Model = droneForList.Model,
                MaxWeight = droneForList.MaxWeight,
                Parcel = droneForList.ParcelId == 0 ? null : GetParcelInPassing(droneForList.ParcelId)
            };
            return drone;
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
            BO.Drone BODrone = new()
            {
                Id = drone.Id,
                Model = drone.Model,
                MaxWeight = (BO.WeightCategories)drone.MaxWeight,
                Battery = GetDroneBattery(drone.Id),
                Status = GetDroneStatus(drone.Id),
                Parcel = GetDroneParcelId(drone.Id) != 0 ? GetParcelInPassing(GetDroneParcelId(drone.Id)) : null,
                Location = GetDroneLocation(drone.Id)
            };
            return BODrone;
        }

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

        public DroneForList GetDroneForList(IDal.DO.Drone drone)
        {
            DroneForList current = new()
            {
                Id = drone.Id,
                MaxWeight = (BO.WeightCategories)drone.MaxWeight,
                Model = drone.Model,
           
            };
            return current;
        }

        public DroneForList GetDroneForList(BO.Drone drone)
        {
            DroneForList current = new()
            {
                Id = drone.Id,
                MaxWeight = drone.MaxWeight,
                Model = drone.Model,
                Battery = drone.Battery,
                Status = drone.Status,
                Location = drone.Location,
                ParcelId = drone.Parcel != null ? drone.Parcel.Id : 0,
            };
            return current;
        }


        /// <summary>
        /// The function displays a drone in parcel according to id.
        /// </summary>
        /// <param name="id">drone's id</param>
        public DroneInParcel GetBLDroneInParcel(int id)
        {
            if (id == 0)
            {
                return null;
            }
            return DroneInParcelDOtOBO(id);
        }

        public DroneInParcel DroneInParcelDOtOBO(int id)
        {
            DroneForList drone = dronesForList.First(drone => drone.Id == id);
            DroneInParcel droneInPrcel = new()
            {
                Id = drone.Id,
                Battery = GetDroneBattery(drone.Id),
                CurrentLocation = drone.Location,
            };
            return droneInPrcel;
        }

        //----------------------------------DroneForList GetObject Methods---------------------------------

        private DroneForList GetOneDroneForList(BO.Drone drone)
        {
            DroneForList droneForList = new()
            {
                Id = drone.Id,
                Model = drone.Model,
                MaxWeight = drone.MaxWeight,
                Status = drone.Status,
                ParcelId = drone.Parcel != null ? drone.Parcel.Id : 0,
                Location = drone.Location,
                Battery = drone.Battery
            };
            return droneForList;
        }

        //----------------------------------Parcel GetObject Methods---------------------------------

        public ParcelForList GetParcelForList(int id)
        {
            BO.Parcel item = GetBLParcel(id);
            ParcelForList current = new()
            {
                DroneId = item.MyDrone!= null? item.MyDrone.Id : 0,
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
                Distatnce = Locatable.Distance((ILocatable)(parcel.Sender), (ILocatable)(parcel.Target))
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
                Phone = item.Phone,
                AmountOfGetParcels = GetAndSupplied(item),
                AmountOfInPassingParcels = GetAndNotSupplied(item),
                AmountOfSendAndNotSuppliedParcels = SendAndNotSupplied(item),
                AmountOfSendAndSuppliedParcels = SendAndSupplied(item)
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
                Location = new Location(CoordinateDoToBo(customer.Longitude), CoordinateDoToBo(customer.Latitude)),
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




