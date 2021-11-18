using System;
using System.Collections.Generic;
using System.Text;
using static DalObject.DataSource;
using IBL.BO;
using System.Linq;
using System.Numerics;
using IDAL.DO;
using IDAL;
using static DalObject.DalObject;


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
        public BO.BaseStation GetBLBaseStation(int id)
        {
            BO.BaseStation baseStation = new dal.GetBaseStation(int id);
        }

        /// <summary>
        /// The function displays a drone according to the input id.
        /// </summary>
        /// <param name="id">drone's id</param>
        public void GetDrone(int id)
        {
            List<Drone> donesList = (List<Drone>)dal.GetDronesList();
            if (donesList.FindIndex(item => item.Id == id) == -1)
            {
                throw new OverloadException("the inserted id wasn't found");
            }
            Drone currDrone = donesList.First(item => item.Id == id);
            Console.WriteLine(currDrone);
        }

        /// <summary>
        /// The function displays a customer according to the input id.
        /// </summary>
        /// <param name="id">customer's id</param>
        public void DisplayCustomer(string id)
        {
            List<Customer> customersList = (List<Customer>)dal.GetCustomersList();
            if (customersList.FindIndex(item => item.Id == id) == -1)
            {
                throw new OverloadException("the inserted id wasn't found");
            }
            Customer currCustomer = customersList.First(item => item.Id == id);
            Console.WriteLine(currCustomer);
        }

        /// <summary>
        /// The function displays a parcel according to the input id.
        /// </summary>
        /// <param name="id">parcel's id</param>
        public void DisplayParcel(int id)
        {
            List<Parcel> parcelsList = (List<Parcel>)dal.GetParcelsList();
            if (parcelsList.FindIndex(item => item.Id == id) == -1)
            {
                throw new OverloadException("the inserted id wasn't found");
            }
            Parcel currParcel = parcelsList.First(item => item.Id == id);
            Console.WriteLine(currParcel);
        }

        /// <summary>
        /// The function displays a parcel according to the input id.
        /// </summary>
        /// <param name="id">parcel's id</param>
        public void DisplayDrone(int id)
        {
            List<Drone> dronesList = (List<Drone>)dal.GetDronesList();
            if (dronesList.FindIndex(item => item.Id == id) == -1)
            {
                throw new OverloadException("the inserted id wasn't found");
            }
            Drone currDrone = dronesList.First(item => item.Id == id);
            Console.WriteLine(currDrone);
        }

        public int caughtChargeSlots(int stationId)
        {
            int caught = dal.AvailableChargingSlots(stationId);
            return caught;
        }

        public IEnumerable<DroneInCharging> GetDronesInMe(int stationId)
        {
            dal.DronesChargingInMe(stationId);
        }
    }
}

