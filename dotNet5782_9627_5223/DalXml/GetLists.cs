using System;
using System.Collections.Generic;
using System.Text;
using DO;
using System.Linq;

namespace DalXml
{
    sealed partial class DalXml
    {

        public IEnumerable<DroneCharge> DronesChargingInMe(Predicate<DroneCharge> InMe)
        {
            List<DroneCharge> droneCharges = Dal.XMLTools.LoadListFromXmlSerializer<DroneCharge>(baseStationsPath);
            return droneCharges.Where(drone => InMe(drone));
        }

        public IEnumerable<int> GetDronesIdInBaseStation(int stationId)
        {
            List<DroneCharge> droneCharges = Dal.XMLTools.LoadListFromXmlSerializer<DroneCharge>(baseStationsPath);
            return droneCharges.FindAll(dc => dc.StationId == stationId).ConvertAll(dc => dc.DroneId);
        }
        public IEnumerable<BaseStation> GetBaseStationsList()
        {
            return Dal.XMLTools.LoadListFromXmlSerializer<BaseStation>(droneChargesPath);
        }

        public IEnumerable<Drone> GetDronesList()
        {
            return Dal.XMLTools.LoadListFromXmlSerializer<Drone>(dronesPath); ;
        }

        public IEnumerable<Customer> GetCustomersList()
        {
            return Dal.XMLTools.LoadListFromXmlSerializer<Customer>(customersPath); ;
        }

        public IEnumerable<Parcel> GetParcelsList()
        {
            return Dal.XMLTools.LoadListFromXmlSerializer<Parcel>(parcelsPath);
        }



        public IEnumerable<BaseStation> AvailableChargeStations(Predicate<BaseStation> AvailableSlots)
        {
            return Dal.XMLTools.LoadListFromXmlSerializer<BaseStation>(baseStationsPath).Where(item => AvailableSlots(item));
        }

        public IEnumerable<Parcel> Parcels(Predicate<Parcel> predicate)
        {
            return Dal.XMLTools.LoadListFromXmlSerializer<Parcel>(parcelsPath).Where(parcel => predicate(parcel));

        }
    }
}

