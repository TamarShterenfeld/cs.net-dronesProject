using System;
using System.Collections.Generic;
using System.Text;
using static DalObject.DataSource;
using IDal.DO;
using System.Linq;
using DAL.DO;

namespace IBL
{
    public partial class BL : IBL
    {

        public static int GetParcelIndex() 
        {
            return DalObject.DalObject.IncreaseParcelIndex();
        }

        private void CheckExistenceOfBaseStation(int baseStationId)
        {
            List<IDal.DO.BaseStation> baseStations = (List<IDal.DO.BaseStation>)dal.GetBaseStationsList();
            int baseStationIndex = baseStations.FindIndex(item => item.Id == baseStationId);
            if (baseStationIndex == -1)
                throw new IntIdException(baseStationId);
        }

        private void CheckExistenceOfCustomer(string customerId)
        {
            List<IDal.DO.Customer> customers = (List<IDal.DO.Customer>)dal.GetCustomersList();
            int customerIndex = customers.FindIndex(item => item.Id == customerId);
            if (customerIndex == -1)
                throw new IntIdException(customerId);
        }

        private void CheckExistenceOfDrone(int droneId)
        {
            List<IDal.DO.Drone> drones = (List<IDal.DO.Drone>)dal.GetDronesList();
            int droneIndex = drones.FindIndex(item => item.Id == droneId);
            if (droneIndex == -1)
                throw new IntIdException(droneId);
        }

        private void CheckExistenceOfParcel(int parcelId)
        {
            List<IDal.DO.Parcel> parcels = (List<IDal.DO.Parcel>)dal.GetParcelsList();
            int parcelIndex = parcels.FindIndex(item => item.Id == parcelId) ;
            if (parcelIndex == -1)
                throw new IntIdException(parcelId);
        }

        private void CheckExistenceOfDroneForList(int droneForListId)
        {
            int droneForListIndex = dronesForList.FindIndex(item => item.Id == droneForListId);
            if (droneForListIndex == -1)
                throw new IntIdException(droneForListId);
        }
    }
}
