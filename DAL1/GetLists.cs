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

        /// <inheritdoc />
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

        /// <inheritdoc />
        public IEnumerable<int> GetDronesIdInBaseStation(int stationId)
        {
            return DronesChargeList.FindAll(dc => dc.StationId == stationId).ConvertAll(dc => dc.DroneId);
        }

        /// <inheritdoc />
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

        /// <inheritdoc />
        public IEnumerable<BaseStation> GetBaseStationsList()
        {
            return BaseStationsList;
        }

        /// <inheritdoc />
        public IEnumerable<Drone> GetDronesList()
        {
            return DronesList;
        }

        /// <inheritdoc />
        public IEnumerable<Customer> GetCustomersList()
        {
            return CustomersList;
        }

        /// <inheritdoc />
        public IEnumerable<Parcel> GetParcelsList()
        {
            return ParcelsList;
        }

        /// <inheritdoc />
        public IEnumerable<DroneCharge> GetDronesCharge()
        {
            return DronesChargeList;
        }


        /// <inheritdoc />
        public IEnumerable<BaseStation> AvailableChargeStations()
        {
            List<BaseStation> availableChargingSlotsList = new List<BaseStation>();
            for (int i = 0; i < BaseStationsList.Count; i++)
            {
                availableChargingSlotsList[i] = BaseStationsList[i];
                BaseStation currBaseStation = availableChargingSlotsList[i];
                currBaseStation.ChargeSlots -= DronesChargeList.ToArray().Count(dc => dc.StationId == availableChargingSlotsList[i].Id);
            }
            return availableChargingSlotsList;
        }
    }
}
