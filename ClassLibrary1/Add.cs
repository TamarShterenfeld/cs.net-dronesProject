using IDal.DO;
using System;
using System.Collections.Generic;
using System.Text;
using static DalObject.DataSource;
using System.Linq;
using  DalObject;
using IBL.BO;



namespace IBL
{
    public partial class BL : IBL
    {
        /// <summary>
        /// The function adds a base station 
        /// </summary>
        /// <param name="baseStation">base station</param>
        public void Add(BO.BaseStation baseStation)
        {
            dal.Add(new IDal.DO.BaseStation() { Id = baseStation.Id, Name = baseStation.Name, Longitude = baseStation.MyLocation.CoorLongitude , Latitude = baseStation.MyLocation.CoorLatitude, ChargeSlots = baseStation.ChargeSlots}); 
        }

        /// <summary>
        /// The function adds a drone 
        /// </summary>
        /// <param name="drone">drone</param>
        public void Add(BO.Drone drone)
        {
            //מה עם התחנה?
            IDal.DO.WeightCategories weight = (IDal.DO.WeightCategories)drone.MaxWeight;
            dal.Add(new IDal.DO.Drone() { Id = drone.Id, MaxWeight = weight } );
        }

        /// <summary>
        /// The function adds a customer 
        /// </summary>
        /// <param name="customer">customer</param>
        public void Add(BO.Customer customer)
        {
            dal.Add(new IDal.DO.Customer() { Id = customer.Id, Name = customer.Name, Phone = customer.Phone}); 
        }

        /// <summary>
        /// The function adds a parcel 
        /// </summary>
        /// <param name="parcel">parcel</param>
        public void Add(BO.Parcel parcel)
        {
            dal.Add(new IDal.DO.Parcel());
        }
    }
}
