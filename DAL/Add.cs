using System;
using System.Collections.Generic;
using System.Text;
using IDAL.DO;
using static IDAL.IDal;
using static DalObject.DataSource;

namespace DalObject
{
    /// <summary>
    ///the class DalObject contains all the needed methods 
    ///which are connected to the data (in DataSource class) of the program.
    /// </summary>
    public partial class DalObject : IDAL.IDal
    {
        void Add(IDAL.DO.BaseStation baseStation)
        {
            //can add the base station just if the input id still doesn't exist in the Base Station's list.
            if (BaseStationsList.FindIndex(item => item.Id == baseStation.Id) == -1)
            {
                BaseStationsList.Add(baseStation);
            }
            else
            {
                throw new OverloadException("Id already exists in base station list, it's not possible to add it!");//לטפל בהערות
            }
        }
        void Add(IDAL.DO.Customer customer)
        {
            //can add the customer just if the input id stil doesn't exist in the customers' list.
            if (CustomersList.FindIndex(item => item.Id == customer.Id) == -1)
            {
                CustomersList.Add(customer);
            }
            else
            {
                throw new OverloadException("Id already exists in drones list, it's not possible to add it!");//לטפל בהערות
            }
        }
        void Add(IDAL.DO.Drone drone)
        {
            //can add the drone just if the input id stil doesnt exist in the Drones' list.
            if (DronesList.FindIndex(item => item.Id == drone.Id) == -1)
            {
                DronesList.Add(drone);
            }
            else
            {
                throw new OverloadException("Id already exists in drones list, it's not possible to add it!");//לטפל בהערות
            }
        }
        void Add(IDAL.DO.Parcel parcel)
        {

        }


    }
}



///// <summary>
///// The function adds a parcel
///// </summary>
///// <param name="id">parcel's id</param>
///// <param name="senderId">parcel's sender id</param>
///// <param name="targetId">parcel's target id</param>
///// <param name="droneId">parcel's drone id</param>
///// <param name="weight">parcel's weight</param>
///// <param name="priority">parcel's priority </param>
//public void AddParcel(int id, string senderId, string targetId, int droneId, string weight, string priority)
//{

//    if (ParcelsList.Count == ParcelsList.Capacity)
//    {
//        Console.WriteLine("The amount of base station objects arrived to its maximum limit");
//        return;
//    }
//    chackingIdentitiesOfParcel(id, senderId, targetId, droneId);
//    WeightCategories weightCategories = (WeightCategories)int.Parse(weight);
//    Priorities priorities = (Priorities)int.Parse(priority);
//    Parcel parcel = new Parcel(id, senderId, targetId, weightCategories, priorities, droneId);
//    IncreaseParcelIndex();
//    ParcelsList.Add(parcel);