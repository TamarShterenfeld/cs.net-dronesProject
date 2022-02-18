using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using DO;
using System.Xml.Linq;
using static DalXml.XMLTools;
using System.Runtime.CompilerServices;

namespace DalXml
{
    /// <summary>
    ///the class DalObject contains all the needed methods 
    ///which are connected to the data (in DataSource class) of the program.
    /// </summary>
    sealed partial class DalXml
    {
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void Add(BaseStation baseStation)
        {
            List<BaseStation> baseStations = LoadListFromXmlSerializer<DO.BaseStation>(baseStationsPath).ToList();
            CheckNotExistenceOfBaseStation(baseStation.Id);
            baseStations.Add(baseStation);
            SaveListToXmlSerializer<BaseStation>(baseStations, baseStationsPath);
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void Add(Customer customer)
        {
            CheckNotExistenceOfCustomer(customer.Id);
            Coordinate longit =  customer.Longitude;
            Coordinate latit = customer.Latitude;
            XElement id = new XElement("id", customer.Id);
            XElement name = new XElement("name", customer.Name);
            XElement phone = new XElement("phone", customer.Phone);
            XElement isDeleted = new XElement("isDeleted", customer.IsDeleted);
            XElement longitude = Convertors.CoordinateToXElement(longit, Locations.Longitude);
            XElement latitude = Convertors.CoordinateToXElement(latit, Locations.Latitude);
            XElement location = new XElement("Location",longitude, latitude);
            XElement myCustomer = new XElement("Customer", id, name, phone,isDeleted, location);
            CustomersRoot.Add(myCustomer);
            CustomersRoot.Save(dirPath + customersPath);
        }

       
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void Add(Drone drone)
        {
            List<Drone> drones = LoadListFromXmlSerializer<DO.Drone>(dronesPath).ToList();
            CheckNotExistenceOfDrone(drone.Id);
            drones.Add(drone);
            SaveListToXmlSerializer<Drone>(drones, dronesPath);
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void Add(Parcel parcel)
        {
            List<Parcel> parcels = LoadListFromXmlSerializer < DO.Parcel>(parcelsPath).ToList();
            CheckNotExistenceOfParcel(parcel.Id);
            parcels.Add(parcel);
            SaveListToXmlSerializer<Parcel>(parcels, parcelsPath);
            IncreaseParcelIndex();
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void Add(DroneCharge droneCharge)
        {
            List<DroneCharge> dronesCharge = LoadListFromXmlSerializer<DO.DroneCharge>(droneChargesPath).ToList();
            CheckNotExistenceOfDroneCharge(droneCharge.DroneId);
            CheckExistenceOfDrone(droneCharge.DroneId);
            BaseStation baseStation = GetBaseStation(droneCharge.StationId);
            baseStation.ChargeSlots--;
            UpDate(baseStation, baseStation.Id);
            DronesChargeList.Add(droneCharge);
            SaveListToXmlSerializer<DroneCharge>(dronesCharge, droneChargesPath);
        }
    }
}




