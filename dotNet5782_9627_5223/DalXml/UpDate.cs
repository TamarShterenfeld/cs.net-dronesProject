using System;
using System.Collections.Generic;
using System.Text;
using DO;
using System.Linq;


namespace DalXml
{
    sealed partial class DalXml
    {
        
        public void UpDate(Drone drone, int id)
        {
            List<Drone> drones = Dal.XMLTools.LoadListFromXmlSerializer<Drone>(dronesPath);
            CheckExistenceOfDrone(id);
            drones.Remove(drones.Find(item => item.Id == id));
            drones.Add(drone);
            Dal.XMLTools.SaveListToXmlSerializer<Drone>(drones, dronesPath);
        }

       
        public void UpDate(BaseStation baseStation, int id)
        {
            List<BaseStation> baseStations = Dal.XMLTools.LoadListFromXmlSerializer<BaseStation>(baseStationsPath);
            CheckExistenceOfBaseStation(id);
            baseStations.Remove(baseStations.Find(item => item.Id == id));
            baseStations.Add(baseStation);
            Dal.XMLTools.SaveListToXmlSerializer<BaseStation>(baseStations, baseStationsPath);
        }

        public void UpDate(Customer customer, string id)
        {
            List<Customer> customers = Dal.XMLTools.LoadListFromXmlSerializer<Customer>(customersPath);
            CheckExistenceOfCustomer(id);
            customers.Remove(customers.Find(item => item.Id == id));
            customers.Add(customer);
            Dal.XMLTools.SaveListToXmlSerializer<Customer>(customers, customersPath);
        }
 
        public void UpDate(Parcel parcel, int id)
        {
            List<Parcel> parcels = Dal.XMLTools.LoadListFromXmlSerializer<Parcel>(parcelsPath);
            CheckExistenceOfParcel(id);
            parcels.Remove(parcels.Find(item => item.Id == id));
            parcels.Add(parcel);
            Dal.XMLTools.SaveListToXmlSerializer<Parcel>(parcels, parcelsPath);
        }
        
        public void SendDroneToRecharge(int droneId, int baseStationId)
        {
            CheckNotExistenceOfDroneCharge(droneId);
            DroneCharge droneCharge = new() { DroneId = droneId, StationId = baseStationId, EntryTime = DateTime.Now };
            Add(droneCharge);
        }

        public void ReleaseDroneFromRecharge(int droneId)
        {
            CheckExistenceOfDroneCharge(droneId);
            DroneCharge droneCharge = GetDroneCharge(droneId);
            Remove(droneCharge);
        }
    }
}

