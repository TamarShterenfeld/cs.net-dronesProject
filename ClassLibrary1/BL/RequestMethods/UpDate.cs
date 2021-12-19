using IBL.BO;
using System;
using System.Collections.Generic;
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
            DroneForList droneForList = dronesForList.First(drone => drone.Id == droneId);
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
            UpDateDroneForList(droneForList);
            IDal.DO.Drone drone = ConvertBoToDoDrone(ConvertDroneForListToDrone(droneForList));
            dal.UpDate(drone, drone.Id);
        }

        void UpDateBaseStation(BaseStation baseStation)
        {
            IDal.DO.BaseStation baseStation1 = dal.GetBaseStation(baseStation.Id);
            dal.UpDate(baseStation1, baseStation1.Id);
        }
        public void UpDateDroneForList(DroneForList droneForList)
        {
            dronesForList.Remove(dronesForList.First(item => item.Id == droneForList.Id));
            dronesForList.Add(droneForList);
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
                var parcels = dal.GetParcelsList()
                     .OrderByDescending(parcel => (int)parcel.Priority)
                     .ThenByDescending(parcel => (int)parcel.Weight)
                     .ThenBy(parcel => customersList.First(customer => customer.Id == parcel.SenderId).Distance(currentDrone));
                List<BaseStationForList> availableBaseStations = (List<BaseStationForList>)GetAvailableChargeSlots();
                foreach (var item in parcels)
                {
                    Parcel parcel = GetBLParcel(item.Id);
                    if (DroneReachLastDestination(currentDrone, parcel))
                    {
                        if (availableBaseStations != null)
                        {
                            Customer target = GetBLCustomer(parcel.Target.Id);
                            List<BaseStation> baseStations1 = (List<BaseStation>)ConvertBaseStationsForListToBaseStation(availableBaseStations);
                            BaseStation nearestBaseStation = NearestBaseStation(currentDrone, baseStations1);
                            isAssociate = true;
                            currentDrone.Status = DroneStatuses.Shipment;
                            currentDrone.ParcelId = parcel.Id;
                            if (BatteryRemainedInLastDestination(currentDrone, parcel) == 0)
                            {
                                if (nearestBaseStation.ChargeSlots > 0)
                                {
                                    currentDrone.Status = DroneStatuses.Maintenance;
                                    IDal.DO.DroneCharge droneCharge = new() { DroneId = currentDrone.Id, StationId = nearestBaseStation.Id, EntryTime = DateTime.Now };
                                    dal.Add(droneCharge);
                                }
                            }
                        }
                        int droneIndex = dronesForList.FindIndex(item => item.Id == currentDrone.Id);
                        UpdateDrone(currentDrone);
                        parcel.AssociationDate = DateTime.Now;
                        parcel.MyDrone = GetBLDroneInParcel(droneId);
                        UpdateParcel(parcel);
                    } 
                }
                if (isAssociate == false)
                    throw new ParcelActionsException(ParcelActions.Associate);
            }
            else
                throw new DroneStatusException(currentDrone.Status);
        }


        public void PickUpParcel(int droneId)
        {
            bool isPickedUp;
            DroneForList currDrone = GetDroneForList(droneId);
            ParcelForList parcelForList = GetParcelForList(currDrone.ParcelId);
            Customer sender = GetBLCustomer(parcelForList.SenderId);
            //the drone is in shipment status' but the parcel still wasn't picked up.
            if (currDrone.Status == DroneStatuses.Shipment)
            {
                if (parcelForList.Status == ParcelStatuses.Associated)
                {
                    isPickedUp = true;
                    currDrone.Battery = ComputeBatteryRemained(currDrone, sender);
                    currDrone.Location = sender.Location;
                    Parcel parcel = ConvertParcelForListToParcel(parcelForList);
                    parcel.PickUpDate = DateTime.Now;
                    UpdateDrone(currDrone);
                    UpdateParcel(parcel);
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
            Parcel parcel1 = GetBLParcel(drone.ParcelId);
            Customer target = GetBLCustomer(parcel1.Target.Id);
            DateTime? time = null;
            if (parcel1.PickUpDate != time ) 
            {
                if (parcel1.SupplyDate == time)
                {
                    ParcelForList parcelForList = GetParcelForList(drone.ParcelId);
                    isSupplied = true;
                    drone.Battery = ComputeMinBatteryNeeded(drone, target);
                    drone.Location = target.Location;
                    drone.Status = DroneStatuses.Available;
                    parcel1.SupplyDate = DateTime.Now;
                    UpdateParcel(parcel1);
                    UpdateDrone(drone);
                }
                else
                    throw new ParcelStatusException(ParcelStatuses.Associated);
            }
            else
                throw new ParcelStatusException(ParcelStatuses.PickedUp);
            if (isSupplied == false)
                throw new ParcelActionsException(ParcelActions.Supply);
        }

        public void SendDroneForCharge(int droneId)
        {
            DroneForList drone = GetDroneForList(droneId);
            if (drone.Status == DroneStatuses.Available)
            {
                List<BaseStationForList> availableBaseStations = (List<BaseStationForList>)GetAvailableChargeSlots();
                if (availableBaseStations != null)
                {
                    List<BaseStation> baseStations1 = (List<BaseStation>)ConvertBaseStationsForListToBaseStation(availableBaseStations);
                    BaseStation baseStation = NearestBaseStation(drone, baseStations1);
                    double battery = ComputeBatteryRemained(drone, baseStation);
                    if (baseStation.ChargeSlots == 0)
                        throw new BLChargeSlotsException(0);
                    if (battery < 0)
                        throw new BatteryException(drone.Battery);
                    drone.Battery = battery;
                    drone.Location = baseStation.Location;
                    drone.Status = DroneStatuses.Maintenance;
                    DroneInCharging drone1 = new() { Battery = drone.Battery, Id = droneId };
                    baseStation.DroneCharging = new();
                    baseStation.DroneCharging.Add(drone1);
                    //the amount of available chargeSlots is decrease by one
                    //while adding the drone to chargeDrone. 
                    UpdateDrone(drone);
                    dal.SendDroneToRecharge(drone.Id, baseStation.Id);
                    UpDateBaseStation(baseStation);
                }
                else
                    throw new ParcelActionsException(ParcelActions.SendforRecharge);

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
                            myBattery = electricityConsumingOfLightWeight;
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
                //the chargeSlots is increased by one within the function 'Remove'
                //which treats the case a removing of a droneCharge from DronesChargeList occurs.
                UpDateDroneForList(drone);
                IDal.DO.Drone drone1 = dal.GetDrone(drone.Id);
                dal.UpDate(drone1, drone1.Id);
                dal.ReleaseDroneFromRecharge(drone.Id);

            }
            else
                throw new DroneStatusException(drone.Status);
        }


    }
}


