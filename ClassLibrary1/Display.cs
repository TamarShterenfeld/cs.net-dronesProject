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
            //IDAL.DO.BaseStation baseStation1 = dal.GetBaseStation(id);
            //BO.BaseStation baseStation = new BO.BaseStation { Id = baseStation1.Id };
        }

        /// <summary>
        /// The function displays a drone according to the input id.
        /// </summary>
        /// <param name="id">drone's id</param>
        public void GetDrone(int id)
        {
            List<BO.Drone> donesList = (List<BO.Drone>)dal.GetDronesList();
            if (donesList.FindIndex(item => item.Id == id) == -1)
            {
                throw new BO.OverloadException("the inserted id wasn't found");
            }
            BO.Drone currDrone = donesList.First(item => item.Id == id);
            Console.WriteLine(currDrone);
        }

        /// <summary>
        /// The function displays a customer according to the input id.
        /// </summary>
        /// <param name="id">customer's id</param>
        public void DisplayCustomer(string id)
        {
            List<BO.Customer> customersList = (List<BO.Customer.>)dal.GetCustomersList();
            if (customersList.FindIndex(item => item.Id == id) == -1)
            {
                throw new BO.OverloadException("the inserted id wasn't found");
            }
            BO.Customer currCustomer = customersList.First(item => item.Id == id);
            Console.WriteLine(currCustomer);
        }

        /// <summary>
        /// The function displays a parcel according to the input id.
        /// </summary>
        /// <param name="id">parcel's id</param>
        public void DisplayParcel(int id)
        {
            List<BO.Parcel> parcelsList = (List<BO.Parcel>)dal.GetParcelsList();
            if (parcelsList.FindIndex(item => item.Id == id) == -1)
            {
                throw new BO.OverloadException("the inserted id wasn't found");
            }
            BO.Parcel currParcel = parcelsList.First(item => item.Id == id);
            Console.WriteLine(currParcel);
        }

        /// <summary>
        /// The function displays a parcel according to the input id.
        /// </summary>
        /// <param name="id">parcel's id</param>
        public void DisplayDrone(int id)
        {
            List<BO.Drone> dronesList = (List<BO.Drone>)dal.GetDronesList();
            if (dronesList.FindIndex(item => item.Id == id) == -1)
            {
                throw new BO.OverloadException("the inserted id wasn't found");
            }
            BO.Drone currDrone = dronesList.First(item => item.Id == id);
            Console.WriteLine(currDrone);
        }

        public int caughtChargeSlots(int stationId)
        {
            int caught = dal.AvailableChargingSlots(stationId);
            return caught;
        }

        public IEnumerable<DroneInCharging> GetDronesInMe(int stationId)
        {
            List<DroneInCharging> droneInCharging = null;
            foreach(DroneCharge droneCharge in dal.DronesChargingInMe(stationId))
            {
                DroneInCharging drone = new DroneInCharging() { Id = droneCharge.DroneId, Battery = 100 };
                // לבדוק שבאמת הבטריה נהית 100
                droneInCharging.Add(drone);
            }
            return droneInCharging;
        }
    }
}

