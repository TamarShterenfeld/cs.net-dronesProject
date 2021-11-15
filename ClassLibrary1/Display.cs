﻿using System;
using System.Collections.Generic;
using System.Text;
using static DalObject.DataSource;
using IBL.BO;
using static IDAL.DO.OverloadException;
using System.Linq;
using System.Numerics;
using IDAL.DO;
using static IDAL.DO.BaseStation;
using static IDAL.DO.Customer;
using static IDAL.DO.DroneCharge;
using static IDAL.DO.Drone;
using static IDAL.DO.Locations;
using static IDAL.DO.Parcel;


namespace IBL
{
    public partial class BL : IBL
    {
        //we didn't use the override function - ToString(),
        //for it doesn't include enough details for printing.

        /// <summary>
        /// The function displays a base station according to the input id.
        /// </summary>
        /// <param name="id">base station's id</param>
        public void DisplayBaseStation(int id)
        {
            if (BaseStationsList.FindIndex(item => item.Id == id) == -1)
            {
                throw new OverloadException("the inserted id wasn't found");
            }
            IBL.BO.BaseStation currBaseStation = BaseStationsList.First(item => item.Id == id);
            Console.WriteLine(currBaseStation);
        }

        /// <summary>
        /// The function displays a drone according to the input id.
        /// </summary>
        /// <param name="id">drone's id</param>
        public void GetDrone(int id)
        {
            if (DronesList.FindIndex(item => item.Id == id) == -1)
            {
                throw new OverloadException("the inserted id wasn't found");
            }
            Drone currDrone = DronesList.First(item => item.Id == id);
            Console.WriteLine(currDrone);
        }

        /// <summary>
        /// The function displays a customer according to the input id.
        /// </summary>
        /// <param name="id">customer's id</param>
        public void DisplayCustomer(string id)
        {
            if (CustomersList.FindIndex(item => item.Id == id) == -1)
            {
                throw new OverloadException("the inserted id wasn't found");
            }
            IDAL.DO.Customer currCustomer = CustomersList.First(item => item.Id == id);
            Console.WriteLine(currCustomer);
        }

        /// <summary>
        /// The function displays a parcel according to the input id.
        /// </summary>
        /// <param name="id">parcel's id</param>
        public void DisplayParcel(int id)
        {
            if (ParcelsList.FindIndex(item => item.Id == id) == -1)
            {
                throw new OverloadException("the inserted id wasn't found");
            }
            Parcel currParcel = ParcelsList.First(item => item.Id == id);
            Console.WriteLine(currParcel);
        }
    }
}

