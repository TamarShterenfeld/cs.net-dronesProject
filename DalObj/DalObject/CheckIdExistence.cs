using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DO;
using static DalObject.DataSource;

namespace DalObject
{
    partial class DalObject 
    {
        /// <summary>
        /// check if the id exists in the baseStationsList
        /// </summary>
        /// <param name="baseStationId">the id for checking</param>
        static void CheckExistenceOfBaseStation(int baseStationId)
        {       
            int baseStationIndex = BaseStationsList.FindIndex(item => item.Id == baseStationId);
            if (baseStationIndex == -1)
                throw new IntIdException(baseStationId);
        }

        /// <summary>
        /// check if the id doesn't exist in the BaseStationsList
        /// </summary>
        /// <param name="baseStationId">the id for checking</param>
        static void CheckNotExistenceOfBaseStation(int baseStationId)
        {
            int baseStationIndex = BaseStationsList.FindIndex(item => item.Id == baseStationId);
            if (baseStationIndex != -1)
                throw new IntIdException(baseStationId);
        }

        /// <summary>
        /// check if the id exists in the CustomersList
        /// </summary>
        /// <param name="customerId">the id for checking</param>
        static void CheckExistenceOfCustomer(string customerId)
        {
            int customerIndex = CustomersList.FindIndex(item => item.Id == customerId);
            if (customerIndex == -1)
                throw new StringIdException(customerId);
        }

        /// <summary>
        /// check if the id doesn't exist in the CustomersList
        /// </summary>
        /// <param name="customerId">the id for checking</param>
        static void CheckNotExistenceOfCustomer(string customerId)
        {
            int customerIndex = CustomersList.FindIndex(item => item.Id == customerId);
            if (customerIndex != -1)
                throw new StringIdException(customerId);
        }

        /// <summary>
        /// check if the id exists in the DronesList
        /// </summary>
        /// <param name="droneId">the id for checking</param>
        static void CheckExistenceOfDrone(int droneId)
        {
            int droneIndex = DronesList.FindIndex(item => item.Id == droneId);
            if (droneIndex == -1)
                throw new IntIdException(droneId);
        }

        /// <summary>
        /// check if the id doesn't exist in the DronesList
        /// </summary>
        /// <param name="droneId">the id for checking</param>
        static void CheckNotExistenceOfDrone(int droneId)
        {
            int droneIndex = DronesList.FindIndex(item => item.Id == droneId);
            if (droneIndex != -1)
                throw new IntIdException(droneId);
        }

        /// <summary>
        /// check if the id exists in the ParcelsList
        /// </summary>
        /// <param name="parcelId">the id for checking</param>
        static void CheckExistenceOfParcel(int parcelId)
        {
            int parcelIndex = ParcelsList.FindIndex(item => item.Id == parcelId);
            if (parcelIndex == -1)
                throw new IntIdException(parcelId);
        }

        /// <summary>
        /// check if the id doesn't exist in the ParcelsList
        /// </summary>
        /// <param name="parcelId">the id for checking</param>
        static void CheckNotExistenceOfParcel(int parcelId)
        {
            int parcelIndex = ParcelsList.FindIndex(item => item.Id == parcelId);
            if (parcelIndex != -1)
                throw new IntIdException(parcelId);
        }

        /// <summary>
        /// check if the id exists in the DronesChargeList
        /// </summary>
        /// <param name="droneForListId">the id for checking</param>
        static void CheckExistenceOfDroneCharge(int droneForListId)
        {
            int droneForListIndex = DronesChargeList.FindIndex(item => item.DroneId == droneForListId);
            if (droneForListIndex == -1)
                throw new IntIdException(droneForListId);
        }

        /// <summary>
        /// check if the id doesn't exist in the DronesChargeList
        /// </summary>
        /// <param name="droneForListId">the id for checking</param>
        static void CheckNotExistenceOfDroneCharge(int droneForListId)
        {
            int droneForListIndex = DronesChargeList.FindIndex(item => item.DroneId == droneForListId);
            if (droneForListIndex != -1)
                throw new IntIdException(droneForListId);
        }

    }
}
