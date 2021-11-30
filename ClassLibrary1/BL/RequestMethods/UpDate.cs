using System;
using System.Collections.Generic;
using System.Text;
using IBL.BO;
using System.Linq;

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
            UpdateDrone(droneForList);
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
                        Customer target = GetBLCustomer(item.Target.Id);
                        BaseStation nearestBaseStation = NearestBaseStation(target, (List<BaseStation>)GetBOBaseStationsList());
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


       
        public void SendDroneForCharge(int droneId)
        {
            DroneForList drone = GetDroneForList(droneId);
            if (drone.Status == DroneStatuses.Available)
            {
                BaseStation baseStation = NearestBaseStation(drone, (List<BaseStation>)GetAvailableChargeSlots());
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
                Drone drone1 = (ConvertDroneForListToDrone(drone));
                dal.SendDroneToRecharge(drone1.Id, baseStation.Id);
            }
            else
                throw new DroneStatusException(drone.Status);
        }


        
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

        
    }
}


