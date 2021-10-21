using System;
using System.Collections.Generic;
using System.Text;
using static DalObject.DalObject;
using static DalObject.DataSource.Config;
using static DalObject.DataSource;
using static IDAL.DO.IDAL;
using System.Linq;

namespace DalObject
{
    public partial class DalObject
    {
        public static void DisplayBaseStation(int id)
        {
            BaseStation currBaseStation = BaseStationsList.First(item => item.Id == id);
            Console.WriteLine($"id: {currBaseStation.Id} \n" +
                              $"name: {currBaseStation.Name} \n" +
                              $"longitude: {currBaseStation.Longitude}\n" +
                              $"latitude:  {currBaseStation.Latitude}\n" +
                              $"number of charge slots: {currBaseStation.ChargeSlots}\n");
        }
        public static void DisplayDrone(int id)
        {
            Drone currDrone = DronesList.First(item => item.Id == id);
            Console.WriteLine($"id: {currDrone.Id} \n" +
                              $"model: {currDrone.Model} \n" +
                              $"status: {currDrone.Status}\n" +
                              $"maxWeight:  {currDrone.MaxWeight}\n" +
                              $"battery: {currDrone.Battery}\n");
        }
        public static void DisplayCustomer(string id)
        {
            Customer currCustomer = CustomersList.First(item => item.Id == id);
            Console.WriteLine($"id: {currCustomer.Id} \n" +
                              $"name: {currCustomer.Name} \n" +
                              $"phone: {currCustomer.Phone}\n"+
                              $"longitude: {currCustomer.Longitude}\n" +
                              $"latitude:  {currCustomer.Latitude}\n");
        }
        public static void DisplayParcel(int id)
        {
            Parcel currParcel = ParcelsList.First(item => item.Id == id);
            Console.WriteLine($"id: {currParcel.Id} \n" +
                              $"senderId: {currParcel.SenderId} \n" +
                              $"targetId: {currParcel.TargetId}\n" +
                              $"droneId:  {currParcel.DroneId}\n" +
                              $"weight:  {currParcel.Weight}\n" +
                              $"priority:  {currParcel.Priority}\n" +
                              $"production:  {currParcel.Production}\n" +
                              $"association:  {currParcel.Association}\n" +
                              $"pickingUp:  {currParcel.PickingUp}\n" +
                              $"arrival: {currParcel.Arrival}\n");
        }
    }
}
