using System;
using System.Collections.Generic;
using System.Text;
using static DalObject.DalObject;
using static DalObject.DataSource.Config;
using static DalObject.DataSource;

namespace DalObject
{
    public partial class DalObject
    {
        public  void DisplayBaseStation(int id)
        {
            while (searchBaseStation(id) == -1)
            {
                Console.WriteLine("Base station's Id does not exist, Please try again!");
                while(!int.TryParse(Console.ReadLine(),out id))
                {
                    Console.WriteLine("Id can contain only digits, Please try again!");
                }
            }
            int index = searchBaseStation(id);
            Console.WriteLine($"id: {BaseStationsArr[index].Id} \n" +
                              $"name: {BaseStationsArr[index].Name} \n" +
                              $"longitude: {BaseStationsArr[index].Longitude}\n" +
                              $"latitude:  {BaseStationsArr[index].Latitude}\n" +
                              $"number of charge slots: {BaseStationsArr[index].ChargeSlots}\n");
        }

        
        public void DisplayDrone(int id)
        {
            while (searchDrone(id) == -1)
            {
                Console.WriteLine("Drone's Id does not exist, Please try again!");
                while (!int.TryParse(Console.ReadLine(), out id))
                {
                    Console.WriteLine("Id can contain only digits, Please try again!");
                }
            }
            int index = searchBaseStation(id);
            Console.WriteLine($"id: {DronesArr[index].Id} \n" +
                              $"model: {DronesArr[index].Model} \n" +
                              $"status: {DronesArr[index].Status}\n" +
                              $"maxWeight:  {DronesArr[index].MaxWeight}\n" +
                              $"battery: {DronesArr[index].Battery}\n");
        }

        public void DisplayCustomer(string id)
        {
            while (searchCustomer(id) == -1)
            {
                Console.WriteLine("Customer's Id does not exist, Please try again!");
                id = Console.ReadLine();
            }
            int index = searchCustomer(id);
            Console.WriteLine($"id: {CustomersArr[index].Id} \n" +
                              $"name: {CustomersArr[index].Name} \n" +
                              $"phone: {CustomersArr[index].Phone}\n"+
                              $"longitude: {CustomersArr[index].Longitude}\n" +
                              $"latitude:  {CustomersArr[index].Latitude}\n");
        }


        public void DisplayParcel(int id)
        {
            while (searchParcel(id) == -1)
            {
                Console.WriteLine("Parcel's Id does not exist, Please try again!");
                while (!int.TryParse(Console.ReadLine(), out id))
                {
                    Console.WriteLine("Id can contain only digits, Please try again!");
                }
            }
            int index = searchParcel(id);
            Console.WriteLine($"id: {ParcelsArr[index].Id} \n" +
                              $"senderId: {ParcelsArr[index].SenderId} \n" +
                              $"targetId: {ParcelsArr[index].TargetId}\n" +
                              $"droneId:  {ParcelsArr[index].DroneId}\n" +
                              $"weight:  {ParcelsArr[index].Weight}\n" +
                              $"priority:  {ParcelsArr[index].Priority}\n" +
                              $"production:  {ParcelsArr[index].Production}\n" +
                              $"association:  {ParcelsArr[index].Association}\n" +
                              $"pickingUp:  {ParcelsArr[index].PickingUp}\n" +
                              $"arrival: {ParcelsArr[index].Arrival}\n");
        }


        private int searchBaseStation(int id)
        {
            int index = -1;
            for (int i = 0; i < IndexOfBaseStation; ++i)
            {
                if (BaseStationsArr[i].Id == id)
                {
                    index = i;
                }
            }
            return index;
        }

        private  int searchDrone(int id)
        {
            int index = -1;
            for (int i = 0; i <IndexOfDrone; ++i)
            {
                if (DronesArr[i].Id == id)
                {
                    index = i;
                }
            }
            return index;
        }

        private  int searchCustomer(string id)
        {
            int index = -1;
            for (int i = 0; i < IndexOfCustomer; ++i)
            {
                if (CustomersArr[i].Id == id)
                {
                    index = i;
                }
            }
            return index;
        }

        private  int searchParcel(int id)
        {
            int index = -1;
            for (int i = 0; i <IndexOfParcel; ++i)
            {
                if (ParcelsArr[i].Id == id)
                {
                    index = i;
                }
            }
            return index;
        }

        

    }
}
