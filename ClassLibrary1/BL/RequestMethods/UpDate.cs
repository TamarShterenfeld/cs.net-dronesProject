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
            DroneForList droneForList = dronesForList.First(drone=>drone.Id == droneId);
            droneForList.Model = model;
            UpdateDroneForList(droneForList);
        }

        public void UpdateBaseStation(int baseStationId, string name, string chargeSlots)
        {
            IDal.DO.BaseStation station = dal.GetBaseStation(baseStationId);
            if (name != null) { station.Name = name; }
            if (chargeSlots != null)
            {
                int chargeSlots1 = int.Parse(chargeSlots);
                //the amount of drones thet charged in this baseStation
                //is bigger than its available chargeSLots.
                if (dal.CaughtChargeSlots(baseStationId) > chargeSlots1)
                    throw new BLChargeSlotsException(chargeSlots1);
                station.ChargeSlots = chargeSlots1;
            }
            dal.UpDate(station, baseStationId);
        }

        public void UpdateCustomer(string customerId, string name, string phone)
        {
            IDal.DO.Customer customer = dal.GetCustomer(customerId);
            if (name != null) { customer.Name = name; }
            if (phone != null) { customer.Phone = phone; }
            dal.UpDate(customer, customerId);
        }

        void UpdateDrone(DroneForList droneForList)
        {
            IDal.DO.Drone drone = ConvertBoToDoDrone(ConvertDroneForListToDrone(droneForList));
            dal.UpDate(drone, drone.Id);
        }

        void UpdateDroneForList(DroneForList droneForList)
        {
            dronesForList.Remove(dronesForList.First(drone => drone.Id == droneForList.Id));
            dronesForList.Add(droneForList);
        }
        void UpdateParcel(Parcel parcel)
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
                        Customer target = GetBLCustomer(item.Target.Id);
                        BaseStation nearestBaseStation = NearestBaseStation(target);
                        isAssociate = true;
                        currentDrone.Status = DroneStatuses.Shipment;
                        currentDrone.ParcelId = item.Id;
                        if (BatteryRemainedInLastDestination(currentDrone, item) == 0)
                        {
                            if (nearestBaseStation.ChargeSlots > 0)
                            {
                                currentDrone.Status = DroneStatuses.Maintenance;
                                IDal.DO.DroneCharge droneCharge = new() { DroneId = currentDrone.Id, StationId = nearestBaseStation.Id, EntryTime = DateTime.Now };
                                dal.Add(droneCharge);
                            }
                            else
                                throw new BLChargeSlotsException(nearestBaseStation.ChargeSlots);
                        }
                    }
                    int droneIndex = dronesForList.FindIndex(item => item.Id == currentDrone.Id);
                    UpdateDrone(currentDrone);
                    UpdateDrone(currentDrone);
                    item.AssociationDate = DateTime.Now;
                    item.MyDrone = GetBLDroneInParcel(droneId);
                    UpdateParcel(item);
                }
            }
            if (isAssociate == false)
                throw new ParcelActionsException(ParcelActions.Associate);
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
            bool isPickedUp = false;
            List<Parcel> parcelsList = (List<Parcel>)dal.GetParcelsList();
            DroneForList currDrone = GetDroneForList(droneId);
            ParcelForList parcelForList = GetParcelForList(currDrone.ParcelId);
            Customer sender = GetBLCustomer(parcelForList.SenderId);
            //the drone is in shipment status' but the parcel still wasn't picked up.
            if (currDrone.Status == DroneStatuses.Shipment)
            {
                if (parcelForList.Status == ParcelStatuses.Associated)
                {
                    isPickedUp = true;
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
            if (isPickedUp == false)
                throw new ParcelActionsException(ParcelActions.PickUp);
        }

        /// <summary>
        /// The function gives arrival date to the parcel.
        /// </summary>
        /// <param name="parcelId">parcel id</param>
        /// <param name="targetId">target id</param>
        public void SupplyParcel(int droneId)
        {
            bool isSupplied = false;
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
            if (isSupplied == false)
                throw new ParcelActionsException(ParcelActions.Supply);
        }


        /// <summary>
        /// the functuin trys to charge the drone.
        /// </summary>
        /// <param name="droneId">drone's id</param>
        /// <param name="baseStationId">base station's id</param>
        public void SendDroneForCharge(int droneId)
        {
            DroneForList drone = GetDroneForList(droneId);
            if (drone.Status == DroneStatuses.Available)
            {
                BaseStation baseStation = NearestBaseStation(drone);
                //while there are no available ChargeSlots
                //and the battery is enough in order to reach the baseStation.
                while (baseStation.ChargeSlots == 0 && ComputeBatteryRemained(drone, baseStation) >= 0)
                {
                    //tries to find another close baseStation
                    //which is closed to the prev nearestBaseStation.
                    baseStation = NearestBaseStation(baseStation);
                }
                double battery = ComputeBatteryRemained(drone, baseStation);
                if (baseStation.ChargeSlots == 0)
                    throw new BLChargeSlotsException(0);
                if (battery < 0)
                    throw new BatteryException(battery);
                drone.Battery = battery;
                drone.Location = baseStation.Location;
                drone.Status = DroneStatuses.Shipment;
                IDal.DO.DroneCharge droneCharge = new() { DroneId = drone.Id, StationId = baseStation.Id, EntryTime = DateTime.Now };
                //the amount of available chargeSlots is decrease by one
                //while adding the drone to chargeDrone. 
                dal.Add(droneCharge);
                UpdateDrone(drone);
            }
            else
                throw new DroneStatusException(drone.Status);
        }


        /// <summary>
        /// the function stops the drone from charging
        /// </summary>
        /// <param name="droneId">drone's id</param>

        public void ReleaseDroneFromRecharge(int droneId, double timeCharge)
        {
            DroneForList drone = GetDroneForList(droneId);
            if (drone.Status == DroneStatuses.Maintenance)
            {
                double myBattery = 1;
                switch (drone.MaxWeight)
                {
                    case BO.WeightCategories.Light:
                        {
                            myBattery =  electricityConsumingOfLightWeight ;
                            break;
                        }
                    case BO.WeightCategories.Average:
                        {
                            myBattery = electricityConsumingOfAverageWeight;
                            break;
                        }
                    case BO.WeightCategories.Heavy:
                        {
                            myBattery = electricityConsumingOfHeavyWeight;
                            break;
                        }
                }
                drone.Battery = myBattery * (timeCharge * chargeRate);
                drone.Status = DroneStatuses.Available;
                dal.ReleaseDroneFromRecharge(drone.Id);
            }
            else
                throw new DroneStatusException(drone.Status);
        }

        bool DroneReachLastDestination(DroneForList drone, Parcel item)
        {
            Customer sender = GetBOCustomersList().First(item1 => item1.Id == item.Sender.Id);
            Customer target = GetBOCustomersList().First(item1 => item1.Id == item.Target.Id);
            if (DroneReachLocation(drone, sender))
            {
                drone.Battery = ComputeBatteryRemained(drone, sender);
                drone.Location = sender.Location;
                if (DroneReachLocation(drone, target))
                {
                    drone.Battery = ComputeBatteryRemained(drone, target);
                    drone.Location = target.Location;
                    BaseStation nearestBaseStation = NearestBaseStation(drone);
                    return DroneReachLocation(drone, nearestBaseStation);
                }
            }
            return false;
        }
        double BatteryRemainedInLastDestination(DroneForList drone, Parcel parcel)
        {
            Customer sender = GetBOCustomersList().First(item1 => item1.Id == parcel.Sender.Id);
            Customer target = GetBOCustomersList().First(item1 => item1.Id == parcel.Target.Id);
            BaseStation baseStation = NearestBaseStation(target);
            drone.Battery = ComputeBatteryRemained(drone, sender);
            drone.Battery = ComputeBatteryRemained(drone, target);
            return ComputeBatteryRemained(drone, baseStation);
        }
    }
}


