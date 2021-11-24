using System;
using System.Collections.Generic;
using System.Text;
using static DalObject.DataSource;
using IDal.DO;


namespace DalObject
{
    public partial class DalObject:IDal.IDal
    {

        /// <inheritdoc />
        public void AssociateParcel(int parcelId, int droneId)
        {
            for (int i = 0; i < ParcelsList.Count; i++)
            {
                if (ParcelsList[i].Id == parcelId)
                {
                    Parcel parcel = ParcelsList[i];
                    parcel.DroneId = droneId;
                    ParcelsList[i] = parcel;
                    break;
                }
            }
        }

        /// <inheritdoc />
        public void PickupParcel(int parcelId)
        {
            for (int i = 0; i < ParcelsList.Count; i++)
            {
                if (ParcelsList[i].Id == parcelId)
                {
                    Parcel parcel = ParcelsList[i];
                    parcel.PickUpDate = DateTime.Now;
                    ParcelsList[i] = parcel;
                    break;
                }
            }
        }

        /// <inheritdoc />
        public void SendDroneToRecharge(int droneId, int baseStationId)
        {
            DronesChargeList.Add(new DroneCharge() { DroneId = droneId, StationId = baseStationId, EntryTime = DateTime.Now });
        }

        /// <inheritdoc />
        public void ReleaseDroneFromRecharge(int droneId)
        {
            DronesChargeList.RemoveAll(dc => dc.DroneId == droneId);
        }
    }
}
