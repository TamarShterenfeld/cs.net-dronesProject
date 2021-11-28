using System;
using System.Collections.Generic;
using System.Text;
using IBL.BO;
using System.Linq;
using DAL.DO;

namespace IBL
{
    public partial class BL : IBL
    {

        public void UpdateDrone(int droneId, string model)
        {
            IDal.DO.Drone drone = dal.GetDrone(droneId);
            drone.Model = model;
            dal.UpDate(drone, droneId);
            DroneForList droneForList = GetDroneForList(droneId);
            UpdateDroneForList(droneForList, droneId);
        }

        public void UpdateBaseStation(int baseStationId, string name, string chargeSlots)
        {
            IDal.DO.BaseStation station = dal.GetBaseStation(baseStationId);
            if (name != null) { station.Name = name; }
            if (chargeSlots != null) { station.ChargeSlots = int.Parse(chargeSlots); }
            dal.UpDate(station, baseStationId);
        }

        public void UpdateCustomer(string customerId, string name, string phone)
        {
            IDal.DO.Customer station = dal.GetCustomer(customerId);
            if (name != null) { station.Name = name; }
            if (phone != null) { station.Phone = phone; }
            dal.UpDate(station, customerId);
        }

        public void UpdateDrone(DroneForList droneForList)
        {
            IDal.DO.Drone drone = ConvertBoToDoDrone(ConvertDroneForListToDrone(droneForList));
            dal.UpDate(drone, drone.Id);
        }

        public void UpdateParcel(Parcel parcel)
        {
            IDal.DO.Parcel parcel1 = ConvertBoToDoParcel(parcel);
            dal.UpDate(parcel1, parcel1.Id);
        }

        public void AssociateParcel(int droneId)
        {
            DroneForList currentDrone = GetDroneForList(droneId);
            List<Customer> customersList = (List<Customer>)GetBOCustomersList();
            bool isAssociate = false;
            if (currentDrone.Status == DroneStatuses.Available)
            {
                List<Parcel> parcels = (List<Parcel>)dal.GetParcelsList()
                    .OrderByDescending(parcel => (int)parcel.Priority)
                    .ThenByDescending(parcel => (int)parcel.Weight)
                    .ThenBy(parcel => customersList.First(customer => customer.Id == parcel.SenderId).Distance(currentDrone));
                foreach (var item in parcels)
                {
                    if (DroneReachLastDestination(currentDrone, item))
                    {
                        isAssociate = true;
                        currentDrone.Status = DroneStatuses.Shipment;
                        currentDrone.ParcelId = item.Id;
                        BaseStation nearestBaseStation = NearestBaseStation(currentDrone);//currentDrone instead of target
                        //there's a need to charge the drone in the nearest baseStation.
                        if (currentDrone.Battery == 0)
                        {
                            if (nearestBaseStation.ChargeSlots > 0)
                            {
                                currentDrone.Status = DroneStatuses.Maintenance;
                                IDal.DO.DroneCharge droneCharge = new() { DroneId = currentDrone.Id, StationId = nearestBaseStation.Id, EntryTime = DateTime.Now };
                                dal.Add(droneCharge);
                            }
                            else
                                throw new ChargeSlotsException(nearestBaseStation.ChargeSlots);
                        }
                        int droneIndex = dronesForList.FindIndex(item => item.Id == currentDrone.Id);
                        UpdateDroneForList(currentDrone, droneIndex);
                        UpdateDrone(currentDrone);
                        item.AssociationDate = DateTime.Now;
                        item.MyDrone = GetBLDroneInParcel(droneId);
                        UpdateParcel(item);
                    }
                }
                if (isAssociate == false)
                    throw new ActionException(Actions.Associate);
            }
            else
                throw new DroneStatusException(currentDrone.Status);
        }



        /// <summary>
        /// The function gives pick up date to the parcel.
        /// </summary>
        /// <param name="parcelId">parcel id</param>
        /// <param name="senderId">sender id</param>
        public void PickUpParcel(int droneId)
        {
            List<Parcel> parcelsList = (List<Parcel>)dal.GetParcelsList();
            DroneForList currDrone = GetDroneForList(droneId);
            ParcelForList parcelForList = GetParcelForList(currDrone.ParcelId);
            Customer sender = GetBLCustomer(parcelForList.SenderId);
            //the drone is in shipment status' but the parcel still wasn't picked up.
            if (currDrone.Status == DroneStatuses.Shipment)
            {
                if (parcelForList.Status == ParcelStatuses.Associated)
                {
                    currDrone.Battery = ComputeMinBatteryNeeded(currDrone, sender);
                    currDrone.Location = sender.Location;
                    Parcel parcel = ConvertParcelForListToParcel(parcelForList);
                    parcel.PickUpDate = DateTime.Now;
                }
                else
                    throw new ParcelStatusException(parcelForList.Status);
            }
            else
                throw new DroneStatusException(currDrone.Status);
        }

        /// <summary>
        /// The function gives arrival date to the parcel.
        /// </summary>
        /// <param name="parcelId">parcel id</param>
        /// <param name="targetId">target id</param>
        public void SupplyParcel(int droneId)
        {
            DroneForList drone = GetDroneForList(droneId);
            ParcelForList parcelForList = GetParcelForList(drone.ParcelId);
            Customer target = GetBLCustomer(parcelForList.TargetId);
            if (parcelForList.Status != ParcelStatuses.PickedUp)
            {
                drone.Battery = ComputeMinBatteryNeeded(drone, target);
                drone.Location = target.Location;
                drone.Status = DroneStatuses.Available;
                Parcel parcel = GetBLParcel(parcelForList.ParcelId);
                parcel.SupplyDate = DateTime.Now;
                UpdateParcel(parcel);
                UpdateDrone(drone);
            }
            else
                throw new ParcelStatusException(parcelForList.Status);
        }


        /// <summary>
        /// the functuin trys to charge the drone.
        /// </summary>
        /// <param name="droneId">drone's id</param>
        /// <param name="baseStationId">base station's id</param>
        public void ChargeDrone(int droneId)
        {
            DroneForList drone = GetDroneForList(droneId);
            if (drone.Status != DroneStatuses.Available) { throw new DroneStatusException(drone.Status); }
            BaseStation baseStation = NearestBaseStation(drone);
            drone.Battery = ComputeMinBatteryNeeded(drone, baseStation);
            //there's a need to continue the function.
        }


        /// <summary>
        /// the function stops the drone from charging
        /// </summary>
        /// <param name="droneId">drone's id</param>

        public void ReleaseDroneFromRecharge(int droneId)
        {
            List<Drone> dronesList = (List<Drone>)dal.GetDronesList();
            List<DroneInCharging> DroneChargeList = (List<DroneInCharging>)dal.GetDronesCharge();
            List<BaseStation> baseStationsList = (List<BaseStation>)dal.GetBaseStationsList();
            int findIndex = dronesList.FindIndex(item => item.Id == droneId);
            if (findIndex == -1)
                throw new IntIdException(droneId);
            findIndex = DroneChargeList.FindIndex(item => item.Id == droneId);
            if (findIndex == -1)
                throw new IntIdException(droneId);
            Drone drone = dronesList.First(item => item.Id == droneId);
            int droneIndex = dronesList.FindIndex(item => item.Id == droneId);
            DroneInCharging droneCharge = DroneChargeList.First(item => item.Id == droneId);
        }

        void UpdateDroneForList(DroneForList droneForList, int id)
        {
            int index = dronesForList.FindIndex(item => item.Id == id);
            dronesForList[index] = droneForList;
        }

        bool DroneReachLastDestination(DroneForList drone, Parcel item)
        {
            Customer sender = GetBOCustomersList().First(item1 => item1.Id == item.Sender.Id);
            Customer target = GetBOCustomersList().First(item1 => item1.Id == item.Target.Id);
            if (DroneReachLocation(drone, sender))
            {
                drone.Battery = ComputeBatteryRemaining(drone, sender);
                drone.Location = sender.Location;
                if (DroneReachLocation(drone, target))
                {
                    drone.Battery = ComputeBatteryRemaining(drone, target);
                    drone.Location = target.Location;
                    BaseStation nearestBaseStation = NearestBaseStation(drone);
                    return DroneReachLocation(drone, nearestBaseStation);
                }
            }
            return false;
        }
    }
}


