using System;
using System.Collections.Generic;
using System.Text;
using DO;
using System.Linq;
using System.Xml.Linq;
using static DalXml.XMLTools;


namespace DalXml
{
    sealed partial class DalXml
    {
        
        public void UpDate(Drone drone, int id)
        {
            List<Drone> drones = LoadListFromXmlSerializer<Drone>(dronesPath);
            CheckExistenceOfDrone(id);
            drones.Remove(drones.Find(item => item.Id == id));
            drones.Add(drone);
            SaveListToXmlSerializer<Drone>(drones, dronesPath);
        }

       
        public void UpDate(BaseStation baseStation, int id)
        {
            List<BaseStation> baseStations = LoadListFromXmlSerializer<BaseStation>(baseStationsPath);
            CheckExistenceOfBaseStation(id);
            baseStations.Remove(baseStations.Find(item => item.Id == id));
            baseStations.Add(baseStation);
            SaveListToXmlSerializer<BaseStation>(baseStations, baseStationsPath);
        }

        public void UpDate(Customer customer, string id)
        {

            XElement customerXElement = (from c in CustomersRoot.Elements()
                                         where c.Element("id").Value == id
                                         select c).FirstOrDefault();
            customerXElement.Element("id").Value = customer.Id;
            customerXElement.Element("name").Value = customer.Name;
            customerXElement.Element("phone").Value = customer.Phone;
            customerXElement.Element("isDeleted").Value = customer.IsDeleted.ToString();
            customerXElement.Element("location").Element("longitude").Value = customer.Longitude.ToString();
            customerXElement.Element("location").Element("latitude").Value = customer.Latitude.ToString();
            CustomersRoot.Save(customersPath);
        }
    
 
        public void UpDate(Parcel parcel, int id)
        {
            List<Parcel> parcels = LoadListFromXmlSerializer<Parcel>(parcelsPath);
            CheckExistenceOfParcel(id);
            parcels.Remove(parcels.Find(item => item.Id == id));
            parcels.Add(parcel);
            SaveListToXmlSerializer<Parcel>(parcels, parcelsPath);
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

