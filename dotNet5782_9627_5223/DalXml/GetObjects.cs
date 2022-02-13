using DalApi.DO;
using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using static DalXml.XMLTools;
using System.Runtime.CompilerServices;

namespace DalXml
{
    sealed partial class DalXml
    {
        [MethodImpl(MethodImplOptions.Synchronized)]
        public BaseStation GetBaseStation(int baseStationId)
        {
            List<BaseStation> baseStations = LoadListFromXmlSerializer<BaseStation>(baseStationsPath);
            CheckExistenceOfBaseStation(baseStationId);
            return baseStations.First(item => item.Id == baseStationId);
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public Drone GetDrone(int droneId)
        {
            List<Drone> drones = LoadListFromXmlSerializer<Drone>(dronesPath);
            CheckExistenceOfDrone(droneId);
            return drones.First(item => item.Id == droneId);
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public DroneCharge GetDroneCharge(int droneId)
        {
            List<DroneCharge> drones = LoadListFromXmlSerializer<DroneCharge>(droneChargesPath);
            CheckExistenceOfDroneCharge(droneId);
            return drones.First(item => item.DroneId == droneId);
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public Customer GetCustomer(string customerId)
        {
            List<Customer> customers = LoadListFromXmlSerializer<Customer>(customersPath);
            CheckExistenceOfCustomer(customerId);
            LoadData();
            Customer customer;
            try
            {
                customer = (from c in CustomersRoot.Elements()
                           where c.Element("id").Value == customerId
                            select new Customer()
                           {
                                Id = c.Element("id").Value,
                                IsDeleted = bool.Parse(c.Element("isDeleted").Value),
                                Name = c.Element("name").Value,
                                Phone = c.Element("phone").Value,
                                Longitude = (c.Element("location").Element("longitude").Value).Parse(Locations.Longitude),
                                Latitude = (c.Element("location").Element("longitude").Value).Parse(Locations.Latitude)

                            }).FirstOrDefault();
            }
            catch
            {
                customer = default;
            }
            return customer;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public Parcel GetParcel(int parcelId)
        {
            List<Parcel> parcels = LoadListFromXmlSerializer<Parcel>(parcelsPath);
            CheckExistenceOfParcel(parcelId);
            return parcels.First(item => item.Id == parcelId);
        }
    }
}
