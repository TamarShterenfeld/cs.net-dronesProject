using System;
using System.Collections.Generic;
using System.Text;
using DO;
using System.Linq;
using static DalXml.XMLTools;
using System.Runtime.CompilerServices;

namespace DalXml
{
    sealed partial class DalXml
    {
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<DroneCharge> DronesChargingInMe(Predicate<DroneCharge> InMe)
        {
            List<DroneCharge> droneCharges = LoadListFromXmlSerializer<DroneCharge>(droneChargesPath).ToList();
            return droneCharges.Where(drone => InMe(drone));
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<int> GetDronesIdInBaseStation(int stationId)
        {
            List<DroneCharge> droneCharges = LoadListFromXmlSerializer<DroneCharge>(droneChargesPath).ToList();
            return droneCharges.FindAll(dc => dc.StationId == stationId).ConvertAll(dc => dc.DroneId);
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<BaseStation> GetBaseStationsList()
        {
            return LoadListFromXmlSerializer<BaseStation>(baseStationsPath);
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<Drone> GetDronesList()
        {
            return LoadListFromXmlSerializer<Drone>(dronesPath); 
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<Customer> GetCustomersList()
        {
            List<Customer> customers;
            try
            {
                customers = (from c in CustomersRoot.Elements()
                            select GetCustomer(c.Element("id").Value)
                             ).ToList();
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

