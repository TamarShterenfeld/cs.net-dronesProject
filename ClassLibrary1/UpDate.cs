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
            dal.UpdateDrone(drone, droneId);
            DroneForList droneForList = GetDroneForList(droneId);
            UpdateDroneForList(droneForList, droneId);
        }

        public void UpdateBaseStation(int baseStationId, string name, string chargeSlots)
        {
            IDal.DO.BaseStation station = dal.GetBaseStation(baseStationId);
            if (name != null) { station.Name = name; }
            if (chargeSlots != null) { station.ChargeSlots = int.Parse(chargeSlots); }
            dal.UpdateBaseStation(station, baseStationId);
        }

        public void UpdateCustomer(string customerId, string name, string phone)
        {
            IDal.DO.Customer station = dal.GetCustomer(customerId);
            if (name != null) { station.Name = name; }
            if (phone != null) { station.Phone = phone; }
            dal.UpdateCustomer(station, customerId);
        }

        void UpdateDrone(DroneForList droneForList)
        {
            IDal.DO.Drone drone = dal.GetDrone(droneForList.Id);
            dal.UpdateDrone(drone, drone.Id);
        }


        public void AssociateParcel(int droneId)
        {
            DroneForList currentDrone = GetDroneForList(droneId);
            List<Customer> customersList = (List<Customer>)GetBOCustomersList();
            if (currentDrone.Status == DroneStatuses.Available)
            {
                List<Parcel> parcels = (List<Parcel>)dal.GetParcelsList()
                    .OrderByDescending(parcel => (int)parcel.Priority)
                    .ThenByDescending(parcel => (int)parcel.Weight)
                    .ThenBy(parcel => customersList.First(customer => customer.Id == parcel.SenderId).Distance(currentDrone));

                foreach (var item in parcels)
                {
                    Customer sender = GetBOCustomersList().First(item1 => item1.Id == item.Sender.Id);
                    Customer target = GetBOCustomersList().First(item1 => item1.Id == item.Target.Id);
                    if (DroneReachLocation(currentDrone, sender))
                    {
                        currentDrone.Battery = ComputeBatteryRemaining(currentDrone, sender);
                        currentDrone.Location = sender.Location;
                        if (DroneReachLocation(currentDrone, target))
                        {
                            currentDrone.Battery = ComputeBatteryRemaining(currentDrone, target);
                            currentDrone.Location = target.Location;
                            BaseStation nearestBaseStation = NearestBaseStation(currentDrone);
                            if (DroneReachLocation(currentDrone, nearestBaseStation))
                            {
                                currentDrone.Battery = ComputeBatteryRemaining(currentDrone, nearestBaseStation);
                                currentDrone.Status = DroneStatuses.Shipment;
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
                            }
                        }
                    }
                }
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
            List<Drone> dronesList = (List<Drone>)dal.GetDronesList();
            int droneIndex = dronesList.FindIndex(item => item.Id == droneId);
            if (droneIndex == -1)
                throw new IntIndexException(droneIndex);
            else
            {
                List<Parcel> parcelsList = (List<Parcel>)dal.GetParcelsList();
                Drone currDrone = dronesList[droneIndex];
                int parcelId = currDrone.Parcel.Id;
                Parcel parcel = parcelsList.First(item => item.Id == parcelId);
                int parcelIndex = parcelsList.FindIndex(item => item.Id == parcelId);

                if (parcel.PickUpDate != new DateTime(01 / 01 / 0001))
                {
                    List<Customer> customersList = (List<Customer>)dal.GetCustomersList();
                    string senderId = parcel.Sender.Id;
                    Customer senderCustomer = customersList.First(item => item.Id == senderId);
                    currDrone.Location = senderCustomer.Location;
                    parcel.PickUpDate = DateTime.Now;
                    //update data in the DAL logic level.
                    dronesList[droneIndex] = currDrone;
                    parcelsList[parcelIndex] = parcel;
                    //update the data in the dronesforlist.
                    DroneForList drone = GetDroneForList(currDrone.Id);
                    int droneForListIndex = dronesForList.FindIndex(item => item.Id == currDrone.Id);
                    dronesForList[droneForListIndex] = drone;
                }
                else
                {
                    throw new DateTimeException(parcel.PickUpDate);
                }
            }
        }

        /// <summary>
        /// The function gives arrival date to the parcel.
        /// </summary>
        /// <param name="parcelId">parcel id</param>
        /// <param name="targetId">target id</param>
        public void SupplyParcel(int droneId)
        {
            List<Drone> dronesList = (List<Drone>)dal.GetDronesList();
            int droneIndex = dronesList.FindIndex(item => item.Id == droneId);
            if (droneIndex == -1)
                throw new IntIdException(droneId);
            else
            {
                Drone currDrone = dronesList[droneIndex];
                int parcelId = currDrone.Parcel.Id;
                List<Parcel> parcelsList = (List<Parcel>)dal.GetParcelsList();
                Parcel parcel = parcelsList.First(item => item.Id == parcelId);
                int parcelIndex = parcelsList.FindIndex(item => item.Id == parcel.Id);
                //check if the associated parcel has been picked up and still wasn't supplied.
                if (parcel.PickUpDate != new DateTime(01 / 01 / 0001))
                {
                    List<Customer> customersList = (List<Customer>)dal.GetCustomersList();
                    if (parcel.SupplyDate == new DateTime(01 / 01 / 0001))
                    {
                        DroneForList drone = GetOneDroneForList(currDrone);
                        Customer targetCustomer = customersList.First(item => item.Id == parcel.Target.Id);
                        currDrone.Status = DroneStatuses.Available;
                        currDrone.Battery = ComputeMinBatteryNeeded(drone, targetCustomer);
                        currDrone.Location = targetCustomer.Location;
                        parcel.SupplyDate = DateTime.Now;
                        //update the data in the DAL logic level.
                        dronesList[droneIndex] = currDrone;
                        parcelsList[parcelIndex] = parcel;
                        //update the data in the object of the BL logic level.
                        drone = GetOneDroneForList(currDrone);
                        int droneForListIndex = dronesForList.FindIndex(item => item.Id == drone.Id);
                        //update the data in the dronesForList.
                        dronesForList[droneForListIndex] = drone;
                    }
                    else
                    {
                        throw new DateTimeException(parcel.SupplyDate);
                    }
                }
                else
                {
                    throw new DateTimeException(parcel.SupplyDate);
                }
            }

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


    }
}


