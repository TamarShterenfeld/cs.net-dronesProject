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
        public static void AssociatingParcel(int parcelId, int droneId)
        {
            inputIntValue(ref parcelId);
            Parcel parcel = ParcelsList.First(item => item.Id == parcelId);
            inputIntValue(ref droneId);
            Drone drone = DronesList.First(item => item.Id == droneId);
            parcel.Association = DateTime.Now;
            parcel.DroneId = droneId;
            //updating the associated drone's status to be as shipment.
            drone.Status = DroneStatuses.Shipment;
        }

        public static void PickingUpParcel(int parcelId, string senderId)
        {
            inputIntValue(ref parcelId);
            Parcel parcel = ParcelsList.First(item => item.Id == parcelId);
            senderId = Console.ReadLine();
            CustomersList.First(item => item.Id == senderId);
            parcel.SenderId = senderId;
            parcel.PickingUp = DateTime.Now;
        }

        public static void SupplyingParcel(int parcelId, string targetId)
        {
            inputIntValue(ref parcelId);
            Parcel parcel = ParcelsList.First(item => item.Id == parcelId);
            targetId = Console.ReadLine();
            CustomersList.First(item => item.Id == targetId);
            parcel.Arrival = DateTime.Now;
        }

        public static void ChargingDrone(int droneId, int baseStationId)
        {
            inputIntValue(ref droneId);
            Drone  drone = DronesList.First(item => item.Id == droneId);
            inputIntValue(ref baseStationId);
            BaseStation baseStation = BaseStationsList.First(item => item.Id == baseStationId);
            while(baseStation.ChargeSlots == 0)
            {
                Console.WriteLine("The chosen base station isn't available to charge the drone.");
                inputIntValue(ref baseStationId);
            }
            while(drone.Status == DroneStatuses.Maintenance)
            {
                Console.WriteLine("The chosen drone is already being charging now");
            }
            DroneCharge droneCharge = new DroneCharge(baseStationId, droneId);
            DroneChargeList.Add(droneCharge);
            drone.Status = DroneStatuses.Maintenance;
        }
        public static void StopDroneCharging(int droneId)
        {
            int baseStationId;
            inputIntValue(ref droneId);
            DroneCharge droneCharge = DroneChargeList.First(item => item.DroneId == droneId);
            Drone drone = DronesList.First(item => item.Id == droneId);
            baseStationId = droneCharge.StationId;
            BaseStation baseStation = BaseStationsList.First(item => item.Id == baseStationId);
            DroneChargeList.Remove(droneCharge);
            baseStation.ChargeSlots++;
            drone.Status = DroneStatuses.Available;
        }
    }
}
