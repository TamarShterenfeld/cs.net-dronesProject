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
    public partial class DalObject : IDAL.IDal
    {
        /// <summary>
        /// The function gives associate date to the parcel.
        /// </summary>
        /// <param name="parcelId">parcel id</param>
        /// <param name="droneId">drone id</param>
        public static void AssociateParcel(int parcelId, int droneId)
        {
            if(ParcelsList.FindIndex(item => item.Id == parcelId) ==-1 || DronesList.FindIndex(item=>item.Id == droneId) == -1)
                new Exception("parcelId or droneId don't exist!");
            int parcelIndex = ParcelsList.FindIndex(item => item.Id == parcelId);
            int droneIndex = DronesList.FindIndex(item => item.Id == droneId);
            Parcel parcel = ParcelsList.First(item => item.Id == parcelId);
            Drone drone = DronesList.First(item => item.Id == parcelId);
            parcel.Association = DateTime.Now;
            parcel.DroneId = droneId;
            ParcelsList[parcelIndex] = parcel;
            DronesList[droneIndex] = drone;
        }

        /// <summary>
        /// The function gives pick up date to the parcel.
        /// </summary>
        /// <param name="parcelId">parcel id</param>
        /// <param name="senderId">sender id</param>
        public static void PickUpParcel(int parcelId, string senderId)
        {
            if (ParcelsList.FindIndex(item => item.Id == parcelId) == -1 || CustomersList.FindIndex(item => item.Id == senderId) == -1)
                throw new Exception("parcelId or senderId don't exist in the customers' list!");
            Parcel parcel = ParcelsList.First(item => item.Id == parcelId);
            int parcelIndex = ParcelsList.FindIndex(item => item.Id == parcelId);
            parcel.SenderId = senderId;
            parcel.PickingUp = DateTime.Now;
            ParcelsList[parcelIndex] = parcel;
        }

        /// <summary>
        /// The function gives arrival date to the parcel.
        /// </summary>
        /// <param name="parcelId">parcel id</param>
        /// <param name="targetId">target id</param>
        public static void SupplyParcel(int parcelId, string targetId)
        {
            if (ParcelsList.FindIndex(item => item.Id == parcelId) == -1 || CustomersList.FindIndex(item=>item.Id == targetId) == -1)
                throw new Exception("parcelId or targetId don't exist!");
            Parcel parcel = ParcelsList.First(item => item.Id == parcelId);
            int parcelIndex = ParcelsList.FindIndex(item => item.Id == parcelId);
            parcel.Arrival = DateTime.Now;
            ParcelsList[parcelIndex] = parcel;
        }

        /// <summary>
        /// the functuin trys to charge the drone.
        /// </summary>
        /// <param name="droneId">drone's id</param>
        /// <param name="baseStationId">base station's id</param>
        public static void ChargingDrone(int droneId, int baseStationId)
        {
            inputIntValue(ref droneId);
            if (DronesList.FindIndex(item => item.Id == droneId) == -1)
                throw new Exception("drone's id doesn't exist in the drones' list.");
            Drone  drone = DronesList.First(item => item.Id == droneId);
            int droneIndex = DronesList.FindIndex(item => item.Id == droneId);

            inputIntValue(ref baseStationId);
            if (BaseStationsList.FindIndex(item => item.Id == baseStationId) == -1)
                throw new Exception("drone's id doesn't exist in the drones' list.");
            BaseStation baseStation = BaseStationsList.First(item => item.Id == baseStationId);
            int baseStationIndex = BaseStationsList.FindIndex(item => item.Id == baseStationId);
            if(baseStation.ChargeSlots == 0)      
                throw new Exception("The chosen base station isn't available to charge the drone.");
            DroneCharge droneCharge = new DroneCharge(baseStationId, droneId);
            DroneChargeList.Add(droneCharge);
            DronesList[droneIndex] = drone;
            BaseStationsList[baseStationIndex] = baseStation;
        }

        /// <summary>
        /// the function stops the drone from charging
        /// </summary>
        /// <param name="droneId">drone's id</param>
        public static void StopDroneCharging(int droneId)
        {
            int baseStationId;
            inputIntValue(ref droneId);
            if (DronesList.FindIndex(item => item.Id == droneId) == -1)
                throw new Exception("drone's id doesn't exist in the drones' list.");
            if (DroneChargeList.FindIndex(item => item.DroneId == droneId) == -1)
                throw new Exception("drone's id doesn't exist in the dronecharge list.");

            Drone drone = DronesList.First(item => item.Id == droneId);
            int droneIndex = DronesList.FindIndex(item => item.Id == droneId);
            DroneCharge droneCharge = DroneChargeList.First(item => item.DroneId == droneId);    
            baseStationId = droneCharge.StationId;
            int baseStationIndex = BaseStationsList.FindIndex(item => item.Id == baseStationId);
            if (baseStationIndex  == -1)
                throw new Exception("baseStation's id doesn't exist in the BaseStation's list.");
            BaseStation baseStation = BaseStationsList.First(item => item.Id == baseStationId);
            DroneChargeList.Remove(droneCharge);
            baseStation.ChargeSlots++;
            BaseStationsList[baseStationIndex] = baseStation;
            DronesList[droneIndex] = drone;
        }
    }
}
