using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IBL.BO;
using IDal.DO;
using IDal;



namespace IBL
{
    public partial class BL : IBL
    {
        //----------------------------------Other GetObject Methods---------------------------------
        /// <summary>
        /// returns drone's battery
        /// </summary>
        /// <param name="droneId">drone's id</param>
        /// <returns>drone's battery</returns>
        private double GetDroneBattery(int droneId)
        {
            return dronesForList.Find(drone => drone.Id == droneId).Battery;
        }

        /// <summary>
        /// returns drone's status
        /// </summary>
        /// <param name="droneId">drone's id</param>
        /// <returns>drone's status</returns>
        private DroneStatuses GetDroneStatus(int droneId)
        {
            return dronesForList.Find(drone => drone.Id == droneId).Status;
        }

        /// <summary>
        /// returns drone's location
        /// </summary>
        /// <param name="droneId">drone's id</param>
        /// <returns>drone's location</returns>
        private Location GetDroneLocation(int droneId)
        {
            return dronesForList.Find(drone => drone.Id == droneId).Location;
        }

        private int GetDroneParcelId(int droneId)
        {
            if(((List<BO.Parcel>)GetBOParcelsList()).FindIndex(parcel => parcel.MyDrone != null && parcel.MyDrone.Id == droneId) == -1)
                return 0;
            BO.Parcel parcel = ((List<BO.Parcel>)GetBOParcelsList()).First(parcel=> parcel.MyDrone != null && parcel.MyDrone.Id == droneId );
            return parcel.Id;
        }

        /// <summary>
        /// return the number of supplied parcels which the customer have sent 
        /// </summary>
        /// <param name="customer"> BO customer</param>
        /// <returns>number of supplied parcels which the customer have sent </returns>
        private int SendAndSupplied(BO.Customer customer)
        {
            int num = 0;
            if (customer.FromCustomer == null) { return num;  }
            foreach (var item in customer.FromCustomer)
            {
                if(item.ParcelStatus == ParcelStatuses.Supplied) { ++num; }
            }
            return num;
        }


        /// <summary>
        /// return the number of unsupplied parcels which the customer have sent 
        /// </summary>
        /// <param name="customer"> BO customer</param>
        /// <returns>number of unsupplied parcels which the customer have sent </returns>
        private int SendAndNotSupplied(BO.Customer customer)
        {
            int num = 0;
            if (customer.FromCustomer == null) { return num; }
            foreach (var item in customer.FromCustomer)
            {
                if (item.ParcelStatus != ParcelStatuses.Supplied) { ++num; }
            }
            return num;
        }

        /// <summary>
        /// return the number of supplied parcels which the customer have got 
        /// </summary>
        /// <param name="customer"> BO customer</param>
        /// <returns>number of supplied parcels which the customer have got </returns>
        private int GetAndSupplied(BO.Customer customer)
        {
            int num = 0;
            if (customer.ToCustomer == null) { return num; }
            foreach (var item in customer.ToCustomer)
            {
                if (item.ParcelStatus == ParcelStatuses.Supplied) { ++num; }
            }
            return num;
        }

        /// <summary>
        /// return the number of unsupplied parcels which the customer have got 
        /// </summary>
        /// <param name="customer"> BO customer</param>
        /// <returns>number of unsupplied parcels which the customer have got </returns>
        private int GetAndNotSupplied(BO.Customer customer)
        {
            int num = 0;
            if (customer.ToCustomer == null) { return num; }
            foreach (var item in customer.ToCustomer)
            {
                if (item.ParcelStatus != ParcelStatuses.Supplied) { ++num; }
            }
            return num;
        }

        private BO.Parcel ParcelDOtOBO(IDal.DO.Parcel parcel)
        {
            BO.Parcel BOParcel = new()
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
        /// convert Coordinate object from BO to DO
        /// </summary>
        /// <param name="coor">BO coordinate</param>
        /// <returns>DO coordinate</returns>
        private IDal.DO.Coordinate CoordinateBoToDo(BO.Coordinate coor)
        {
            return new IDal.DO.Coordinate() { InputCoorValue = coor.InputCoorValue, Degrees = coor.Degrees, Direction = (IDal.DO.Directions)coor.Direction, MyLocation = (IDal.DO.Locations)coor.MyLocation , Minutes = coor.Minutes, Seconds = coor.Seconds};
        }

        /// <summary>
        /// convert Coordinate object from DO to BO
        /// </summary>
        /// <param name="coor">DO coordinate</param>
        /// <returns>BO coordinate</returns>
        private BO.Coordinate CoordinateDoToBo(IDal.DO.Coordinate coor)
        {
            return new BO.Coordinate() { InputCoorValue = coor.InputCoorValue, Degrees = coor.Degrees, Direction = (BO.Directions)coor.Direction, MyLocation = (BO.Locations)coor.MyLocation, Minutes = coor.Minutes, Seconds = coor.Seconds };
        }
    }
}
