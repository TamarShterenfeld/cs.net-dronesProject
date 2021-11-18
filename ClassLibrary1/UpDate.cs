using System;
using System.Collections.Generic;
using System.Text;
using IBL.BO;
using System.Linq;

namespace IBL
{
    public partial class BL : IBL
    {
        /// <summary>
        /// The function gives associate date to the parcel.
        /// </summary>
        /// <param name="parcelId">parcel id</param>
        /// <param name="droneId">drone id</param>
        public void AssociateParcel(int parcelId, int droneId)
        {
            List<Parcel> ParcelsList = (List<Parcel>)dal.GetParcelsList();
            List<Drone> DronesList = (List<Drone>)dal.GetDronesList();
            if(ParcelsList.FindIndex(item => item.Id == parcelId) ==-1 || DronesList.FindIndex(item=>item.Id == droneId) == -1)
                new OverloadException("parcelId or droneId don't exist!");
            int parcelIndex = ParcelsList.FindIndex(item => item.Id == parcelId);
            int droneIndex = DronesList.FindIndex(item => item.Id == droneId);
            Parcel parcel = ParcelsList.First(item => item.Id == parcelId);
            Drone drone = DronesList.First(item => item.Id == parcelId);
            parcel.AssociationDate = DateTime.Now;
            parcel.MyDrone = new DroneInParcel { Id = droneId };
            ParcelsList[parcelIndex] = parcel;
            DronesList[droneIndex] = drone;
        }

        /// <summary>
        /// The function gives pick up date to the parcel.
        /// </summary>
        /// <param name="parcelId">parcel id</param>
        /// <param name="senderId">sender id</param>
        public void PickUpParcel(int parcelId, string senderId)
        {
            List<Parcel> parcelsList = (List<Parcel>)dal.GetParcelsList();
            List<Customer> customersList = (List<Customer>)dal.GetCustomersList();
            if (parcelsList.FindIndex(item => item.Id == parcelId) == -1 || customersList.FindIndex(item => item.Id == senderId) == -1)
                throw new OverloadException("parcelId or senderId don't exist in the customers' list!");
            Parcel parcel = parcelsList.First(item => item.Id == parcelId);
            int parcelIndex = parcelsList.FindIndex(item => item.Id == parcelId);
            parcel.Sender = new CustomerInParcel{ Id = senderId};
            parcel.PickUpDate = DateTime.Now;
            parcelsList[parcelIndex] = parcel;
        }

        /// <summary>
        /// The function gives arrival date to the parcel.
        /// </summary>
        /// <param name="parcelId">parcel id</param>
        /// <param name="targetId">target id</param>
        public void SupplyParcel(int parcelId, string targetId)
        {
            List<Parcel> ParcelsList = (List<Parcel>)dal.GetParcelsList();
            List<Customer> CustomersList = (List<Customer>)dal.GetCustomersList();
            if (ParcelsList.FindIndex(item => item.Id == parcelId) == -1 || CustomersList.FindIndex(item=>item.Id == targetId) == -1)
                throw new OverloadException("parcelId or targetId don't exist!");
            Parcel parcel = ParcelsList.First(item => item.Id == parcelId);
            int parcelIndex = ParcelsList.FindIndex(item => item.Id == parcelId);
            parcel.SupplyDate = DateTime.Now;
            ParcelsList[parcelIndex] = parcel;
        }

        /// <summary>
        /// the functuin trys to charge the drone.
        /// </summary>
        /// <param name="droneId">drone's id</param>
        /// <param name="baseStationId">base station's id</param>
        public  void ChargingDrone(int droneId, int baseStationId)
        {
            inputIntValue(ref droneId);
            List<Drone> DronesList = (List<Drone>)dal.GetDronesList();
            if (DronesList.FindIndex(item => item.Id == droneId) == -1)
                throw new OverloadException("drone's id doesn't exist in the drones' list.");
            Drone  drone = DronesList.First(item => item.Id == droneId);
            int droneIndex = DronesList.FindIndex(item => item.Id == droneId);

            inputIntValue(ref baseStationId);
            List<BaseStation> BaseStationsList = (List<BaseStation>)dal.GetBaseStationsList();
            if (BaseStationsList.FindIndex(item => item.Id == baseStationId) == -1)
                throw new OverloadException("drone's id doesn't exist in the drones' list.");
            BaseStation baseStation = BaseStationsList.First(item => item.Id == baseStationId);
            int baseStationIndex = BaseStationsList.FindIndex(item => item.Id == baseStationId);
            if(baseStation.ChargeSlots == 0)      
                throw new OverloadException("The chosen base station isn't available to charge the drone.");

            DroneInCharging droneCharge = new DroneInCharging(baseStationId, droneId);
            List<DroneInCharging> dronesChargeList = (List<DroneInCharging>)dal.GetDronesCharge();
            dronesChargeList.Add(droneCharge);

            DronesList[droneIndex] = drone;
            BaseStationsList[baseStationIndex] = baseStation;
        }

        /// <summary>
        /// the function stops the drone from charging
        /// </summary>
        /// <param name="droneId">drone's id</param>
        public void ReleaseDroneCharging(int droneId)
        {
            List<Drone> dronesList = (List<Drone>)dal.GetDronesList();
            List<DroneInCharging> DroneChargeList = (List<DroneInCharging>)dal.GetDronesCharge();
            List<BaseStation> baseStationsList = (List<BaseStation>)dal.GetBaseStationsList();
            int baseStationId;
            inputIntValue(ref droneId);
            if (dronesList.FindIndex(item => item.Id == droneId) == -1)
                throw new OverloadException("drone's id doesn't exist in the drones' list.");
            if (DroneChargeList.FindIndex(item => item.Id == droneId) == -1)
                throw new OverloadException("drone's id doesn't exist in the dronecharge list.");

            Drone drone = dronesList.First(item => item.Id == droneId);
            int droneIndex = dronesList.FindIndex(item => item.Id == droneId);
            DroneInCharging droneCharge = DroneChargeList.First(item => item.Id == droneId);    
            //baseStationId =baseStationsList.First(item=>item. droneCharge.;
            //int baseStationIndex = baseStationsList.FindIndex(item => item.Id == baseStationId);
            //if (baseStationIndex  == -1)
            //    throw new OverloadException("baseStation's id doesn't exist in the BaseStation's list.");
            //BaseStation baseStation = baseStationsList.First(item => item.Id == baseStationId);
            //DroneChargeList.Remove(droneCharge);
            //baseStation.ChargeSlots++;
            //baseStationsList[baseStationIndex] = baseStation;
            //dronesList[droneIndex] = drone;
        }
    }
}
