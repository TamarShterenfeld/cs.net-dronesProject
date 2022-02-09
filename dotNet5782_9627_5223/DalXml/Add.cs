using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using DO;

namespace DalXml
{
    /// <summary>
    ///the class DalObject contains all the needed methods 
    ///which are connected to the data (in DataSource class) of the program.
    /// </summary>
    public partial class DalXml
    {
        public void Add(BaseStation baseStation)
        {
            List<BaseStation> BaseStation = Dal.XMLTools.LoadListFromXmlSerializer<DO.BaseStation>(baseStationsPath);
            CheckNotExistenceOfBaseStation(baseStation.Id);
            BaseStation.Add(baseStation);

        }


        public void Add(Customer customer)
        {
            List<Customer> CustomersList = Dal.XMLTools.LoadListFromXmlSerializer<DO.Customer>(customersPath);
            CheckNotExistenceOfCustomer(customer.Id);
            CustomersList.Add(customer);
        }


        public void Add(Drone drone)
        {
            
        }

        public void Add(Parcel parcel)
        {
            
        }

        public void Add(DroneCharge droneCharge)
        {
            
        }
    }
}




