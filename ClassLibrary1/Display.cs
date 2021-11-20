using System;
using System.Collections.Generic;
using System.Text;
using static DalObject.DataSource;
using IBL.BO;
using System.Linq;
using System.Numerics;
using IDal.DO;
using IDal;
using static DalObject.DalObject;



namespace IBL
{
    public partial class BL : IBL
    {
        //we didn't use the override function - ToString(),
        //for it doesn't include enough details for printing.

        /// <summary>
        /// The function displays a base station according to id.
        /// </summary>
        /// <param name="id">base station's id</param>
        public BO.BaseStation GetBLBaseStation(int id)
        {
            return BaseStationDOtOBO(dal.GetBaseStation(id)); ;
        }

        private BO.BaseStation BaseStationDOtOBO(IDal.DO.BaseStation baseStation)
        {
            BO.BaseStation BOBaseStation = new BO.BaseStation()
            {
                Id = baseStation.Id,
                Name = baseStation.Name,
                MyLocation = new Location(baseStation.Longitude, baseStation.Latitude),
                ChargeSlots = baseStation.ChargeSlots - AvailableChargingSlots(baseStation.Id),
                DroneCharging = (List<DroneInCharging>)GetDronesInMe(baseStation.Id)
            };
            return BOBaseStation;
        }


        /// <summary>
        /// The function displays a drone according to the id.
        /// </summary>
        /// <param name="id">drone's id</param>
        public BO.Drone GetBLDrone(int id)
        {
            return DroneDOtOBO(dal.GetDrone(id));
            
        }

        private BO.Drone DroneDOtOBO(IDal.DO.Drone drone)
        {
            BO.Drone BODrone = new BO.Drone()
            {
                Id = drone.Id,
                Model = drone.Model,
                MaxWeight = (BO.WeightCategories)drone.MaxWeight,
                //Battery = 0,
                //Status = 0,
                //Parcel = 
                MyLocation = new Location(drone.Longitude, drone.Latitude),
            };
            return BODrone;
        }

        /// <summary>
        /// The function displays a customer according to id.
        /// </summary>
        /// <param name="id">customer's id</param>
        public BO.Customer GetBLCustomer(string id)
        {
            return CustomerDOtOBO(dal.GetCustomer(id));

        }

        private BO.Customer CustomerDOtOBO(IDal.DO.Customer customer)
        {
            BO.Customer BOCustomer = new BO.Customer()
            {
                Id = customer.Id,
                Name = customer.Name,
                Phone = customer.Phone,
                MyLocation = new Location(customer.Longitude, customer.Latitude),
                //FromCustomer = null;
                //ToCustomer = null;
            };
            return BOCustomer;
        }

        /// <summary>
        /// The function displays a parcel according to id.
        /// </summary>
        /// <param name="id">parcel's id</param>
        public BO.Parcel GetBLParcel(int id)
        {
            return ParcelDOtOBO(dal.GetParcel(id));

        }

        private BO.Parcel ParcelDOtOBO(IDal.DO.Parcel parcel)
        {
            BO.Parcel BOParcel = new BO.Parcel()
            {
                Id = parcel.Id,
                Sender = GetBLCustomrInParcel(parcel.SenderId),
                Target = GetBLCustomrInParcel(parcel.TargetId),
                Weight = (BO.WeightCategories)parcel.Weight,
                Priority = (BO.Priorities)parcel.Priority,
                MyDrone = GetBLDroneInParcel(parcel.DroneId),
                ProductionDate = parcel.ProductionDate,
                AssociationDate = parcel.AssociationDate,
                PickUpDate = parcel.PickUpDate,
                SupplyDate = parcel.SupplyDate
            };
            return BOParcel;
        }

        /// <summary>
        /// The function displays a customer in parcel according to id.
        /// </summary>
        /// <param name="id">parcel's id</param>
        public CustomerInParcel GetBLCustomrInParcel(string id)
        {
            return CustomrInParcelDOtOBO(dal.GetCustomer(id));

        }

        private CustomerInParcel CustomrInParcelDOtOBO(IDal.DO.Customer customer)
        {
            CustomerInParcel BOCustomrInParcel = new CustomerInParcel()
            {
                Id = customer.Id,
                Name = customer.Name
            };
            return BOCustomrInParcel;
        }

        

        public DroneInParcel GetBLDroneInParcel(int id)
        {
            return DroneInParcelDOtOBO(dal.GetDrone(id));

        }

        private DroneInParcel DroneInParcelDOtOBO(IDal.DO.Drone drone)
        {
            DroneInParcel BOCustomerInParcel = new DroneInParcel()
            {
                Id = drone.Id,
                Battery = drone.Battery,
                CurrentLocation = new Location(drone.Longitude, drone.Latitude),
            };
            return BOCustomerInParcel;
        }

        ///// <summary>
        ///// The function displays a drone according to the input id.
        ///// </summary>
        ///// <param name="id">drone's id</param>
        //public void GetDrone(int id)
        //{
        //    List<BO.Drone> donesList = (List<BO.Drone>)dal.GetDronesList();
        //    if (donesList.FindIndex(item => item.Id == id) == -1)
        //    {
        //        throw new BO.OverloadException("the inserted id wasn't found");
        //    }
        //    BO.Drone currDrone = donesList.First(item => item.Id == id);
        //    Console.WriteLine(currDrone);
        //}

        ///// <summary>
        ///// The function displays a customer according to the input id.
        ///// </summary>
        ///// <param name="id">customer's id</param>
        //public void DisplayCustomer(string id)
        //{
        //    List<BO.Customer> customersList = (List<BO.Customer>)dal.GetCustomersList();
        //    if (customersList.FindIndex(item => item.Id == id) == -1)
        //    {
        //        throw new BO.OverloadException("the inserted id wasn't found");
        //    }
        //    BO.Customer currCustomer = customersList.First(item => item.Id == id);
        //    Console.WriteLine(currCustomer);
        //}

        /// <summary>
        /// The function displays a parcel according to the input id.
        /// </summary>
        /// <param name="id">parcel's id</param>
        //public void DisplayParcel(int id)
        //{
        //    List<BO.Parcel> parcelsList = (List<BO.Parcel>)dal.GetParcelsList();
        //    if (parcelsList.FindIndex(item => item.Id == id) == -1)
        //    {
        //        throw new BO.OverloadException("the inserted id wasn't found");
        //    }
        //    BO.Parcel currParcel = parcelsList.First(item => item.Id == id);
        //    Console.WriteLine(currParcel);
        //}

        ///// <summary>
        ///// The function displays a parcel according to the input id.
        ///// </summary>
        ///// <param name="id">parcel's id</param>
        //public void DisplayBaseStation (int id)
        //{
        //    List<BO.BaseStation> baseStationsList = (List<BO.BaseStation>)dal.GetParcelsList();
        //    if (baseStationsList.FindIndex(item => item.Id == id) == -1)
        //    {
        //        throw new BO.OverloadException("the inserted id wasn't found");
        //    }
        //    BO.BaseStation currBaseStation = baseStationsList.First(item => item.Id == id);
        //    Console.WriteLine(currBaseStation);
        //}

        /// <summary>
        /// The function displays a parcel according to the input id.
        /// </summary>
        /// <param name="id">parcel's id</param>
        //public void DisplayDrone(int id)
        //{
        //    List<BO.Drone> dronesList = (List<BO.Drone>)dal.GetDronesList();
        //    if (dronesList.FindIndex(item => item.Id == id) == -1)
        //    {
        //        throw new BO.OverloadException("the inserted id wasn't found");
        //    }
        //    BO.Drone currDrone = dronesList.First(item => item.Id == id);
        //    Console.WriteLine(currDrone);
        //}

        public int caughtChargeSlots(int stationId)
        {
            int caught = dal.AvailableChargeSlots(stationId);
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

