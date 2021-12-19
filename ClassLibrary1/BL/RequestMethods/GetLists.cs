using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IBL.BO;

using IBL;


namespace IBL
{
    public partial class BL: IBL
    {
        //a static random field - for general use.
        public static readonly Random rand = new();
        //---------------------------------BaseStation GetList methods------------------------------------------------
        public IEnumerable<BO.BaseStation> GetBOBaseStationsList()
        {
            List<BO.BaseStation> boBaseStationList = new();
            List<IDal.DO.BaseStation> doBaseStationList = (List<IDal.DO.BaseStation>)dal.GetBaseStationsList();
            foreach (IDal.DO.BaseStation item in doBaseStationList)
            {
                boBaseStationList.Add(GetBLBaseStation(item.Id));
            }
            return boBaseStationList;
        }
        public IEnumerable<BaseStationForList> GetAvailableChargeSlots()
        {
            List<IDal.DO.BaseStation> DoAvailableChargeSlots = dal.AvailableChargeStations(station => station.ChargeSlots - dal.CaughtChargeSlots(station.Id) > 0).ToList();
            List<BaseStationForList> BoAvailableChargeSlots = new();
            foreach (IDal.DO.BaseStation item in DoAvailableChargeSlots)
            {
                BoAvailableChargeSlots.Add(GetBaseStationForList(item.Id));
            }

            return BoAvailableChargeSlots;
        }

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
        public IEnumerable<DroneInCharging> GetDronesInMe(int stationId)
        {
            List<DroneInCharging> droneInCharging = new();
            if (dal.DronesChargingInMe(drone => drone.StationId == stationId ).ToList() != null)
            {   
                foreach (IDal.DO.DroneCharge droneCharge in dal.DronesChargingInMe(drone => drone.StationId == stationId).ToList())
                {
                    DroneInCharging drone = new(droneCharge.DroneId, rand.NextDouble()*40+60);
                    droneInCharging.Add(drone);
                }    
            }
            return droneInCharging;
        }

        

        public IEnumerable<BO.Drone> GetBODronesList()
        {
            List<BO.Drone> BoDronesList = new();
            List<IDal.DO.Drone> DoDronesList = (List<IDal.DO.Drone>)dal.GetDronesList();
            foreach (var item in DoDronesList)
            {
                BoDronesList.Add(GetBLDrone(item.Id));
            }
            return BoDronesList;
        }

        //---------------------------------DronesForList GetList Methods------------------------------------------------
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
  
        public IEnumerable<DroneForList> GetDronesForList()
        {
            return dronesForList;
        }

        public IEnumerable<DroneForList> GetStatusFilteredDroneForList(DroneStatuses status)
        {
            List<DroneForList> dronesForList = (List<DroneForList>)GetDronesForList();
            List<DroneForList> drones = new();
            drones = dronesForList.FindAll(item => item.Status == status);
            return drones;
        }

        public IEnumerable<DroneForList> GetWeightFilteredDroneForList(WeightCategories weight)
        {
            List<DroneForList> dronesForList = (List<DroneForList>)GetDronesForList();
            List<DroneForList> drones = new();
            drones = dronesForList.FindAll(item => item.MaxWeight == weight);
            return drones;
        }

        // ---------------------------------Parcels GetList Methods------------------------------------------------

        public IEnumerable<BO.Parcel> GetBOParcelsList()
        {
            List<BO.Parcel> boParcelList = new ();
            List<IDal.DO.Parcel> doParcelList = (List<IDal.DO.Parcel>)dal.GetParcelsList();
            foreach (IDal.DO.Parcel item in doParcelList)
            {
                boParcelList.Add(GetBLParcel(item.Id));
            }
            return boParcelList;
        }

        public IEnumerable<ParcelInCustomer> GetParcelInCustomerList(FromOrTo fromOrTo, string id)
        {
            List<ParcelInCustomer> parcelInCustomer = new();
            List<IDal.DO.Parcel> ParcelList = dal.Parcels( parcel => (fromOrTo == FromOrTo.From && parcel.SenderId == id) || (fromOrTo == FromOrTo.To && parcel.TargetId == id)).ToList();
            foreach (IDal.DO.Parcel parcel in ParcelList)
            {
                parcelInCustomer.Add(ConvertParcelDoToParcelInCustomer(parcel, fromOrTo));
            }
            return parcelInCustomer;
        }
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

        public IEnumerable<ParcelForList> GetNotAssociatedParcelsList()
        {
            List<ParcelForList> boNotAssociatedParcelsList = new ();
            List<IDal.DO.Parcel> doNotAccosiatedParcelsList = dal.Parcels(parcel=> parcel.AssociationDate == null).ToList();
            foreach (IDal.DO.Parcel item in doNotAccosiatedParcelsList)
            {
                boNotAssociatedParcelsList.Add(GetParcelForList(item.Id));
            }
            return boNotAssociatedParcelsList;
        }

        

        //---------------------------------Customers GetList Methods------------------------------------------------
        public IEnumerable<BO.Customer> GetBOCustomersList()
        {
            List<BO.Customer> boCustomerList = new();
            List<IDal.DO.Customer> doCustomerList = (List<IDal.DO.Customer>)dal.GetCustomersList();
            foreach (IDal.DO.Customer item in doCustomerList)
            {
                boCustomerList.Add(GetBLCustomer(item.Id));
            }
            return boCustomerList;
        }
       
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

    

