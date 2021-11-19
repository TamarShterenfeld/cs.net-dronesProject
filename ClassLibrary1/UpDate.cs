﻿using System;
using System.Collections.Generic;
using System.Text;
using IBL.BO;
using System.Linq;

namespace IBL
{
    public partial class BL : IBL
    {
        /// <summary>
        /// The function gives associate date to the parcel.
        /// </summary>
        /// <param name="parcelId">parcel id</param>
        /// <param name="droneId">drone id</param>
        public void AssociateParcel(int droneId)
        {
            List<Drone> dronesList = (List<Drone>)dal.GetDronesList();
            int droneIndex = dronesList.FindIndex(item => item.Id == droneId);
            if (droneIndex == -1)
                throw new OverloadException("drone id doesn't exist in the dronesList");
            else
            {
                Drone currDrone = dronesList[droneIndex];
                if (currDrone.Status != DroneStatuses.Available)
                {
                    //objListOrder.Sort((x, y) => x.OrderDate.CompareTo(y.OrderDate));
                    List<Parcel> parcelsList = (List<Parcel>)dal.GetParcelsList();
                    List<Customer> customersList = (List<Customer>)dal.GetCustomersList();
                    List<Parcel> PrioritiesList = new List<Parcel>((List<Parcel>)dal.GetParcelsList());
                    List<Parcel> WeightsList = new List<Parcel>((List<Parcel>)dal.GetParcelsList());
                    List<Parcel> LocationList = new List<Parcel>((List<Parcel>)dal.GetParcelsList());
                    List<Location> locations = new List<Location>();
                    List<List<Parcel>> parcelsConditions = new List<List<Parcel>>(3){ PrioritiesList, WeightsList, LocationList };
                 
                    //the counterArray counts for each parcel in the suitable index -
                    //the sum of all the indexes it has appeared in the above three arraies.
                    List<int> counterList = new List<int>();
                    //sorting the three different lists
                    for (int i = 0; i < parcelsConditions.Count; i++)
                    {
                        switch (i)
                        {
                            case 0:
                                {
                                    parcelsConditions[i].Sort((x, y) => x.Priority.CompareTo(y.Priority));
                                    break;
                                }
                            case 1:
                                {
                                    parcelsConditions[i].Sort((x, y) => x.Weight.CompareTo(y.Weight));
                                    break;
                                }
                            case 2:
                                {
                                    foreach (var item in parcelsConditions[i])
                                    {
                                        Customer currCustomer = customersList.First(item1 => item1.Id == item.Sender.Id);
                                        locations.Add(currCustomer.MyLocation);
                                    }
                                  // locations.Sort((x,y)=>x.CoorLatitude.Comp
                                    break;
                                }
                            default:
                                throw new OverloadException("there are no more than three lists to sort.");
                        }

                        foreach (var item1 in parcelsConditions[i])
                        {
                            counterList[i] += parcelsConditions[i].FindIndex(item => item.Id == item1.Id);
                        }
                    }
                    //the parcel which appeared in the first indexes of the sorted lists - 
                    //it means it was one of the most optimal parcels for the current drone.
                    int indexOfOptimalParcel = counterList.Min();
                    //לבדוק בדיוק כמה סוללה הוא צריך על מנת להגיע ליעד
                    while(currDrone.Battery < 50 && counterList.Count != 0)
                    {
                        //if the currDrone can't arrive to the optimal parcel's destination
                        //another parcel will be chosen again.
                        counterList.RemoveAt(indexOfOptimalParcel);
                        indexOfOptimalParcel = counterList.Min();
                    }
                    if(counterList.Count == 0)
                    {
                        throw new OverloadException("there's no parcel that can be associated to this drone.");
                    }
                    Parcel optimalParcel = parcelsList[indexOfOptimalParcel];
                    optimalParcel.MyDrone = new DroneInParcel { Battery = currDrone.Battery, Id = currDrone.Id, CurrentLocation = currDrone.MyLocation };
                    optimalParcel.AssociationDate = DateTime.Now;
                    currDrone.Status = DroneStatuses.Shipment;
                }
                else
                {
                    throw new OverloadException("the drone status isn't available - can't be associated to any parcel!");
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
                throw new OverloadException("drone id doesn't exist in the dronesList");
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
                    currDrone.MyLocation = senderCustomer.MyLocation;
                    parcel.PickUpDate = DateTime.Now;
                }
                else
                {
                    throw new OverloadException("the parcel has been picked up already");
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
                throw new OverloadException("drone id doesn't exist in the dronesList");
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
                        currDrone.MyLocation = targetCustomer.MyLocation;
                    }
                    else
                    {
                        throw new OverloadException("the hasn't still picked up - it can't be supllied by now.");
                    }
                }
                else
                {
                    throw new OverloadException("the parcel hasn't been already picked up - it can't be supllied by now.");
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
            inputIntValue(ref droneId);
            List<Drone> DronesList = (List<Drone>)dal.GetDronesList();
            if (DronesList.FindIndex(item => item.Id == droneId) == -1)
                throw new OverloadException("drone's id doesn't exist in the drones' list.");
            Drone drone = DronesList.First(item => item.Id == droneId);
            int droneIndex = DronesList.FindIndex(item => item.Id == droneId);

            //    inputIntValue(ref baseStationId);
            //    List<BaseStation> BaseStationsList = (List<BaseStation>)dal.GetBaseStationsList();
            //    if (BaseStationsList.FindIndex(item => item.Id == baseStationId) == -1)
            //        throw new OverloadException("drone's id doesn't exist in the drones' list.");
            //    BaseStation baseStation = BaseStationsList.First(item => item.Id == baseStationId);
            //    int baseStationIndex = BaseStationsList.FindIndex(item => item.Id == baseStationId);
            //    if (baseStation.ChargeSlots == 0)
            //        throw new OverloadException("The chosen base station isn't available to charge the drone.");

            //    DroneInCharging droneCharge = new DroneInCharging(baseStationId, droneId);
            //    List<DroneInCharging> dronesChargeList = (List<DroneInCharging>)dal.GetDronesCharge();
            //    dronesChargeList.Add(droneCharge);

            //    DronesList[droneIndex] = drone;
            //    BaseStationsList[baseStationIndex] = baseStation;
            //}

            /// <summary>
            /// the function stops the drone from charging
            /// </summary>
            /// <param name="droneId">drone's id</param>
        }
        public void ReleaseDroneFromRecharge(int droneId)
        {
            List<Drone> dronesList = (List<Drone>)dal.GetDronesList();
            List<DroneInCharging> DroneChargeList = (List<DroneInCharging>)dal.GetDronesCharge();
            List<BaseStation> baseStationsList = (List<BaseStation>)dal.GetBaseStationsList();
            int baseStationId;
            inputIntValue(ref droneId);
            if (dronesList.FindIndex(item => item.Id == droneId) == -1)
                throw new OverloadException("drone's id doesn't exist in the drones' list.");
            if (DroneChargeList.FindIndex(item => item.Id == droneId) == -1)
                throw new OverloadException("drone's id doesn't exist in the dronecharge list.");

            Drone drone = dronesList.First(item => item.Id == droneId);
            int droneIndex = dronesList.FindIndex(item => item.Id == droneId);
            DroneInCharging droneCharge = DroneChargeList.First(item => item.Id == droneId);
            //baseStationId =baseStationsList.First(item=>item. droneCharge.;
            //int baseStationIndex = baseStationsList.FindIndex(item => item.Id == baseStationId);
            //if (baseStationIndex  == -1)
            //    throw new OverloadException("baseStation's id doesn't exist in the BaseStation's list.");
            //BaseStation baseStation = baseStationsList.First(item => item.Id == baseStationId);
            //DroneChargeList.Remove(droneCharge);
            //baseStation.ChargeSlots++;
            //baseStationsList[baseStationIndex] = baseStation;
            //dronesList[droneIndex] = drone;
        }
    }
}


