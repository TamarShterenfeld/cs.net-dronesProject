﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IBL.BO;
using IDal.DO;
using static System.Math;


namespace IBL
{
    /// <summary>
    /// The class BL is the business logic level 
    /// which has the responsibility of pull & calaulating lists, object etc. from the DAL logic level
    /// the pulling of data from the DAL logic level is done by an IDal object - a field in the BL class.
    /// </summary>
    public partial class BL : IBL
    {
        internal IDal.IDal dal;
        List<DroneForList> dronesForList;
        readonly double electricityConsumingOfAvailable;
        readonly double electricityConsumingOfLightWeight;
        readonly double electricityConsumingOfHeavyWeight;
        readonly double electricityConsumingOfAverageWeight;
        readonly double chargeRate;


        public List<DroneForList> DronesForList
        {
            get
            {
                return dronesForList;
            }
            set
            {
                dronesForList = value;
            }

        }

        public BL()
        {
            dal = new DalObject.DalObject();
            double[] droneElectricityInfo = dal.ElectricityConsuming();
            electricityConsumingOfAvailable = droneElectricityInfo[0];
            electricityConsumingOfLightWeight = droneElectricityInfo[1];
            electricityConsumingOfAverageWeight = droneElectricityInfo[2];
            electricityConsumingOfHeavyWeight = droneElectricityInfo[3];
            chargeRate = droneElectricityInfo[4];
            List<BO.Drone> drones = (List<BO.Drone>)GetBODronesList();
            Random rand = new();

            for (int i = 0; i < drones.Count; i++)
            {

                BO.Parcel parcelOfDrone = GetBLParcel(drones[i].Parcel.Id);
                //the parcel hasn't been supplied.
                if (parcelOfDrone.SupplyDate == new DateTime(01 / 01 / 0001) &&
                    drones[i].Status == BO.DroneStatuses.Shipment)
                {
                    drones[i].Status = BO.DroneStatuses.Shipment;

                    //the parcel has been accosiated and hasn't been picked up.
                    if (parcelOfDrone.AssociationDate == new DateTime(01 / 01 / 0001) &&
                        parcelOfDrone.PickUpDate != new DateTime(01 / 01 / 0001))
                    {
                        drones[i].Location = NearestBaseStation(drones[i]).Location;
                    }

                    //the parcel has been picked hasn't been supplied - (in the general condition)
                    else
                    {
                        drones[i].Location = GetBOCustomersList().First(item => drones[i].Parcel.Sender.Id == item.Id).Location;
                    }

                    double minBattery = ComputeMinBatteryNeeded(drones[i], GetBLCustomer(drones[i].Parcel.Target.Id));
                    if (minBattery != -1)
                    {
                        drones[i].Battery = RandomBattery(minBattery);
                    }
                }
                else
                {
                    drones[i].Status = (BO.DroneStatuses)rand.Next(1, 3);
                    List<BO.BaseStation> baseStationList = (List<BO.BaseStation>)GetBOBaseStationsList();
                    List<BO.Customer> customersList = (List<BO.Customer>)GetBOCustomersList();
                    switch (drones[i].Status)
                    {
                        case BO.DroneStatuses.Available:
                            {
                                List<BO.CustomerForList> customerForLists = (List<CustomerForList>)CustomersWithSuppliedParcels();
                                if (customerForLists.Count != 0)
                                {
                                    BO.Customer chosenCustomer = customersList.First(item => item.Id == customerForLists[rand.Next(0, customerForLists.Count - 1)].Id);
                                    drones[i].Location = chosenCustomer.Location;
                                    double minBattery = ComputeMinBatteryNeeded(drones[i], chosenCustomer);
                                    drones[i].Battery = RandomBattery(minBattery);
                                }
                                break;
                            }

                        case BO.DroneStatuses.Maintenance:
                            {
                                drones[i].Location = baseStationList[rand.Next(0, baseStationList.Count - 1)].Location;
                                drones[i].Battery = rand.Next(0, 20);
                                break;
                            }
                    }
                }
            }
        }






        /// <summary>
        /// the function returns the nearest BaseStation to the input drone.
        /// </summary>
        /// <param name="drone"></param>
        /// <returns></returns>

        private BO.BaseStation NearestBaseStation(BO.Drone drone)
        {
            double minDistance = int.MaxValue;
            BO.BaseStation nearestBaseStation =
                       GetBOBaseStationsList().Last
                       (item => Min(item.Distance(drone), minDistance) == item.Distance(drone));
            return nearestBaseStation;
        }

        /// <summary>
        /// the function computes the min battery that is needed
        /// to the drone for reaching to the location of an ILocatable location
        /// the calculation is depended the distance + the drone's status.
        /// </summary>
        /// <param name="drone"></param>
        /// <param name="ilocatable"></param>
        /// <returns></returns>
        private double ComputeMinBatteryNeeded(BO.Drone drone, ILocatable ilocatable)
        {
            double distance = drone.Distance(ilocatable);

            if (drone.Status == BO.DroneStatuses.Available)
            {

                return distance * electricityConsumingOfAvailable;
            }
            else
            {
                switch (drone.MaxWeight)
                {
                    case BO.WeightCategories.Light:
                        {
                            return electricityConsumingOfLightWeight * distance;
                        }
                    case BO.WeightCategories.Average:
                        {
                            return electricityConsumingOfAverageWeight * distance;
                        }
                    case BO.WeightCategories.Heavy:
                        {
                            return electricityConsumingOfHeavyWeight * distance;
                        }
                }

                //the distance wasn't succeeded to be computed.
                return -1;
            }
        }

        /// <summary>
        /// the function gets a double minBattery value,
        /// it rounds the value (if there is a need) and random a value between it to 100.
        /// </summary>
        /// <param name="minBattery"></param>
        /// <returns></returns>
        static double RandomBattery(double minBattery)
        {
            Random rand = new();
            double randBattery = rand.Next((int)minBattery, 100);
            //check if the minBattery holds a real double value
            double fractionalPart = minBattery - (int)minBattery;
            if (fractionalPart > 0)
            {
                double randomFraction = rand.NextDouble();
                //adds the highest fractional value
                //between the original fractional part to the random fraction.
                randBattery += Max(randomFraction, fractionalPart);
            }
            return randBattery;
        }

        /// <summary>
        /// the function returns a list of all the customers that has supplied at least one parcel
        /// </summary>
        /// <returns></returns>
        private IEnumerable<BO.CustomerForList> CustomersWithSuppliedParcels()
        {
            List<BO.CustomerForList> customers = (List<BO.CustomerForList>)GetBOCustomersList();
            List<BO.CustomerForList> customerWithSuppliedParcels = new();
            foreach (BO.CustomerForList item in customers)
            {
                if (item.AmountOfSendAndSuppliedParcels > 0)
                {
                    customerWithSuppliedParcels.Add(item);
                }
            }
            return customerWithSuppliedParcels;
        }
    }
}
