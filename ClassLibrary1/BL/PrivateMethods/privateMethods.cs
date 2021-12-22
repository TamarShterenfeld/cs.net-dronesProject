using BO;
using System;
using System.Collections.Generic;
using System.Linq;

namespace IBL
{
    public partial class BL 
    {
        /// <summary>
        /// the function increases the parcel's index
        /// and returns it.
        /// </summary>
        /// <returns>the next parcel index</returns>
        public static int GetParcelIndex()
        {
            return DalApi.DalFactory.GetDal().GetLastParcelId();    
        }

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

    }
}
