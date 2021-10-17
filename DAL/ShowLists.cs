using System;
using System.Collections.Generic;
using System.Text;
using static IDAL.DO.IDAL;
using static DalObject.DataSource;


namespace DalObject
{
    public  partial class DalObject
    {
        public  void ShowingBaseStationsList()
        {
            List<BaseStation> baseStationsList = new List<BaseStation>(GettingBaseStationList());
            foreach (BaseStation item in baseStationsList)
            {
                Console.WriteLine($"id: {item.Id} \n" +
                              $"name: {item.Name} \n" +
                              $"longitude: {item.Longitude}\n" +
                              $"latitude:  {item.Latitude}\n" +
                              $"number of charge slots: {item.ChargeSlots}\n");
            }
        }

        public  void ShowingBDronesList()
        {
            List<Drone> dronesList = new List<Drone>(GettingDronesList());
            foreach (Drone item in DronesArr)
            {
                Console.WriteLine($"id: {item.Id} \n" +
                                $"model: {item.Model} \n" +
                                $"weight: {item.MaxWeight}\n" +
                                $"battery:  {item.Battery}\n" +
                                $"status: {item.Status}\n");
            }
        }

        public  void ShowingCustomersList()
        {
            List<Customer> customersList = new List<Customer>(GettingCustomerList());
            foreach (Customer item in CustomersArr)
            {
                Console.WriteLine($"id: {item.Id} \n" +
                                $"name: {item.Name} \n" +
                                $"phone number: {item.Phone}\n" +
                                $"longitude:  {item.Longitude}\n" +
                                $"latitude: {item.Latitude}\n");
            }
        }

        public  void ShowingParcelsList()
        {
            List<Parcel> parcelsList = new List<Parcel>(GettingParcelList());
            foreach (Parcel item in ParcelsArr)
            {
                Console.WriteLine($"id: {item.Id} \n" +
                                $"sender's id: {item.SenderId} \n" +
                                $"target's id: {item.TargetId} \n" +
                                $"weight: {item.Weight} \n" +
                                $"priority:{item.Priority}\n" +
                                $"drone's id:{item.DroneId}\n" +
                                $"production date:{item.Production}\n" +
                                $"association date:{item.Association}\n" +
                                $"picking up date:{item.PickingUp}\n" +
                                $"arrival date:{item.Arrival}\n"); 
            }
        }

        public  void ShowingNotAssociatedParcelsList()
        {
            List<Parcel> notAssociatedParcelsList = new List<Parcel>(GettingNotAssociatedParcels());
            foreach (Parcel item in notAssociatedParcelsList)
            {
                Console.WriteLine($"id: {item.Id} \n" +
                                $"sender's id: {item.SenderId} \n" +
                                $"target's id: {item.TargetId} \n" +
                                $"weight: {item.Weight} \n" +
                                $"priority:{item.Priority}\n" +
                                $"drone's id:{item.DroneId}\n" +
                                $"production date:{item.Production}\n" +
                                $"association date:{item.Association}\n" +
                                $"picking up date:{item.PickingUp}\n" +
                                $"arrival date:{item.Arrival}\n");
            }
        }

        public void AvailableChargeSlots()
        {
            foreach (BaseStation item in GettingAvailableChageSlots())
            {
                Console.WriteLine($"id: {item.Id} \n" +
                              $"name: {item.Name} \n" +
                              $"longitude: {item.Longitude}\n" +
                              $"latitude:  {item.Latitude}\n" +
                              $"number of charge slots: {item.ChargeSlots}\n");
            }
        }

    }


}


