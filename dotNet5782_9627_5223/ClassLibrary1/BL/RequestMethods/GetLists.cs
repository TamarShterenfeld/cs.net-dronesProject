using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO;
using System.Runtime.CompilerServices;

using IBL;


namespace IBL
{
    public partial class BL
    {
        //a static random field - for general use.
         static readonly Random rand = new();
        //---------------------------------BaseStation GetList methods------------------------------------------------

        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<BO.BaseStation> GetBOBaseStationsList()
        {
            List<BO.BaseStation> boBaseStationList = new();
            List<DO.BaseStation> doBaseStationList = (List<DO.BaseStation>)dal.GetBaseStationsList();
            foreach (DO.BaseStation item in doBaseStationList)
            {
                boBaseStationList.Add(GetBLBaseStation(item.Id));
            }
            return boBaseStationList;
        }
        
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<BaseStationForList> GetAvailableChargeSlots()
        {
            List<DO.BaseStation> DoAvailableChargeSlots = dal.AvailableChargeStations(station => station.ChargeSlots - dal.CaughtChargeSlots(station.Id) > 0).ToList();
            List<BaseStationForList> BoAvailableChargeSlots = new();
            foreach (DO.BaseStation item in DoAvailableChargeSlots)
            {
                BoAvailableChargeSlots.Add(GetBaseStationForList(item.Id));
            }

            return BoAvailableChargeSlots;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<BaseStationForList> GetBaseStationList()
        {
            List<BaseStationForList> baseStationForLists = new();
            List<BO.BaseStation> baseStations = (List<BO.BaseStation>)GetBOBaseStationsList();
            foreach (BO.BaseStation item in baseStations)
            {
                baseStationForLists.Add(GetBaseStationForList(item.Id));
            }
            return baseStationForLists;
        }

        //---------------------------------Drones GetList Methods------------------------------------------------
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<DroneInCharging> GetDronesInMe(int stationId)
        {
            List<DroneInCharging> droneInCharging = new();
            if (dal.DronesChargingInMe(drone => drone.StationId == stationId ).ToList() != null)
            {   
                foreach (DO.DroneCharge droneCharge in dal.DronesChargingInMe(drone => drone.StationId == stationId).ToList())
                {
                    DroneInCharging drone = new(droneCharge.DroneId, rand.NextDouble()*40+60);
                    droneInCharging.Add(drone);
                }    
            }
            return droneInCharging;
        }


        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<BO.Drone> GetBODronesList()
        {
            List<BO.Drone> BoDronesList = new();
            List<DO.Drone> DoDronesList = (List<DO.Drone>)dal.GetDronesList();
            foreach (var item in DoDronesList)
            {
                BoDronesList.Add(GetBLDrone(item.Id));
            }
            return BoDronesList;
        }

        //---------------------------------DronesForList GetList Methods------------------------------------------------
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<DroneForList> InitDroneForList()
        {
            List<DroneForList> droneForList = new();
            DroneForList singleDrone;
            int parcelId = 0;
            foreach (var drone in dal.GetDronesList())
            {
                singleDrone = ConvertDroneDoToDroneForList(drone);
                singleDrone.ParcelId = ++parcelId <= dal.GetLastParcelId()? parcelId:0;               
                Coordinate coorLongitude = new Coordinate(rand.Next(0, 180) * 0.6 + rand.Next(-180, 0) * 0.4, Locations.Longitude);
                Coordinate coorLatitude = new Coordinate(rand.Next(0, 180) * 0.4 + rand.Next(-180, 0) * 0.6, Locations.Latitude);
                singleDrone.Location = new Location(coorLongitude, coorLatitude);

                droneForList.Add(singleDrone);
            }
            return droneForList;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<DroneForList> GetDronesForList()
        {
            return dronesForList;
        }

        //public IEnumerable<DroneForList> GetStatusFilteredDroneForList(DroneStatuses status)
        //{
        //    List<DroneForList> dronesForList = (List<DroneForList>)GetDronesForList();
        //    List<DroneForList> drones = new();
        //    drones = dronesForList.FindAll(item => item.Status == status);
        //    return drones;
        //}

        //public IEnumerable<DroneForList> GetWeightFilteredDroneForList(WeightCategories weight)
        //{
        //    List<DroneForList> dronesForList = (List<DroneForList>)GetDronesForList();
        //    List<DroneForList> drones = new();
        //    drones = dronesForList.FindAll(item => item.MaxWeight == weight);
        //    return drones;
        //}

        // ---------------------------------Parcels GetList Methods------------------------------------------------

        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<BO.Parcel> GetBOParcelsList()
        {
            List<BO.Parcel> boParcelList = new ();
            List<DO.Parcel> doParcelList = (List<DO.Parcel>)dal.GetParcelsList();
            foreach (DO.Parcel item in doParcelList)
            {
                boParcelList.Add(GetBLParcel(item.Id));
            }
            return boParcelList;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<ParcelInCustomer> GetParcelInCustomerList(FromOrTo fromOrTo, string id)
        {
            List<ParcelInCustomer> parcelInCustomer = new();
            List<DO.Parcel> ParcelList = dal.Parcels( parcel => (fromOrTo == FromOrTo.From && parcel.SenderId == id) || (fromOrTo == FromOrTo.To && parcel.TargetId == id)).ToList();
            foreach (DO.Parcel parcel in ParcelList)
            {
                parcelInCustomer.Add(ConvertParcelDoToParcelInCustomer(parcel, fromOrTo));
            }
            return parcelInCustomer;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<ParcelForList> GetParcelsList()
        {
            List<ParcelForList> parcelsForList = new();
            List<BO.Parcel> parcels = (List<BO.Parcel>)GetBOParcelsList();

            foreach (BO.Parcel item in parcels)
            {
                parcelsForList.Add(GetParcelForList(item.Id));
            }
            return parcelsForList;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<ParcelForList> GetNotAssociatedParcelsList()
        {
            List<ParcelForList> boNotAssociatedParcelsList = new ();
            List<DO.Parcel> doNotAccosiatedParcelsList = dal.Parcels(parcel=> parcel.AssociationDate == null).ToList();
            foreach (DO.Parcel item in doNotAccosiatedParcelsList)
            {
                boNotAssociatedParcelsList.Add(GetParcelForList(item.Id));
            }
            return boNotAssociatedParcelsList;
        }



        //---------------------------------Customers GetList Methods------------------------------------------------
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<BO.Customer> GetBOCustomersList()
        {
            List<BO.Customer> boCustomerList = new();
            List<DO.Customer> doCustomerList = (List<DO.Customer>)dal.GetCustomersList();
            foreach (DO.Customer item in doCustomerList)
            {
                boCustomerList.Add(GetBLCustomer(item.Id));
            }
            return boCustomerList;
        }
       
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<CustomerForList> GetCustomersList()
        {
            List<CustomerForList> customersForList = new();
            List<BO.Customer> customers = (List<BO.Customer>)GetBOCustomersList();
            foreach (BO.Customer item in customers)
            {      
                customersForList.Add(GetCustomerForList(item.Id));
            }
            return customersForList;
        }       
    }
}

    

