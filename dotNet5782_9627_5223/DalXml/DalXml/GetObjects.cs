using DalApi;
using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using static DalXml.XMLTools;
using System.Runtime.CompilerServices;
using System.Xml.Linq;

namespace DalXml
{
    sealed partial class DalXml
    {
        [MethodImpl(MethodImplOptions.Synchronized)]
        public BaseStation GetBaseStation(int baseStationId)
        {
            List<BaseStation> baseStations = LoadListFromXmlSerializer<BaseStation>(baseStationsPath).ToList();
            CheckExistenceOfBaseStation(baseStationId);
            return baseStations.First(item => item.Id == baseStationId);
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public Drone GetDrone(int droneId)
        {
            List<Drone> drones = LoadListFromXmlSerializer<Drone>(dronesPath).ToList();
            CheckExistenceOfDrone(droneId);
            return drones.First(item => item.Id == droneId);
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public DroneCharge GetDroneCharge(int droneId)
        {
            List<DroneCharge> drones = LoadListFromXmlSerializer<DroneCharge>(droneChargesPath).ToList();
            CheckExistenceOfDroneCharge(droneId);
            return drones.First(item => item.DroneId == droneId);
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public Customer GetCustomer(string customerId)
        {
            CheckExistenceOfCustomer(customerId);
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
                                Longitude = Convertors.ExlementToCoordinate(c.Element("Location"), Locations.Longitude),
                                Latitude = Convertors.ExlementToCoordinate(c.Element("Location"), Locations.Longitude)

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
            List<Parcel> parcels = GetParcelsList().ToList();//
            CheckExistenceOfParcel(parcelId);
            return parcels.First(item => item.Id == parcelId);
        }
    }
}
