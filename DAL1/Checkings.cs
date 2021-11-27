using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.DO;
using static DalObject.DataSource;

namespace DalObject
{
    public partial class DalObject : IDal.IDal
    {
        void CheckExistenceOfBaseStation(int baseStationId)
        {       
            int baseStationIndex = BaseStationsList.FindIndex(item => item.Id == baseStationId);
            if (baseStationIndex == -1)
                throw new IntIdException(baseStationId);
        }

        void CheckNotExistenceOfBaseStation(int baseStationId)
        {
            int baseStationIndex = BaseStationsList.FindIndex(item => item.Id == baseStationId);
            if (baseStationIndex != -1)
                throw new IntIdException(baseStationId);
        }

        void CheckExistenceOfCustomer(string customerId)
        {
            int customerIndex = CustomersList.FindIndex(item => item.Id == customerId);
            if (customerIndex == -1)
                throw new IntIdException(customerId);
        }

        void CheckNotExistenceOfCustomer(string customerId)
        {
            int customerIndex = CustomersList.FindIndex(item => item.Id == customerId);
            if (customerIndex != -1)
                throw new IntIdException(customerId);
        }

        void CheckExistenceOfDrone(int droneId)
        {
            int droneIndex = DronesList.FindIndex(item => item.Id == droneId);
            if (droneIndex == -1)
                throw new IntIdException(droneId);
        }

        void CheckNotExistenceOfDrone(int droneId)
        {
            int droneIndex = DronesList.FindIndex(item => item.Id == droneId);
            if (droneIndex != -1)
                throw new IntIdException(droneId);
        }

        void CheckExistenceOfParcel(int parcelId)
        {
            int parcelIndex = ParcelsList.FindIndex(item => item.Id == parcelId);
            if (parcelIndex == -1)
                throw new IntIdException(parcelId);
        }

        void CheckNotExistenceOfParcel(int parcelId)
        {
            int parcelIndex = ParcelsList.FindIndex(item => item.Id == parcelId);
            if (parcelIndex != -1)
                throw new IntIdException(parcelId);
        }

        void CheckExistenceOfDroneCharge(int droneForListId)
        {
            int droneForListIndex = DronesChargeList.FindIndex(item => item.DroneId == droneForListId);
            if (droneForListIndex == -1)
                throw new IntIdException(droneForListId);
        }

        void CheckNotExistenceOfDroneCharge(int droneForListId)
        {
            int droneForListIndex = DronesChargeList.FindIndex(item => item.DroneId == droneForListId);
            if (droneForListIndex != -1)
                throw new IntIdException(droneForListId);
        }

    }
}
