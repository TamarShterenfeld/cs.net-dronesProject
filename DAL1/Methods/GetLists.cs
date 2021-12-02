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

        public IEnumerable<DroneCharge> DronesChargingInMe(int stationId)
        {
            List<DroneCharge> dronesCharge = new();
            foreach (DroneCharge droneCharge in DronesChargeList)
            {
                if (droneCharge.StationId == stationId)
                {
                    dronesCharge.Add(droneCharge);
                }
            }
            return dronesCharge;
        }

        public IEnumerable<int> GetDronesIdInBaseStation(int stationId)
        {
            return DronesChargeList.FindAll(dc => dc.StationId == stationId).ConvertAll(dc => dc.DroneId);
        }

        public IEnumerable<Parcel> NotAssociatedParcels()
        {
            DateTime date = new();
            List<Parcel> parcels = new ();
            foreach (var parcel in ParcelsList)
            {
                if (parcel.AssociationDate == date)
                {
                    parcels.Add(parcel);
                }
            }
            return parcels;
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

        public IEnumerable<DroneCharge> GetDronesCharge()
        {
            return DronesChargeList;
        }


        public IEnumerable<BaseStation> AvailableChargeStations()
        {
            List<BaseStation> availableChargingSlotsList = new ();
            foreach(var item in BaseStationsList)
            {
                int caught = CaughtChargeSlots(item.Id);
                if (item.ChargeSlots - caught > 0) { availableChargingSlotsList.Add(item); }
            }
            return availableChargingSlotsList;
        }

    }
}
