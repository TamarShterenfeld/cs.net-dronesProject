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

        public void UpdateDrone(int id, string model)
        {
            IDal.DO.Drone drone = dal.GetDrone(id);
            drone.Model = model;
            //dronesForList. לעדכן ברשימת הרחפנים בב.ל
            dal.UpdateDrone(drone, id);
        }

        public void UpdateBaseStation(int id, string name, string num)
        {
            IDal.DO.BaseStation station = dal.GetBaseStation(id);
            if (name != null) { station.Name = name; }
            if (num != null) { station.ChargeSlots = int.Parse(num); }
            dal.UpdateBaseStation(station, id);
        }

        public void UpdateCustomer(string id, string name, string phone)
        {
            IDal.DO.Customer station = dal.GetCustomer(id);
            if (name != null) { station.Name = name; }
            if (phone != null) { station.Phone = phone; }
            dal.UpdateCustomer(station, id);
        }

        /// <summary>
        /// The function gives associate date to the parcel.
        /// </summary>
        /// <param name="parcelId">parcel id</param>
        /// <param name="droneId">drone id</param>
        public void AssociateParcel(int droneId)
        {
            List<Drone> dronesList = (List<Drone>)dal.GetDronesList();

            List<Customer> customersList = (List<Customer>)dal.GetCustomersList();
            int droneIndex = dronesList.FindIndex(item => item.Id == droneId);
            if (droneIndex == -1)
                throw new IntIndexException(droneIndex);
            else
            {
                Drone currDrone = dronesList[droneIndex];
                if (currDrone.Status == DroneStatuses.Available)
                {
                    List<Parcel> parcels = (List<Parcel>)dal.GetParcelsList()
                        .OrderByDescending(parcel => (int)parcel.Priority)
                        .ThenByDescending(parcel => (int)parcel.Weight)
                        .ThenBy(parcel => customersList.First(customer => customer.Id == parcel.SenderId).Distance(currDrone));

                    foreach (var item in parcels)
                    {
                        Drone currentDrone = currDrone;
                        BO.Customer sender = GetBOCustomersList().First(item1 => item1.Id == item.Sender.Id);
                        BO.Customer target = GetBOCustomersList().First(item1 => item1.Id == item.Target.Id);
                        double minBattery1 = ComputeMinBatteryNeeded(currentDrone, sender);
                        if (currDrone.Battery -  minBattery1 > 0)
                        {
                            currentDrone.Battery -= minBattery1;
                            currentDrone.Location = sender.Location;
                            double minBattery2 = ComputeMinBatteryNeeded(currentDrone, target);
                            if (currentDrone.Battery - minBattery2 >= 0)
                            {
                                currentDrone.Battery -= minBattery2;
                                currentDrone.Location = target.Location;
                                BaseStation nearestBaseStation = NearestBaseStation(currentDrone);
                                double minBattery3 = ComputeMinBatteryNeeded(currentDrone, nearestBaseStation);
                                if ((currentDrone.Battery - minBattery3) >= 0)
                                {
                                    currDrone.Parcel = GetParcelInPassing(item.Id);
                                    //if(currentDrone.Battery - minBattery3 == 0)
                                    //{
                                        
                                    //    nearestBaseStation.DroneCharging.Add(GetDr)
                                    //}

                                    
                                        
                                }
                            }
                        }
                    }
                }

            }
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
                if (parcel.PickUpDate != new DateTime(01 / 01 / 0001))
                {
                    List<Customer> customersList = (List<Customer>)dal.GetCustomersList();
                    string senderId = parcel.Sender.Id;
                    Customer senderCustomer = customersList.First(item => item.Id == senderId);
                    currDrone.Location = senderCustomer.Location;
                    parcel.PickUpDate = DateTime.Now;
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
                throw new IntIdException(droneIndex);
            else
            {
                Drone currDrone = dronesList[droneIndex];
                int parcelId = currDrone.Parcel.Id;
                List<Parcel> parcelsList = (List<Parcel>)dal.GetParcelsList();
                Parcel parcel = parcelsList.First(item => item.Id == parcelId);
                //check if the associated parcel has been picked up and still wasn't supplied.
                if (parcel.PickUpDate != new DateTime(01 / 01 / 0001))
                {
                    List<Customer> customersList = (List<Customer>)dal.GetCustomersList();
                    if (parcel.SupplyDate == new DateTime(01 / 01 / 0001))
                    {
                        Customer targetCustomer = customersList.First(item => item.Id == parcel.Target.Id);
                        currDrone.Status = DroneStatuses.Available;
                        currDrone.Location = targetCustomer.Location;
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
            InputIntValue(ref droneId);
            List<Drone> DronesList = (List<Drone>)dal.GetDronesList();
            int findIndex = DronesList.FindIndex(item => item.Id == droneId);
            if (findIndex == -1)
                throw new IntIdException(findIndex);
            Drone drone = DronesList.First(item => item.Id == droneId);
            int droneIndex = DronesList.FindIndex(item => item.Id == droneId);
        }
        public void ReleaseDroneFromRecharge(int droneId)
        {
            List<Drone> dronesList = (List<Drone>)dal.GetDronesList();
            List<DroneInCharging> DroneChargeList = (List<DroneInCharging>)dal.GetDronesCharge();
            List<BaseStation> baseStationsList = (List<BaseStation>)dal.GetBaseStationsList();
            int findIndex = dronesList.FindIndex(item => item.Id == droneId);
            if (findIndex == -1)
                throw new IntIdException(findIndex);
            findIndex = DroneChargeList.FindIndex(item => item.Id == droneId);
            if (findIndex == -1)
                throw new IntIdException(findIndex);
            Drone drone = dronesList.First(item => item.Id == droneId);
            int droneIndex = dronesList.FindIndex(item => item.Id == droneId);
            DroneInCharging droneCharge = DroneChargeList.First(item => item.Id == droneId);
        }
    }
}


