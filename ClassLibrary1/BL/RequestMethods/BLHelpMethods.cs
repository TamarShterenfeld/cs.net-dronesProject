using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO;
using static System.Math;

namespace IBL
{
    public partial class BL 
    {
        /// <summary>
        /// the function returns the nearest BaseStation to the input drone.
        /// </summary>
        /// <param name="location"></param>
        /// <param name="baseStations">the list of the basStations that the nearest baseStation has to be found from.</param>
        /// <returns>the nearest baseStation</returns>
        static BaseStation NearestBaseStation(ILocatable location, List<BaseStation> baseStations)
        {
            double minDistance = int.MaxValue;
            BaseStation nearestBaseStation = new();

            foreach (BaseStation item in baseStations)
            {
                if (item.Distance(location) < minDistance)
                {
                    minDistance = item.Distance(location);
                    nearestBaseStation = item;
                }
            }
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
        private double ComputeMinBatteryNeeded(DroneForList drone, ILocatable ilocatable)
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
