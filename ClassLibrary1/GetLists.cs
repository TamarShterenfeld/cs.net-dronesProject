using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IBL.BO;
using IDal.DO;



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

        /// <summary>
        /// The function shows all the available charge slots
        /// </summary>
        public IEnumerable<BO.BaseStationForList> GetAvailableChargeSlots()
        {
            List<IDal.DO.BaseStation> DoAvailableChargeSlots = (List<IDal.DO.BaseStation>)dal.AvailableChargeStations();
            List<BO.BaseStationForList> BoAvailableChargeSlots = new();
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
            List<DroneInCharging> droneInCharging = null;
            if (dal.DronesChargingInMe(stationId) != null)
            {       foreach (DroneCharge droneCharge in dal.DronesChargingInMe(stationId))
                {
                    DroneInCharging drone = new(droneCharge.DroneId, 100);
                    // לבדוק שבאמת הבטריה נהית 100
                    droneInCharging.Add(drone);
                }    
            }
            return droneInCharging;
        } 

        /// <summary>
        /// The function returns all the DronesLIst items 
        /// (by converting the IDal.DO DronesLIst to BO DronesLIst)
        /// </summary>
        public IEnumerable<BO.Drone> GetBODronesList()
        {
            List<BO.Drone> BoDronesList = new();
            List<IDal.DO.Drone> DoDronesList = (List<IDal.DO.Drone>)dal.GetDronesList();
            foreach (var item in DoDronesList)
            {
                BoDronesList.Add(GetBLDroneFromBL(item.Id));
            }
            return BoDronesList;
        }


        /// <summary>
        /// the function returns a droneForList
        /// by converting the BO.Drone to DroneForList type.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<DroneForList> GetDronesList()
        {
            List<DroneForList> droneForLists = new();
            List<BO.Drone> drones = (List<BO.Drone>)GetBOCustomersList();

            foreach (BO.Drone item in drones)
            {

                droneForLists.Add(GetDroneForList(item.Id));
            }
            return droneForLists;
        }

        //---------------------------------DronesForList GetList Methods------------------------------------------------

        //public List<DroneForList> GetDroneForList(List<BO.Drone> drones)
        //{
        //    List<DroneForList> droneForLists = new();
        //    foreach (var drone in drones)
        //    {
        //        droneForLists.Add(GetOneDroneForList(drone));
        //    }
        //    return droneForLists;
        //}


        public IEnumerable<DroneForList> InitDroneForList()
        {
            List<DroneForList> droneForList = new();
            DroneForList singleDrone;
            int parcelId = 0;
            foreach (var drone in dal.GetDronesList())
            {
                singleDrone = GetDroneForList(drone);
                //singleDrone.Status = RandomPriority();
                //singleDrone.Location = new Location(RandomLongitude(), RandomLatitude());
                //singleDrone.Battery = rand.NextDouble() * 20 + 20;
                singleDrone.ParcelId = ++parcelId <= dal.getLastParcelId()? parcelId:0;
                droneForList.Add(singleDrone);

            }
            return droneForList;
        }

        public IEnumerable<DroneForList> GetDronesForLists()
        {
            return dronesForList;
        }



        // ---------------------------------Parcels GetList Methods------------------------------------------------

        /// <summary>
        /// The function returns all the ParcelsLIst items 
        /// (by converting the IDal.DO ParcelsLIst to BO ParcelsLIst)
        /// </summary>
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

        /// <summary>
        /// the function returns a ParcelForList list
        /// by converting the BO.Parcel PArcelForList  type.
        /// </summary>
        /// <returns></returns>
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


        /// <summary>
        /// The function shows all the not associated parcels 
        /// </summary>
        public IEnumerable<BO.ParcelForList> GetNotAssociatedParcelsList()
        {
            List<BO.ParcelForList> boNotAssociatedParcelsList = new ();
            List<IDal.DO.Parcel> doNotAccosiatedParcelsList = (List<IDal.DO.Parcel>)dal.NotAssociatedParcels();
            foreach (IDal.DO.Parcel item in doNotAccosiatedParcelsList)
            {
                boNotAssociatedParcelsList.Add(GetParcelForList(item.Id));
            }
            return boNotAssociatedParcelsList;
        }


        //---------------------------------Customers GetList Methods------------------------------------------------

        /// <summary>
        /// The function returns all the CustomersLIst items 
        /// (by converting the IDal.DO CustomersLIst to BO CustomersLIst)
        /// </summary>
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
       
        /// <summary>
        /// The function returns List of ParcelInCustomer items for sender or target
        /// </summary>
        /// <param name="fromOrTo"> sender or target </param>
        /// <returns> List of ParcelInCustomer items </returns>
        public IEnumerable<ParcelInCustomer> GetParcelInCustomerList(FromOrTo fromOrTo , string id)
        {
            List<ParcelInCustomer> parcelInCustomer = new ();
            List<IDal.DO.Parcel> ParcelList = (List<IDal.DO.Parcel>)dal.GetParcelsList();
            foreach (IDal.DO.Parcel parcel in ParcelList)
            {
                if((fromOrTo == FromOrTo.From && parcel.SenderId == id)  || (fromOrTo == FromOrTo.To && parcel.TargetId == id))
                {
                    parcelInCustomer.Add(ParcelInCustomerDOtOBO(parcel, fromOrTo));
                }
            }
            return parcelInCustomer;
        }

        /// <summary>
        /// the function returns a customerForLIst list
        /// by converting the BO.customero customerForLIst type.
        /// </summary>
        /// <returns></returns>
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

        public void UpdateBaseStation(string id, string name, string num)
        {
           // throw new NotImplementedException();
        }
    }
}

    

