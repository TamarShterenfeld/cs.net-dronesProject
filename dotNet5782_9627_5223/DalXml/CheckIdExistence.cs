using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using DO;
using static Dal.XMLTools;

namespace DalXml
{
    sealed partial class DalXml
    {
        /// <summary>
        /// check if the id exists in the baseStationsList
        /// </summary>
        /// <param name="baseStationId">the id for checking</param>
        private void CheckExistenceOfBaseStation(int baseStationId)
        {
            List<BaseStation> baseStations = LoadListFromXmlSerializer<DO.BaseStation>(baseStationsPath);
            int baseStationIndex = baseStations.FindIndex(item => item.Id == baseStationId);
            if (baseStationIndex == -1)
                throw new IntIdException(baseStationId);
            Dal.XMLTools.SaveListToXmlSerializer<BaseStation>(baseStations, baseStationsPath);
        }

        private void CheckNotExistenceOfBaseStation(int baseStationId)
        {
            List<BaseStation> baseStations = LoadListFromXmlSerializer<DO.BaseStation>(baseStationsPath);
            int baseStationIndex = baseStations.FindIndex(item => item.Id == baseStationId);
            if (baseStationIndex != -1)
                throw new IntIdException(baseStationId);
            SaveListToXmlSerializer<BaseStation>(baseStations, baseStationsPath);
        }

        /// <summary>
        /// check if the id exists in the CustomersList
        /// </summary>
        /// <param name="customerId">the id for checking</param>
        void CheckExistenceOfCustomer(string customerId)
        {
            LoadData();
            dynamic ans;
                 ans = (from p in CustomersRoot.Elements()
                           where p.Element("id").Value == customerId
                           select p).First();

            if (ans == null) throw new IntIdException(customerId); 
        }

        /// <summary>
        /// check if the id doesn't exist in the CustomersList
        /// </summary>
        /// <param name="customerId">the id for checking</param>
        void CheckNotExistenceOfCustomer(string customerId)
        {
            LoadData();
            dynamic ans;
            ans = (from p in CustomersRoot.Elements()
                   where p.Element("id").Value == customerId
                   select p).First();

            if (ans != null) throw new IntIdException(customerId);
        }

        /// <summary>
        /// check if the id exists in the DronesList
        /// </summary>
        /// <param name="droneId">the id for checking</param>
        void CheckExistenceOfDrone(int droneId)
        {
            List<Drone> drones = Dal.XMLTools.LoadListFromXmlSerializer<DO.Drone>(dronesPath);
            int droneIndex = drones.FindIndex(item => item.Id == droneId);
            if (droneIndex == -1)
                throw new IntIdException(droneId);
            Dal.XMLTools.SaveListToXmlSerializer<Drone>(drones, dronesPath);
        }

        /// <summary>
        /// check if the id doesn't exist in the DronesList
        /// </summary>
        /// <param name="droneId">the id for checking</param>
        void CheckNotExistenceOfDrone(int droneId)
        {
            List<Drone> drones = Dal.XMLTools.LoadListFromXmlSerializer<DO.Drone>(dronesPath);
            int droneIndex = drones.FindIndex(item => item.Id == droneId);
            if (droneIndex != -1)
                throw new IntIdException(droneId);
            Dal.XMLTools.SaveListToXmlSerializer<Drone>(drones, dronesPath);
        }

        /// <summary>
        /// check if the id exists in the ParcelsList
        /// </summary>
        /// <param name="parcelId">the id for checking</param>
        void CheckExistenceOfParcel(int parcelId)
        {
            List<Parcel> parcels = Dal.XMLTools.LoadListFromXmlSerializer<DO.Parcel>(parcelsPath);
            int droneIndex = parcels.FindIndex(item => item.Id == parcelId);
            if (droneIndex == -1)
                throw new IntIdException(parcelId);
            Dal.XMLTools.SaveListToXmlSerializer<Parcel>(parcels, parcelsPath);
        }

        /// <summary>
        /// check if the id doesn't exist in the ParcelsList
        /// </summary>
        /// <param name="parcelId">the id for checking</param>
        void CheckNotExistenceOfParcel(int parcelId)
        {
            List<Parcel> parcels = Dal.XMLTools.LoadListFromXmlSerializer<DO.Parcel>(parcelsPath);
            int droneIndex = parcels.FindIndex(item => item.Id == parcelId);
            if (droneIndex != -1)
                throw new IntIdException(parcelId);
            Dal.XMLTools.SaveListToXmlSerializer<Parcel>(parcels, parcelsPath);
        }

        /// <summary>
        /// check if the id exists in the DronesChargeList
        /// </summary>
        /// <param name="droneForListId">the id for checking</param>
        void CheckExistenceOfDroneCharge(int droneForListId)
        {
            List<DroneCharge> drones = Dal.XMLTools.LoadListFromXmlSerializer<DO.DroneCharge>(dronesPath);
            int droneIndex = drones.FindIndex(item => item.DroneId == droneForListId);
            if (droneIndex == -1)
                throw new IntIdException(droneForListId);
            Dal.XMLTools.SaveListToXmlSerializer<DroneCharge>(drones, parcelsPath);
        }

        /// <summary>
        /// check if the id doesn't exist in the DronesChargeList
        /// </summary>
        /// <param name="droneForListId">the id for checking</param>
        static void CheckNotExistenceOfDroneCharge(int droneForListId)
        {

        }

    }
}
