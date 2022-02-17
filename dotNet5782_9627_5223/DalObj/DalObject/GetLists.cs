using System;
using System.Collections.Generic;
using System.Text;
using static DalObject.DataSource;
using DO;
using System.Linq;
using System.Runtime.CompilerServices;

namespace DalObject
{
    public partial class DalObject
    {

        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<DroneCharge> DronesChargingInMe(Predicate<DroneCharge>InMe)
        {
            return DronesChargeList.Where(drone => InMe(drone));
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<int> GetDronesIdInBaseStation(int stationId)
        {
            return DronesChargeList.FindAll(dc => dc.StationId == stationId).ConvertAll(dc => dc.DroneId);
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<BaseStation> GetBaseStationsList()
        {
            return BaseStationsList.Where(item=>!item.IsDeleted);
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<Drone> GetDronesList()
        {
            return DronesList;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<Customer> GetCustomersList()
        {
            return CustomersList.Where(item => !item.IsDeleted);
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<Parcel> GetParcelsList()
        {
            return ParcelsList;
        }


        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<BaseStation> AvailableChargeStations(Predicate<BaseStation>AvailableSlots)
        { 
            return BaseStationsList.Where(station => AvailableSlots(station));
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<Parcel> Parcels(Predicate<Parcel> predicate)
        {
            return ParcelsList.Where(parcel => predicate(parcel));
        }

    }
}
