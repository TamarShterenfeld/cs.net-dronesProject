using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO;
using System.Runtime.CompilerServices;
using Singleton;

namespace IBL
{
    /// <summary>
    /// The class BL is the business logic level 
    /// which has the responsibility of pull & calaulating lists, object etc. from the DAL logic level
    /// the pulling of data from the DAL logic level is done by an IDal object - a field in the BL class.
    /// </summary>
    sealed partial class BL : Singleton<BL>, BLApi.IBL
    {
        //the single object which has the responsibility of pulling the data from the DAl logic level.
        internal DalApi.IDal dal;
        readonly List<DroneForList> dronesForList;
        readonly double electricityConsumingOfAvailable;
        readonly double electricityConsumingOfLightWeight;
        readonly double electricityConsumingOfHeavyWeight;
        readonly double electricityConsumingOfAverageWeight;
        readonly double chargeRate;
        BL()
        {
            dal = DalApi.DalFactory.GetDal();
            lock (dal)
            {
                dronesForList = (List<DroneForList>)InitDroneForList();
                double[] droneElectricityInfo = dal.ElectricityConsuming();
                electricityConsumingOfAvailable = droneElectricityInfo[0];
                electricityConsumingOfLightWeight = droneElectricityInfo[1];
                electricityConsumingOfAverageWeight = droneElectricityInfo[2];
                electricityConsumingOfHeavyWeight = droneElectricityInfo[3];
                chargeRate = droneElectricityInfo[4];
                Random rand = new();
                for (int i = 0; i < dronesForList.Count; i++)
                {
                    parcelForDrone(dronesForList[i]);
                }

            }

        }
        void parcelForDrone(DroneForList drone)
        {
            BO.Parcel parcelOfDrone = GetBLParcel(drone.ParcelId);
            //the parcel hasn't been supplied.
            if (parcelOfDrone.SupplyDate == null &&
                drone.Status == BO.DroneStatuses.Shipment)
            {

                //the parcel has been accosiated and hasn't been picked up.
                if (parcelOfDrone.AssociationDate == null &&
                    parcelOfDrone.PickUpDate != null)
                {
                    drone.Location = NearestBaseStation(drone, (List<BO.BaseStation>)GetBOBaseStationsList()).Location;
                }

                //the parcel has been picked hasn't been supplied - (in the general condition)
                else
                {
                    drone.Location = GetBOCustomersList().First(item => GetBLParcel(drone.ParcelId).Sender.Id == item.Id).Location;
                }

                double minBattery = ComputeMinBatteryNeeded(drone, GetBLCustomer(GetBLParcel(drone.ParcelId).Target.Id));
                if (minBattery != -1)
                {
                    drone.Battery = RandomBattery(minBattery);
                }
            }
            else
            {
                DroneStatuses status = (BO.DroneStatuses)rand.Next(1, 3);
                if (status == DroneStatuses.Maintenance)
                {
                    List<BO.BaseStation> baseStations = GetBOBaseStationsList().ToList();
                    BO.BaseStation baseStation = baseStations[rand.Next(0, baseStations.Count - 1)];
                    if (baseStation.ChargeSlots > 0)
                    {
                        drone.Status = DroneStatuses.Maintenance;
                        dal.SendDroneToRecharge(drone.Id, baseStation.Id);
                    }
                    else
                    {
                        drone.Status = DroneStatuses.Available;
                    }
                }
                else
                {
                    drone.Status = DroneStatuses.Available;
                }

                List<BO.BaseStation> baseStationList = (List<BO.BaseStation>)GetBOBaseStationsList();
                List<BO.Customer> customersList = (List<BO.Customer>)GetBOCustomersList();
                switch (drone.Status)
                {
                    case BO.DroneStatuses.Available:
                        {
                            List<CustomerForList> customerForLists = (List<CustomerForList>)CustomersWithSuppliedParcels();
                            if (customerForLists.Count != 0)
                            {
                                int index = rand.Next(0, customerForLists.Count - 1);
                                BO.Customer chosenCustomer = customersList.First(item => item.Id == customerForLists[index].Id);
                                drone.Location = chosenCustomer.Location;
                                double minBattery = ComputeMinBatteryNeeded(drone, chosenCustomer);
                                drone.Battery = RandomBattery(minBattery);
                            }
                            break;
                        }

                    case BO.DroneStatuses.Maintenance:
                        {
                            drone.Location = baseStationList[rand.Next(0, baseStationList.Count - 1)].Location;
                            drone.Battery = rand.Next(0, 20);
                            break;
                        }
                }
            }

        }
    }
}
