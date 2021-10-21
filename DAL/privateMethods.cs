using System;
using System.Collections.Generic;
using System.Text;
using static DalObject.DataSource;
using static DalObject.DataSource.Config;
using static IDAL.DO.IDAL;
using  IDAL.DO;

namespace DalObject
{
    public partial class  DalObject
    {
        /// <summary>
        /// getting a string and check its existance in WeightCategories enum.
        /// </summary>
        private static void inputWeightCategory(ref string maxWeight)
        {
            bool isExist1 = false;
            string currentEnum;
            //checking if the inputed category (string) exists in WeightCategories enum
            while (isExist1 == false)
            {
                maxWeight = Console.ReadLine();
                for (int i = 0; i < Enum.GetNames(typeof(WeightCategories)).Length; i++)
                {
                    currentEnum = (string)Enum.GetNames(typeof(WeightCategories)).GetValue(i);
                    if (currentEnum == maxWeight || currentEnum.ToLower() == maxWeight)
                    {
                        //category is assigned to hold the numeric value of the enum type.
                        maxWeight = i.ToString();
                        isExist1 = true;
                        break;
                    }
                }
                if (isExist1 == false)
                {
                    Console.WriteLine("The entered weight category doesn't exist\nPlease enter another weight category");
                }
            }
        }

        /// <summary>
        /// getting a string and check its existance in DronesStatuses enum.
        /// </summary>
        private static void inputDroneStatus(ref string status)
        {
            bool isExist2 = false;
            string currentEnum;
            while (isExist2 == false)
            {
                status = Console.ReadLine();
                for (int i = 0; i < Enum.GetNames(typeof(DroneStatuses)).Length; i++)
                {
                    currentEnum = (string)Enum.GetNames(typeof(DroneStatuses)).GetValue(i);
                    if (currentEnum == status || currentEnum.ToLower() == status)
                    {
                        //status is assigned to hold the numeic value of the enum type.
                        status = i.ToString();
                        isExist2 = true;
                        break;
                    }
                }
                if (isExist2 == false)
                {
                    Console.WriteLine("The entered status doesn't exist\nPlease enter another status");
                }
            }
        }
        /// <summary>
        /// getting a string and check its existance in Priorities enum.
        /// </summary>
        private static void inputPriority(ref string priority)
        {
            bool isExist3 = false;
            string currentEnum;
            while (isExist3 == false)
            {
                priority = Console.ReadLine();
                for (int i = 0; i < Enum.GetNames(typeof(Priorities)).Length; i++)
                {
                    currentEnum = (string)Enum.GetNames(typeof(Priorities)).GetValue(i);
                    if (currentEnum == priority || currentEnum.ToLower() == priority)
                    {
                        //priority is assigned to hold the numeic value of the enum type.
                        priority = i.ToString();
                        isExist3 = true;
                        break;
                    }
                }
                if (isExist3 == false)
                {
                    Console.WriteLine("The entered status doesn't exist\nPlease enter another status");
                }
            }
        }

        private static bool chackingIdentitiesOfParcel(int id, string senderId, string targetId, int droneId)
        {
            if (searchParcel(id) == -1)
            {
                Console.WriteLine("Can not add parcel, this parcel ID already exists ");
                return false;
            }
            if (searchCustomer(senderId) == -1 || searchCustomer(targetId) == -1)
            {
                Console.WriteLine("Can not add parcel, sender ID or target ID does not exist ");
                return false;
            }
            if (searchDrone(droneId) == -1)
            {
                Console.WriteLine("Can not add parcel, drone ID does not exist ");
                return false;
            }
            return true;
        }
        private static void inputIntValue(ref int id)
        {
            while (!int.TryParse(Console.ReadLine(), out id))
            {
                Console.WriteLine("Id can contain only digits, Please try again!");
            }
        }
        private static int searchDronesChargeList(int droneId)
        {
            DroneCharge item;
            for (int i = 0; i < DroneChargeList.Count; i++)
            {
                item = DroneChargeList[i];
                if (item.DroneId == droneId)
                    return i;
            }
            return -1;
        }
        private static int searchBaseStation(int id)
        {
            int index = -1;
            for (int i = 0; i < IndexOfBaseStation; ++i)
            {
                if (BaseStationsList[i].Id == id)
                {
                    index = i;
                }
            }
            return index;
        }
        private static int searchDrone(int id)
        {
            int index = -1;
            for (int i = 0; i < IndexOfDrone; ++i)
            {
                if (DronesList[i].Id == id)
                {
                    index = i;
                }
            }
            return index;
        }
        private static int searchCustomer(string id)
        {
            int index = -1;
            for (int i = 0; i < IndexOfCustomer; ++i)
            {
                if (CustomersList[i].Id == id)
                {
                    index = i;
                }
            }
            return index;
        }
        private static int searchParcel(int id)
        {
            int index = -1;
            for (int i = 0; i < IndexOfParcel; ++i)
            {
                if (ParcelsList[i].Id == id)
                {
                    index = i;
                }
            }
            return index;
        }
    }
}
