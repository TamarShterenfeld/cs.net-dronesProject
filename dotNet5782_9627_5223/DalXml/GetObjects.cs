using DO;
using System.Collections.Generic;
using System.Linq;

namespace DalXml
{
    sealed partial class DalXml
    {

        public BaseStation GetBaseStation(int baseStationId)
        {
            List<BaseStation> baseStations = Dal.XMLTools.LoadListFromXmlSerializer<BaseStation>(baseStationsPath);
            CheckExistenceOfBaseStation(baseStationId);
            return baseStations.First(item => item.Id == baseStationId);
        }


        public Drone GetDrone(int droneId)
        {
            List<Drone> drones = Dal.XMLTools.LoadListFromXmlSerializer<Drone>(dronesPath);
            CheckExistenceOfDrone(droneId);
            return drones.First(item => item.Id == droneId);
        }

        public DroneCharge GetDroneCharge(int droneId)
        {
            List<DroneCharge> drones = Dal.XMLTools.LoadListFromXmlSerializer<DroneCharge>(droneChargesPath);
            CheckExistenceOfDroneCharge(droneId);
            return drones.First(item => item.DroneId == droneId);
        }


        public Customer GetCustomer(string customerId)
        {
            List<Customer> customers = Dal.XMLTools.LoadListFromXmlSerializer<Customer>(customersPath);
            CheckExistenceOfCustomer(customerId);
            return customers.First(item => item.Id == customerId);
        }


        public Parcel GetParcel(int parcelId)
        {
            List<Parcel> parcels = Dal.XMLTools.LoadListFromXmlSerializer<Parcel>(parcelsPath);
            CheckExistenceOfParcel(parcelId);
            return parcels.First(item => item.Id == parcelId);
        }
    }
}
