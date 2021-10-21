using System;
using System.Collections.Generic;
using System.Text;
using static IDAL.DO.IDAL;
using IDAL.DO;
using static DalObject.DataSource;

namespace DalObject
{
    public partial class DalObject
    {
        public static void AssociatingParcel(int parcelId, int droneId)
        {
            inputIntValue(ref parcelId);
            while (searchParcel(parcelId) == -1)
            {
                Console.WriteLine("ParcelId wasn't found! Please enter another ParcelId!");
                inputIntValue(ref parcelId);
            }
            inputIntValue(ref droneId);
            while (searchDrone(droneId) == -1)
            {
                Console.WriteLine("DroneId wasn't found! Please enter another DroneId!");
                inputIntValue(ref droneId);
            }
            int parcelIndex = searchParcel(parcelId);
            ParcelsArr[parcelIndex].Association = DateTime.Now;
            ParcelsArr[parcelIndex].DroneId = droneId;
            //updating the associated drone's status to be shipment.
            int droneIndex = searchDrone(droneId);
            DronesArr[droneIndex].Status = DroneStatuses.Shipment;
        }

        public static void PickingUpParcel(int parcelId, string senderId)
        {
            inputIntValue(ref parcelId);
            while (searchParcel(parcelId) == -1)
            {
                Console.WriteLine("ParcelId wasn't found! Please enter another ParcelId!");
                inputIntValue(ref parcelId);
            }

            senderId = Console.ReadLine();
            while (searchCustomer(senderId) == -1)
            {
                Console.WriteLine("senderId wasn't found! Please enter another senderId!");
                senderId = Console.ReadLine();
            }
            int parcelIndex = searchParcel(parcelId);
            ParcelsArr[parcelIndex].SenderId = senderId;
            ParcelsArr[parcelIndex].PickingUp = DateTime.Now;
        }

        public static void SupplyingParcel(int parcelId, string targetId)
        {
            inputIntValue(ref parcelId);
            while (searchParcel(parcelId) == -1)
            {
                Console.WriteLine("ParcelId wasn't found! Please enter another ParcelId!");
                inputIntValue(ref parcelId );
            }
            targetId = Console.ReadLine();
            while (searchCustomer(targetId) == -1)
            {
                Console.WriteLine("senderId wasn't found! Please enter another senderId!");
                targetId = Console.ReadLine();
            }
            int parcelIndex = searchParcel(parcelId);
            ParcelsArr[parcelIndex].Arrival = DateTime.Now;
        }

        public static void ChargingDrone(int droneId, int baseStationId)
        {
            inputIntValue(ref droneId);
            while (searchParcel(droneId) == -1)
            {
                Console.WriteLine("ParcelId wasn't found! Please enter another ParcelId!");
                inputIntValue(ref droneId);
            }

            inputIntValue(ref baseStationId);
            while (searchParcel(baseStationId) == -1)
            {
                Console.WriteLine("BaseStationId wasn't found! Please enter another baseStationId!");
                inputIntValue(ref baseStationId);
            }

            while(BaseStationsArr[baseStationId].ChargeSlots == 0)
            {
                Console.WriteLine("The chosen base station isn't available to charge the drone.");
                inputIntValue(ref baseStationId);
            }

            DroneCharge droneCharge = new DroneCharge(baseStationId, droneId);
            DroneChargeList.Add(droneCharge);
            int droneIndex = searchDrone(droneId);
            DronesArr[droneIndex].Status = DroneStatuses.Maintenance;
        }

        public static void StopDroneCharging(int droneId)
        {
            int index,  droneIndex, baseStationId, baseStationIndex;
            inputIntValue(ref droneId);
            while (searchDronesChargeList(droneId)==-1)
            {
                Console.WriteLine("droneId don't exist in DroneCharge List, Try again!");
                inputIntValue(ref droneId);
            }
            index = searchDronesChargeList(droneId);
            droneIndex = searchDrone(droneId);
            baseStationId = DroneChargeList[droneIndex].StationId;
            baseStationIndex = searchBaseStation(baseStationId);
            DroneChargeList.RemoveAt(index);
            BaseStationsArr[baseStationId].ChargeSlots--;
            DronesArr[droneId].Status = DroneStatuses.Available;
        }

        private int searchDroneCharge(int droneId, int baseStationId)
        {
            DroneCharge item;
            for (int i = 0; i<DroneChargeList.Count;i++)
            {
                item = DroneChargeList[i];
                if (item.DroneId == droneId && item.StationId == baseStationId)
                    return i;
            }
            return -1;
        }
    }
}
