using System;
using System.Collections.Generic;
using System.Text;
using DO;
using System.Linq;

namespace DalXml
{
    public partial class DalXml
    {

        public IEnumerable<DroneCharge> DronesChargingInMe(Predicate<DroneCharge>InMe)
        {
            return new List<DroneCharge>();
        }

        public IEnumerable<int> GetDronesIdInBaseStation(int stationId)
        {
            return new List<int>();
        }
        public IEnumerable<BaseStation> GetBaseStationsList()
        {
            return new List<BaseStation>();
        }

        public IEnumerable<Drone> GetDronesList()
        {
            return new List<Drone>();
        }

        public IEnumerable<Customer> GetCustomersList()
        {
            return new List<Customer>();
        }

        public IEnumerable<Parcel> GetParcelsList()
        {
            return new List<Parcel>();
        }

       

        public IEnumerable<BaseStation> AvailableChargeStations(Predicate<BaseStation>AvailableSlots)
        { 
            return new List<BaseStation>();
        }

        public IEnumerable<Parcel> Parcels(Predicate<Parcel> predicate)
        {
            return new List<Parcel>();
        }

    }
}
