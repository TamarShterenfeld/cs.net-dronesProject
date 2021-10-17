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
            id = searchBaseStation(id);
            Console.WriteLine($"id: {BaseStationsArr[id].Id} \n" +
                              $"name: {BaseStationsArr[id].Name} \n" +
                              $"longitude: {BaseStationsArr[id].Longitude}\n" +
                              $"latitude:  {BaseStationsArr[IndexOfBaseStation].Latitude}\n" +
                              $"number of charge slots: {BaseStationsArr[IndexOfBaseStation].ChargeSlots}\n");
        }

        public  void DisplayDrone(int id)
        {
            while (searchDrone(id) == -1)
            {
                Console.WriteLine("Drone's Id does not exist, Please try again!");
                while (!int.TryParse(Console.ReadLine(), out id))
                {
                    Console.WriteLine("Id can contain only digits, Please try again!");
                }
            }
            id = searchBaseStation(id);
            Console.WriteLine($"id: {DronesArr[id].Id} \n" +
                              $"model: {DronesArr[id].Model} \n" +
                              $"status: {DronesArr[id].Status}\n" +
                              $"maxWeight:  {DronesArr[id].MaxWeight}\n" +
                              $"battery: {DronesArr[id].Battery}\n");
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
