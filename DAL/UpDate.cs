using System;
using System.Collections.Generic;
using System.Text;
using static IDAL.DO.IDAL;
using IDAL.DO;
using static DalObject.DataSource;
using static DalObject.DataSource.Config;
using System.Linq;

namespace DalObject
{
    public partial class DalObject
    {
        /// <summary>
        /// The function gives associate time to the parcel.
        /// </summary>
        /// <param name="parcelId">parcel id</param>
        /// <param name="droneId">drone id</param>
        public static void AssociateParcel(int parcelId, int droneId)
        {
            if(ParcelsList.FindIndex(item => item.Id == parcelId) ==-1 || DronesList.FindIndex(item=>item.Id == droneId) == -1)
                new Exception("parcelId or droneId don't exist!");
            Parcel parcel = ParcelsList.First(item => item.Id == parcelId);
            Drone drone = DronesList.First(item => item.Id == droneId);
            if (drone.Status != DroneStatuses.Available)
                new Exception("the requested drone isn't available!");
            parcel.Association = DateTime.Now;
            parcel.DroneId = droneId;
            //updating the associated drone's status to be as shipment.
            drone.Status = DroneStatuses.Shipment;
        }

        /// <summary>
        /// The function gives pick up time to the parcel.
        /// </summary>
        /// <param name="parcelId">parcel id</param>
        /// <param name="senderId">sender id</param>
        public static void PickUpParcel(int parcelId, string senderId)
        {
            if (ParcelsList.FindIndex(item => item.Id == parcelId) == -1 || CustomersList.FindIndex(item => item.Id == senderId) == -1)
                throw new Exception("parcelId or senderId don't exist!");
            Parcel parcel = ParcelsList.First(item => item.Id == parcelId);
            parcel.SenderId = senderId;
            parcel.PickingUp = DateTime.Now;
        }

        /// <summary>
        /// The function gives supply time to the parcel.
        /// </summary>
        /// <param name="parcelId">parcel id</param>
        /// <param name="targetId">target id</param>
        public static void SupplyParcel(int parcelId, string targetId)
        {
            if (ParcelsList.FindIndex(item => item.Id == parcelId) == -1 || CustomersList.FindIndex(item=>item.Id == targetId) == -1)
                throw new Exception("parcelId or targetId don't exist!");
            Parcel parcel = ParcelsList.First(item => item.Id == parcelId);
            parcel.Arrival = DateTime.Now;
        }

        /// <summary>
        /// the functuin trys to charge the drone.
        /// </summary>
        /// <param name="droneId">drone's id</param>
        /// <param name="baseStationId">base station's id</param>
        public static void ChargingDrone(int droneId, int baseStationId)
        {
            inputIntValue(ref droneId);
            Drone  drone = DronesList.First(item => item.Id == droneId);
            inputIntValue(ref baseStationId);
            BaseStation baseStation = BaseStationsList.First(item => item.Id == baseStationId);
            if(baseStation.ChargeSlots == 0)      
                throw new Exception("The chosen base station isn't available to charge the drone.");
            if(drone.Status == DroneStatuses.Maintenance)
                Console.WriteLine("The chosen drone is already being charging now");
            DroneCharge droneCharge = new DroneCharge(baseStationId, droneId);
            drone.Status = DroneStatuses.Maintenance;
            DroneChargeList.Add(droneCharge);          
        }

        /// <summary>
        /// the function stops the drone from charging
        /// </summary>
        /// <param name="droneId">drone's id</param>
        public static void StopDroneCharging(int droneId)
        {
            int baseStationId;
            inputIntValue(ref droneId);
            Drone drone = DronesList.First(item => item.Id == droneId);
            DroneCharge droneCharge = DroneChargeList.First(item => item.DroneId == droneId);    
            baseStationId = droneCharge.StationId;
            BaseStation baseStation = BaseStationsList.First(item => item.Id == baseStationId);
            DroneChargeList.Remove(droneCharge);
            baseStation.ChargeSlots++;
            drone.Status = DroneStatuses.Available;
        }
    }
}
