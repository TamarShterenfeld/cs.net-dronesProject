using BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using static System.Math;
namespace IBL
{
    public partial class BL 
    {
       

        /// <summary>
        /// the function computes if the drone will succeed in arriving at the locatable's location.
        /// </summary>
        /// <param name="drone">the drone object</param>
        /// <param name="locatable"></param>
        /// <returns>true - if it succeeds in reaching the location, else - false</returns>
        bool DroneReachLocation(DroneForList drone, ILocatable locatable)
        {
            return drone.Battery - ComputeMinBatteryNeeded(drone, locatable) >= 0;
        }


        /// <summary>
        /// the function computes the minimum battery needed for arriving the locatable's location.
        /// </summary>
        /// <param name="drone">the drone object</param>
        /// <param name="locatable">the location it has to arrive at</param>
        /// <returns>the remained battery of the drone after arriving the locatable's location</returns>
        double ComputeBatteryRemained(DroneForList drone, ILocatable locatable)
        {
            return (drone.Battery - ComputeMinBatteryNeeded(drone, locatable));
        }

        /// <summary>
        /// the function computes if the drone succeeds in arriving at:
        /// 1.the sender location.
        /// 2.then, to the target's location.
        /// 3.then, to the nearest baseStation's location.
        /// </summary>
        /// <param name="drone">the drone object</param>
        /// <param name="parcel">the parcel that is supposed to be passed</param>
        /// <returns>true - if the drone succeeds in arriving at all the prev locations, else - false</returns>
        bool DroneReachLastDestination(DroneForList drone, Parcel parcel)
        {
            Customer sender =((List<Customer>)GetBOCustomersList()).First(item1 => item1.Id == parcel.Sender.Id);
            Customer target = GetBOCustomersList().First(item1 => item1.Id == parcel.Target.Id);
            if (DroneReachLocation(drone, sender))
            {
                drone.Battery = ComputeBatteryRemained(drone, sender);
                drone.Location = sender.Location;
                if (DroneReachLocation(drone, target))
                {
                    drone.Battery = ComputeBatteryRemained(drone, target);
                    drone.Location = target.Location;
                    BaseStation nearestBaseStation = NearestBaseStation(drone, (List<BaseStation>)GetBOBaseStationsList());
                    return DroneReachLocation(drone, nearestBaseStation);
                }
            }
            return false;
        }

        /// <summary>
        /// the function computes the remained battery after arriving at:
        /// 1.the sender location.
        /// 2.then, to the target's location.
        /// 3.then, to the nearest baseStation's location.
        /// </summary>
        /// <param name="drone">the drone object</param>
        /// <param name="parcel">the parcel that is supposed to be passed</param>
        /// <returns></returns>
        double BatteryRemainedInLastDestination(DroneForList drone, Parcel parcel)
        {
            Customer sender = GetBOCustomersList().First(item1 => item1.Id == parcel.Sender.Id);
            Customer target = GetBOCustomersList().First(item1 => item1.Id == parcel.Target.Id);
            BaseStation baseStation = NearestBaseStation(target, (List<BaseStation>)GetBOBaseStationsList());
            drone.Battery = ComputeBatteryRemained(drone, sender);
            drone.Battery = ComputeBatteryRemained(drone, target);
            return ComputeBatteryRemained(drone, baseStation);
        }

        /// <summary>
        /// the function returns the nearest BaseStation to the input drone.
        /// </summary>
        /// <param name="location"></param>
        /// <param name="baseStations">the list of the basStations that the nearest baseStation has to be found from.</param>
        /// <returns>the nearest baseStation</returns>
        public BaseStation NearestBaseStation(ILocatable location, List<BaseStation> baseStations)
        {
            double minDistance = baseStations.Min(station => station.Distance(location));
            return baseStations.FirstOrDefault(item =>item.Distance(location) == minDistance);
        }

        /// <summary>
        /// the function computes the min battery that is needed
        /// to the drone for reaching to the location of an ILocatable location
        /// the calculation is depended the distance + the drone's status.
        /// </summary>
        /// <param name="drone"></param>
        /// <param name="ilocatable"></param>
        /// <returns></returns>
        double ComputeMinBatteryNeeded(DroneForList drone, ILocatable ilocatable)
        {
            double distance = drone.Distance(ilocatable);

            if (drone.Status == BO.DroneStatuses.Available)
            {

                return distance * BatteryUsages[0];
            }
            else
            {
                switch (drone.MaxWeight)
                {
                    case BO.WeightCategories.Light:
                        {
                            return BatteryUsages[1] * distance;
                        }
                    case BO.WeightCategories.Average:
                        {
                            return BatteryUsages[2] * distance;
                        }
                    case BO.WeightCategories.Heavy:
                        {
                            return BatteryUsages[3] * distance;
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
        IEnumerable<BO.CustomerForList> CustomersWithSuppliedParcels()
        {
            List<BO.CustomerForList> customers = (List<BO.CustomerForList>)GetCustomersList();
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

