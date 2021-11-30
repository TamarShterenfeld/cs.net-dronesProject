using System;
using System.Collections.Generic;
using System.Text;
using static DalObject.DataSource;
using IBL.BO;
using System.Linq;
using System.Numerics;
using static IBL.BO.Locatable;



namespace IBL
{
    public partial class BL : IBL
    {

        //----------------------------------BaseStation GetObject Methods---------------------------------
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
        public Drone GetBLDroneFromBL(int id)
        {
            DroneForList droneForList = dronesForList.First(drone => drone.Id == id);
            ParcelInPassing parcel = new() { Id = droneForList.ParcelId };
            BO.Drone drone = new BO.Drone(droneForList.Id, droneForList.Model, droneForList.MaxWeight, droneForList.Battery, droneForList.Status, parcel, droneForList.Location );
            return drone;
        }

        public Drone GetBLDrone(int id)
        {
            return DroneDOtOBO(dal.GetDrone(id));
        }

        public Drone DroneDOtOBO(IDal.DO.Drone drone)
        {
            Drone bODrone = new Drone(drone.Id, drone.Model,(WeightCategories)( drone.MaxWeight), 0, DroneStatuses.Available, null, null);           
            return bODrone;
        }

        //----------------------------------DroneForList GetObject Methods---------------------------------

        public DroneForList GetDroneForList(int id)
        {
            BO.Drone item = GetBLDrone(id);
            ParcelInPassing parcel = new();
            if (item.Parcel != null) 
                return new DroneForList(item.Id, item.Parcel.Id, item.Model, item.MaxWeight, item.Battery, item.Status, item.Location);
            else
                return new DroneForList(item.Id, 0 , item.Model, item.MaxWeight, item.Battery, item.Status, item.Location);
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

        //----------------------------------DroneInParcel GetObject Methods---------------------------------
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

        //----------------------------------Parcel GetObject Methods---------------------------------

        public ParcelStatuses ParcelStatus(IDal.DO.Parcel parcel)
        {
            DateTime time = new();
            return parcel.AssociationDate == time ? ParcelStatuses.Production :
                    parcel.PickUpDate == time ? ParcelStatuses.Associated :
                    parcel.SupplyDate == time ? ParcelStatuses.PickedUp : ParcelStatuses.Supplied;
        }
      
        public Parcel GetBLParcel(int id)
        {
            return ParcelDOtOBO(dal.GetParcel(id));

        }

        //----------------------------------ParcelForList GetObject Methods---------------------------------

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

        //----------------------------------ParcelInPassing GetObject Methods---------------------------------
        public ParcelInPassing GetParcelInPassing(int id)
        {
            BO.Parcel parcel = GetBLParcel(id);
            BO.Customer sender = GetBLCustomer(parcel.Sender.Id);
            BO.Customer target = GetBLCustomer(parcel.Target.Id);
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
                Distatnce = sender.Distance(target),
            };
            return parcelInPassing;
        }

        //----------------------------------ParcelInCustomer GetObject Methods---------------------------------
       
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

        //----------------------------------Customer GetObject Methods---------------------------------
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

        public CustomerInParcel GetBLCustomrInParcel(string id)
        {

            return CustomrInParcelDOtOBO(dal.GetCustomer(id));
        }

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




