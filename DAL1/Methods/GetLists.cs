using System;
using System.Collections.Generic;
using System.Text;
using static DalObject.DataSource;
using IDal.DO;
using System.Linq;

namespace DalObject
{
    public partial class DalObject
    {

        public IEnumerable<DroneCharge> DronesChargingInMe(Predicate<DroneCharge>InMe)
        {
            return DronesChargeList.Where(drone => InMe(drone));
        }

        public IEnumerable<int> GetDronesIdInBaseStation(int stationId)
        {
            return DronesChargeList.FindAll(dc => dc.StationId == stationId).ConvertAll(dc => dc.DroneId);
        }
        public IEnumerable<BaseStation> GetBaseStationsList()
        {
            return BaseStationsList;
        }

        public IEnumerable<Drone> GetDronesList()
        {
            return DronesList;
        }

        public IEnumerable<Customer> GetCustomersList()
        {
            return CustomersList;
        }

        public IEnumerable<Parcel> GetParcelsList()
        {
            return ParcelsList;
        }

       

        public IEnumerable<BaseStation> AvailableChargeStations(Predicate<BaseStation>AvailableSlots)
        { 
            return BaseStationsList.Where(station => AvailableSlots(station));
        }

        public IEnumerable<Parcel> Parcels(Predicate<Parcel> predicate)
        {
            return ParcelsList.Where(parcel => predicate(parcel));
        }

    }
}
