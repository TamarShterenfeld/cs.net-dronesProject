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
        public  void AssociatingParcel(int parcelId, int droneId)
        {
            while (int.TryParse(Console.ReadLine(), out parcelId))
            {
                Console.WriteLine("Id can contain only digits");
            }
            while (searchParcel(parcelId) == -1)
            {
                Console.WriteLine("ParcelId wasn't found! Please enter another ParcelId!");
                while (int.TryParse(Console.ReadLine(), out parcelId))
                {
                    Console.WriteLine("Id can contain only digits");
                }
            }
            while (int.TryParse(Console.ReadLine(), out droneId))
            {
                Console.WriteLine("Id can contain only digits");
            }
            while (searchDrone(droneId) == -1)
            {
                Console.WriteLine("DroneId wasn't found! Please enter another DroneId!");
                while (int.TryParse(Console.ReadLine(), out droneId))
                {
                    Console.WriteLine("Id can contain only digits");
                }
            }
            ParcelsArr[parcelId].Association = DateTime.Now; // האינדקס נראה לא בסדר
            ParcelsArr[parcelId].DroneId = droneId;
            //updating the associated drone's status to be shipment.
            DronesArr[droneId].Status = DroneStatuses.Shipment;
        }

        public void PickingUpParcel(int parcelId, string senderId)
        {
            while (int.TryParse(Console.ReadLine(), out parcelId))
            {
                Console.WriteLine("Id can contain only digits");
            }
            while (searchParcel(parcelId) == -1)
            {
                Console.WriteLine("ParcelId wasn't found! Please enter another ParcelId!");
                while (int.TryParse(Console.ReadLine(), out parcelId))
                {
                    Console.WriteLine("Id can contain only digits");
                }
            }

            senderId = Console.ReadLine();
            while (searchCustomer(senderId) == -1)
            {
                Console.WriteLine("senderId wasn't found! Please enter another senderId!");
                senderId = Console.ReadLine();
            }
            ParcelsArr[parcelId].SenderId = senderId;
            ParcelsArr[parcelId].PickingUp = DateTime.Now;
        }

        public void SupplyingParcel(int parcelId, string targetId)
        {
            while (int.TryParse(Console.ReadLine(), out parcelId))
            {
                Console.WriteLine("Id can contain only digits");
            }
            while (searchParcel(parcelId) == -1)
            {
                Console.WriteLine("ParcelId wasn't found! Please enter another ParcelId!");
                while (int.TryParse(Console.ReadLine(), out parcelId))
                {
                    Console.WriteLine("Id can contain only digits");
                }
            }
            targetId = Console.ReadLine();
            while (searchCustomer(targetId) == -1)
            {
                Console.WriteLine("senderId wasn't found! Please enter another senderId!");
                targetId = Console.ReadLine();
            }
            ParcelsArr[parcelId].Arrival = DateTime.Now;
        }

        public void ChargingDrone(int droneId, int baseStationId)
        {
            while (int.TryParse(Console.ReadLine(), out droneId))
            {
                Console.WriteLine("Id can contain only digits");
            }

            while (searchParcel(droneId) == -1)
            {
                Console.WriteLine("ParcelId wasn't found! Please enter another ParcelId!");
                while (int.TryParse(Console.ReadLine(), out droneId))
                {
                    Console.WriteLine("Id can contain only digits");
                }
            }

            while (int.TryParse(Console.ReadLine(), out baseStationId))
            {
                Console.WriteLine("Id can contain only digits");
            }

            while (searchParcel(baseStationId) == -1)
            {
                Console.WriteLine("BaseStationId wasn't found! Please enter another baseStationId!");
                while (int.TryParse(Console.ReadLine(), out baseStationId))
                {
                    Console.WriteLine("Id can contain only digits");
                }
            }

            while(BaseStationsArr[baseStationId].ChargeSlots == 0)
            {
                Console.WriteLine("The chosen base station isn't available to charge the drone.");
                while (int.TryParse(Console.ReadLine(), out baseStationId))
                {
                    Console.WriteLine("Id can contain only digits");
                }
            }

            DroneCharge droneCharge = new DroneCharge();
            droneCharge.DroneId = droneId;
            droneCharge.StationId = baseStationId;
            DroneChargeList.Add(droneCharge);
            DronesArr[droneId].Status = DroneStatuses.Maintenance;

        }

        public void StopDroneCharging(int droneId, int baseStationId)// איך הפונקציה עובדת
        {
            int index;
            while (int.TryParse(Console.ReadLine(), out droneId))
            {
                Console.WriteLine("Id can contain only digits");
            }
            while (int.TryParse(Console.ReadLine(), out baseStationId))
            {
                Console.WriteLine("Id can contain only digits");
            }
            while(searchDroneCharge(droneId, baseStationId)==-1)
            {
                Console.WriteLine("droneId & baseStationId don't exist in DroneCharge List, Try again!");
                while (int.TryParse(Console.ReadLine(), out droneId))
                {
                    Console.WriteLine("Id can contain only digits");
                }
                while (int.TryParse(Console.ReadLine(), out baseStationId))
                {
                    Console.WriteLine("Id can contain only digits");
                }
            }
            index = searchDroneCharge(droneId, baseStationId);
            DroneChargeList.RemoveAt(index);
            BaseStationsArr[baseStationId].ChargeSlots--;
            DronesArr[droneId].Status = DroneStatuses.Available;
        }

        private int searchDroneCharge(int droneId, int baseStationId)
        {
            DroneCharge item;// למה צריך ליצור אוביקט
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
