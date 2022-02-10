using System;
using System.Collections.Generic;
using System.Text;
using DO;
using System.Linq;
using DalApi.DO;
using static DalXml.XMLTools;

namespace DalXml
{
    public sealed partial class DalXml
    {

        public IEnumerable<DroneCharge> DronesChargingInMe(Predicate<DroneCharge> InMe)
        {
            List<DroneCharge> droneCharges = LoadListFromXmlSerializer<DroneCharge>(baseStationsPath);
            return droneCharges.Where(drone => InMe(drone));
        }

        public IEnumerable<int> GetDronesIdInBaseStation(int stationId)
        {
            List<DroneCharge> droneCharges = LoadListFromXmlSerializer<DroneCharge>(baseStationsPath);
            return droneCharges.FindAll(dc => dc.StationId == stationId).ConvertAll(dc => dc.DroneId);
        }
        public IEnumerable<BaseStation> GetBaseStationsList()
        {
            return LoadListFromXmlSerializer<BaseStation>(droneChargesPath);
        }

        public IEnumerable<Drone> GetDronesList()
        {
            return LoadListFromXmlSerializer<Drone>(dronesPath); 
        }

        public IEnumerable<Customer> GetCustomersList()
        {
            LoadData();
            List<Customer> customers;
            try
            {
                customers = (from c in CustomersRoot.Elements()
                            select new Customer()
                            {
                                Id = c.Element("id").Value,
                                IsDeleted = bool.Parse(c.Element("isDeleted").Value),
                                Name = c.Element("name").Value,
                                Phone = c.Element("phone").Value,
                                Longitude = (c.Element("location").Element("longitude").Value).Parse(Locations.Longitude),
                                Latitude = (c.Element("location").Element("longitude").Value).Parse(Locations.Latitude)
                            }).ToList();
            }
            catch
            {
                customers = null;
            }
            return customers;
        }

        public IEnumerable<Parcel> GetParcelsList()
        {
            return LoadListFromXmlSerializer<Parcel>(parcelsPath);
        }



        public IEnumerable<BaseStation> AvailableChargeStations(Predicate<BaseStation> AvailableSlots)
        {
            return LoadListFromXmlSerializer<BaseStation>(baseStationsPath).Where(item => AvailableSlots(item));
        }

        public IEnumerable<Parcel> Parcels(Predicate<Parcel> predicate)
        {
            return LoadListFromXmlSerializer<Parcel>(parcelsPath).Where(parcel => predicate(parcel));

        }
    }
}

