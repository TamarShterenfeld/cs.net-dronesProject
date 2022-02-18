using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO;
using System.Runtime.CompilerServices;

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
        int SendAndSupplied(BO.Customer customer)
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
        int SendAndNotSupplied(BO.Customer customer)
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
        int GetAndSupplied(BO.Customer customer)
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
        int GetAndNotSupplied(BO.Customer customer)
        {
            int num = 0;
            if (customer.ToCustomer == null) { return num; }
            foreach (var item in customer.ToCustomer)
            {
                if (item.ParcelStatus != ParcelStatuses.Supplied) { ++num; }
            }
            return num;
        }

        ParcelStatuses ParcelStatus(DO.Parcel parcel)
        {
            if (parcel.AssociationDate == null)
                return ParcelStatuses.Production;
            if (parcel.PickUpDate == null)
                return ParcelStatuses.Associated;
            if (parcel.SupplyDate == null)
                return ParcelStatuses.PickedUp;
            else
                return ParcelStatuses.Supplied;
        }
    }
}
