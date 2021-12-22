using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO;

namespace IBL
{
    public partial class BL 
    {
        //----------------------------------Other GetObject Methods---------------------------------
        /// <summary>
        /// returns drone's battery
        /// </summary>
        /// <param name="droneId">drone's id</param>
        /// <returns>drone's battery</returns>
        double GetDroneBattery(int droneId)
        {
            return dronesForList.Find(drone => drone.Id == droneId).Battery;
        }

        /// <summary>
        /// return the number of supplied parcels which the customer have sent 
        /// </summary>
        /// <param name="customer"> BO customer</param>
        /// <returns>number of supplied parcels which the customer have sent </returns>
        static int SendAndSupplied(BO.Customer customer)
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
        static int SendAndNotSupplied(BO.Customer customer)
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
        static int GetAndSupplied(BO.Customer customer)
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
        static int GetAndNotSupplied(BO.Customer customer)
        {
            int num = 0;
            if (customer.ToCustomer == null) { return num; }
            foreach (var item in customer.ToCustomer)
            {
                if (item.ParcelStatus != ParcelStatuses.Supplied) { ++num; }
            }
            return num;
        }

        /// <summary>
        /// the function converts a IDal.DO.Parcel object to a BO.Parcel object.
        /// </summary>
        /// <param name="parcel">the IDal.DO parcel object</param>
        /// <returns>a BO.Parcel object</returns>
         Parcel ParcelDOtOBO(DO.Parcel parcel)
        {
            Parcel BOParcel = new()
            {
                Id = parcel.Id,
                Sender = GetCustomrInParcel(parcel.SenderId),
                Target = GetCustomrInParcel(parcel.TargetId),
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
        static DO.Coordinate CoordinateBoToDo(BO.Coordinate coor)
        {
            return new DO.Coordinate() { InputCoorValue = coor.InputCoorValue, Degrees = coor.Degrees, Direction = (DO.Directions)coor.Direction, MyLocation = (DO.Locations)coor.MyLocation , Minutes = coor.Minutes, Seconds = coor.Seconds};
        }

        /// <summary>
        /// convert Coordinate object from DO to BO
        /// </summary>
        /// <param name="coor">DO coordinate</param>
        /// <returns>BO coordinate</returns>
         static BO.Coordinate CoordinateDoToBo(DO.Coordinate coor)
        {
            return new BO.Coordinate() { InputCoorValue = coor.InputCoorValue, Degrees = coor.Degrees, Direction = (BO.Directions)coor.Direction, MyLocation = (BO.Locations)coor.MyLocation, Minutes = coor.Minutes, Seconds = coor.Seconds };
        }
    }
}
