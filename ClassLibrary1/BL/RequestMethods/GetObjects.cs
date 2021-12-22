using System;
using System.Collections.Generic;
using System.Text;
using BO;
using System.Linq;
using System.Numerics;



namespace IBL
{
    public partial class BL 
    {

        //----------------------------------BaseStation GetObject Methods---------------------------------
        public BaseStation GetBLBaseStation(int id)
        {
            return ConvertBaseStationDOtOBO(dal.GetBaseStation(id)); ;
        }

        public BaseStationForList GetBaseStationForList(int id)
        {
            BaseStation item = GetBLBaseStation(id);
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



        public Drone GetBLDrone(int id)
        {
            Drone drone = ConvertDroneDOtOBO(dal.GetDrone(id));
            DroneForList drone1 = dronesForList.First(item => item.Id == drone.Id);
            drone.Battery = drone1.Battery; drone.Status = drone1.Status; drone.Parcel.Id = drone1.ParcelId;
            drone.Location = drone1.Location;
            return drone;
        }



        //----------------------------------DroneForList GetObject Methods---------------------------------

        public DroneForList GetDroneForList(int id)
        {
            Drone item = GetBLDrone(id);
            if (item.Parcel != null)
                return new DroneForList(item.Id, item.Parcel.Id, item.Model, item.MaxWeight, item.Battery, item.Status, item.Location);
            else
                return new DroneForList(item.Id, 0, item.Model, item.MaxWeight, item.Battery, item.Status, item.Location);
        }




        //----------------------------------DroneInParcel GetObject Methods---------------------------------
        public DroneInParcel GetBLDroneInParcel(int id)
        {
            if (id == 0)
            {
                return null;
            }
            return GetDroneInParcel(id);
        }

        public DroneInParcel GetDroneInParcel(int id)
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

        public ParcelStatuses ParcelStatus(DO.Parcel parcel)
        {
            DateTime? time = new();
            if (parcel.AssociationDate == time)
                return ParcelStatuses.Production;
            if (parcel.PickUpDate == time)
                return ParcelStatuses.Associated;
            if (parcel.SupplyDate == time)
                return ParcelStatuses.PickedUp;
            else
                return ParcelStatuses.Supplied;
        }

        public Parcel GetBLParcel(int id)
        {
            return ParcelDOtOBO(dal.GetParcel(id));

        }

        //----------------------------------ParcelForList GetObject Methods---------------------------------
        public ParcelForList GetParcelForList(int id)
        {
            Parcel item = GetBLParcel(id);
            ParcelForList current = new()
            {
                DroneId = item.MyDrone != null ? item.MyDrone.Id : 0,
                ParcelId = item.Id,
                Priority = item.Priority,
                SenderId = item.Sender.Id,
                Status = ParcelStatus(dal.GetParcel(item.Id)),
                TargetId = item.Target.Id,
                Weight = item.Weight
            };
            return current;
        }

        //----------------------------------ParcelInPassing GetObject Methods---------------------------------
        public ParcelInPassing GetParcelInPassing(int id)
        {
            Parcel parcel = GetBLParcel(id);
            Customer sender = GetBLCustomer(parcel.Sender.Id);
            Customer target = GetBLCustomer(parcel.Target.Id);
            ParcelInPassing parcelInPassing = new()
            {
                Id = parcel.Id,
                Weight = parcel.Weight,
                Priority = parcel.Priority,
                ToDestination = parcel.PickUpDate == null,
                Sender = parcel.Sender,
                Target = parcel.Target,
                Collect = GetBLCustomer(parcel.Sender.Id).Location,
                Destination = GetBLCustomer(parcel.Target.Id).Location,
                Distatnce = sender.Distance(target),
            };
            return parcelInPassing;
        }



        //----------------------------------Customer GetObject Methods---------------------------------
        public Customer GetBLCustomer(string id)
        {
            DO.Customer customer = dal.GetCustomer(id);
            return ConvertCustomerDoToBo(customer);

        }


        public CustomerForList GetCustomerForList(string id)
        {
            Customer item = GetBLCustomer(id);
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

        public CustomerInParcel GetCustomrInParcel(string id)
        {

            return ConvertCustomerDoToCustomerInParcel(dal.GetCustomer(id));
        }


    }
}




