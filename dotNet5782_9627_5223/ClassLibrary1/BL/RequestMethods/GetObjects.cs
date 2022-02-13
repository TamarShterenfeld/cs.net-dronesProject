using System;
using System.Collections.Generic;
using System.Text;
using BO;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;



namespace IBL
{
    public partial class BL
    {

        //----------------------------------BaseStation GetObject Methods---------------------------------
        [MethodImpl(MethodImplOptions.Synchronized)]
        public BaseStation GetBLBaseStation(int id)
        {
            lock (dal)
            {
                return ConvertBaseStationDOtOBO(dal.GetBaseStation(id));
            }
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public BaseStationForList GetBaseStationForList(int id)
        {
            BaseStation item = GetBLBaseStation(id);
            BaseStationForList current = new()
            {
                Id = item.Id,
                AvailableChargeSlots = item.ChargeSlots,
                CaughtChargeSlots = dal.CaughtChargeSlots(item.Id),
                Name = item.Name,
            };
            return current;
        }

        //----------------------------------Drone GetObject Methods---------------------------------


        [MethodImpl(MethodImplOptions.Synchronized)]
        public Drone GetBLDrone(int id)
        {
            lock (dal)
            {
                Drone drone = ConvertDroneDOtOBO(dal.GetDrone(id));
                DroneForList drone1 = dronesForList.First(item => item.Id == drone.Id);
                drone.Battery = drone1.Battery; drone.Status = drone1.Status; drone.Parcel.Id = drone1.ParcelId;
                drone.Location = drone1.Location;
                return drone;
            }
        }



        //----------------------------------DroneForList GetObject Methods---------------------------------

        [MethodImpl(MethodImplOptions.Synchronized)]
        public DroneForList GetDroneForList(int id)
        {
            Drone item = GetBLDrone(id);
            if (item.Parcel != null)
                return new DroneForList(item.Id, item.Parcel.Id, item.Model, item.MaxWeight, item.Battery, item.Status, item.Location);
            else
                return new DroneForList(item.Id, 0, item.Model, item.MaxWeight, item.Battery, item.Status, item.Location);
        }

        //----------------------------------DroneInParcel GetObject Methods---------------------------------
        [MethodImpl(MethodImplOptions.Synchronized)]
        public DroneInParcel GetBLDroneInParcel(int id)
        {
            if (id == 0)
            {
                return null;
            }
            return GetDroneInParcel(id);
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
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

        [MethodImpl(MethodImplOptions.Synchronized)]
        public Parcel GetBLParcel(int id)
        {
            lock (dal)
            {
                return ParcelDOtOBO(dal.GetParcel(id));
            }

        }

        //----------------------------------ParcelForList GetObject Methods---------------------------------
        [MethodImpl(MethodImplOptions.Synchronized)]
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
        [MethodImpl(MethodImplOptions.Synchronized)]
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
        [MethodImpl(MethodImplOptions.Synchronized)]
        public Customer GetBLCustomer(string id)
        {
            lock (dal)
            {
                DO.Customer customer = dal.GetCustomer(id);
                return ConvertCustomerDoToBo(customer);
            }

        }

        [MethodImpl(MethodImplOptions.Synchronized)]
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

        [MethodImpl(MethodImplOptions.Synchronized)]
        public CustomerInParcel GetCustomrInParcel(string id)
        {
            lock(dal)
            {
                return ConvertCustomerDoToCustomerInParcel(dal.GetCustomer(id));
            }
        }
    }
}




