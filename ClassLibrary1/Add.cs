using IDAL.DO;
using System;
using System.Collections.Generic;
using System.Text;
using static DalObject.DataSource;
using System.Linq;
using  DalObject;


namespace IBL
{
    public partial class BL :IBL
    {
        /// <summary>
        /// The function adds a base station 
        /// </summary>
        /// <param name="baseStation">base station</param>
        public void Add(BO.BaseStation baseStation)
        {
            dal.Add(new IDAL.DO.BaseStation() { Id = baseStation.Id, Name = baseStation.Name, Longitude = baseStation.Location.CoorLongitude, Latitude = baseStation.Location.CoorLatitude, ChargeSlots = baseStation.ChargeSlots}); 
        }

        /// <summary>
        /// The function adds a drone 
        /// </summary>
        /// <param name="drone">drone</param>
        public void Add(BO.Drone drone)
        {
            dal.Add(new IDAL.DO.Drone());
        }

        /// <summary>
        /// The function adds a customer 
        /// </summary>
        /// <param name="customer">customer</param>
        public void Add(BO.Customer customer)
        {
            dal.Add(new IDAL.DO.Customer()); 
        }

        /// <summary>
        /// The function adds a parcel 
        /// </summary>
        /// <param name="parcel">parcel</param>
        public void Add(BO.Parcel parcel)
        {
            dal.Add(new IDAL.DO.Parcel());
        }
    }
}
