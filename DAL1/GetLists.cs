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
            List<DroneCharge> dronesCharge = null;
            foreach (DroneCharge droneCharge in DronesChargeList)
            {
                if (droneCharge.StationId == stationId)
                {
                    dronesCharge.Add(droneCharge);
                }
            }
            return dronesCharge;
        }

        public IEnumerable<int> GetDronesIdInBaseStation(int requestedId)
        {
            return DronesChargeList.FindAll(dc => dc.StationId == requestedId).ConvertAll(dc => dc.DroneId);
        }

        public IEnumerable<Parcel> NotAssociatedParcels()
        {
            List<Parcel> parcels = new List<Parcel>();
            for (int i = 0; i < ParcelsList.Count; i++)
            {
                if (ParcelsList[i].DroneId == -1)
                {
                    parcels.Add(ParcelsList[i]);
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
            List<BaseStation> availableChargingSlotsList = new List<BaseStation>();
            for (int i = 0; i < BaseStationsList.Count; i++)
            {
                availableChargingSlotsList[i] = BaseStationsList[i];
                availableChargingSlotsList[i].ChargeSlots -= DronesChargeList.ToArray().Count(dc => dc.StationId == availableChargingSlotsList[i].Id);
            }
            return availableChargingSlotsList;
        }
    }
}
