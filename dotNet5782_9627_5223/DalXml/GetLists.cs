using System;
using System.Collections.Generic;
using System.Text;
using DO;
using System.Linq;
using DalApi.DO;
using static DalXml.XMLTools;
using System.Runtime.CompilerServices;

namespace DalXml
{
    sealed partial class DalXml
    {
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<DroneCharge> DronesChargingInMe(Predicate<DroneCharge> InMe)
        {
            List<DroneCharge> droneCharges = LoadListFromXmlSerializer<DroneCharge>(baseStationsPath);
            return droneCharges.Where(drone => InMe(drone));
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<int> GetDronesIdInBaseStation(int stationId)
        {
            List<DroneCharge> droneCharges = LoadListFromXmlSerializer<DroneCharge>(baseStationsPath);
            return droneCharges.FindAll(dc => dc.StationId == stationId).ConvertAll(dc => dc.DroneId);
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<BaseStation> GetBaseStationsList()
        {
            return LoadListFromXmlSerializer<BaseStation>(droneChargesPath);
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<Drone> GetDronesList()
        {
            return LoadListFromXmlSerializer<Drone>(dronesPath); 
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
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

        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<Parcel> GetParcelsList()
        {
            return LoadListFromXmlSerializer<Parcel>(parcelsPath);
        }


        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<BaseStation> AvailableChargeStations(Predicate<BaseStation> AvailableSlots)
        {
            return LoadListFromXmlSerializer<BaseStation>(baseStationsPath).Where(item => AvailableSlots(item));
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<Parcel> Parcels(Predicate<Parcel> predicate)
        {
            return LoadListFromXmlSerializer<Parcel>(parcelsPath).Where(parcel => predicate(parcel));

        }
    }
}

