using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IBL.BO;
using IDal.DO;
using static System.Math;


namespace IBL
{

    /// <summary>
    /// The class BL is the business logic level 
    /// which has the responsibility of pull & calaulating lists, object etc. from the DAL logic level
    /// the pulling of data from the DAL logic level is done by an IDal object - a field in the BL class.
    /// </summary>
    public partial class BL : IBL
    {
        private IDal.IDal dal;
        private List<DroneForList> dronesForList;
        double electricityConsumingOfAvailable;
        double electricityConsumingOfLightWeight;
        double electricityConsumingOfHeavyWeight;
        double electricityConsumingOfAverageWeight;
        double chargeRate;


        public List<DroneForList> DronesForList
        {
            get
            {
                return dronesForList;
            }
            set
            {
                dronesForList = value;
            }

        }

        public BL()
        {
            dal = new DalObject.DalObject();
            double[] droneElectricityInfo = dal.ElectricityConsuming();
            electricityConsumingOfAvailable = droneElectricityInfo[0];
            electricityConsumingOfLightWeight = droneElectricityInfo[1];
            electricityConsumingOfAverageWeight = droneElectricityInfo[2];
            electricityConsumingOfHeavyWeight = droneElectricityInfo[3];
            chargeRate = droneElectricityInfo[0];
            List<BO.Drone> drones = (List<BO.Drone>)GetDronesList();
            Random rand = new Random();

            for (int i = 0; i < drones.Count; i++)
            {

                BO.Parcel parcelOfDrone = GetBLParcel(drones[i].Parcel.Id);
                //the parcel hasn't been supplied.
                if (parcelOfDrone.SupplyDate == new DateTime(01 / 01 / 0001) &&
                    drones[i].Status == BO.DroneStatuses.Shipment)
                {
                    drones[i].Status = BO.DroneStatuses.Shipment;

                    //the parcel has been accosiated and hasn't been picked up.
                    if (parcelOfDrone.AssociationDate == new DateTime(01 / 01 / 0001) &&
                        parcelOfDrone.PickUpDate != new DateTime(01 / 01 / 0001))
                    {
                        drones[i].Location = nearestBaseStation(drones[i]).Location;
                    }

                    //the parcel has been picked hasn't been supplied - (in the general condition)
                    else
                    {
                        drones[i].Location = GetCustomersList().First(item => drones[i].Parcel.Sender.Id == item.Id).Location;
                    }

                    double minBattery = ComputeMinBatteryNeeded(drones[i], GetBLCustomer(drones[i].Parcel.Target.Id));
                    if(minBattery != -1)
                    {
                        drones[i].Battery = RandomBattery(minBattery);
                    }
                }
                else
                {
                    drones[i].Status = (BO.DroneStatuses)rand.Next(1, 3);
                    List<BO.BaseStation> baseStationList = (List<BO.BaseStation>)GetBaseStationsList();
                    List<BO.Customer> customersList = (List<BO.Customer>)GetCustomersList();
                    switch (drones[i].Status)
                    {
                        case BO.DroneStatuses.Available:
                            {
                                List<BO.CustomerForList> customerForLists = (List<CustomerForList>)CustomersWithSuppliedParcels();
                                if (customerForLists.Count != 0)
                                {
                                    BO.Customer chosenCustomer = customersList.First(item => item.Id == customerForLists[rand.Next(0, customerForLists.Count - 1)].Id);
                                    drones[i].Location = chosenCustomer.Location;
                                    double minBattery = ComputeMinBatteryNeeded(drones[i], chosenCustomer);
                                    drones[i].Battery = RandomBattery(minBattery);
                                }
                                break;
                            }

                        case BO.DroneStatuses.Maintenance:
                            {
                                drones[i].Location = baseStationList[rand.Next(0, baseStationList.Count - 1)].Location;
                                drones[i].Battery = rand.Next(0, 20);
                                break;
                            }
                    }
                }
            }
        }






        /// <summary>
        /// the function returns the nearest BaseStation to the input drone.
        /// </summary>
        /// <param name="drone"></param>
        /// <returns></returns>

        private BO.BaseStation nearestBaseStation(BO.Drone drone)
        {
            double minDistance = int.MaxValue;
            BO.BaseStation nearestBaseStation =
                       GetBaseStationsList().Last
                       (item => Min(item.Distance(drone), minDistance) == item.Distance(drone));
            return nearestBaseStation;
        }

        /// <summary>
        /// the function computes the min battery that is needed
        /// to the drone for reaching to the location of an ILocatable location
        /// the calculation is depended the distance + the drone's status.
        /// </summary>
        /// <param name="drone"></param>
        /// <param name="Ilocatable"></param>
        /// <returns></returns>
        private double ComputeMinBatteryNeeded(BO.Drone drone, ILocatable Ilocatable)
        {
            double distance = drone.Distance(Ilocatable);

            if (drone.Status == BO.DroneStatuses.Available)
            {
                return distance * electricityConsumingOfAvailable;
            }
            else
            {
                switch (drone.MaxWeight)
                {
                    case BO.WeightCategories.Light:
                        {
                            return electricityConsumingOfLightWeight * distance;
                        }
                    case BO.WeightCategories.Average:
                        {
                            return electricityConsumingOfAverageWeight * distance;
                        }
                    case BO.WeightCategories.Heavy:
                        {
                            return electricityConsumingOfHeavyWeight * distance;
                        }
                }

                //the distance wasn't succeeded to be computed.
                return -1;
            }
        }

        /// <summary>
        /// the function gets a double minBattery value,
        /// it rounds the value (if there is a need) and random a value between it to 100.
        /// </summary>
        /// <param name="minBattery"></param>
        /// <returns></returns>
        int RandomBattery(double minBattery)
        {
            Random rand = new Random();
            //check if the minBattery holds a real double value
            //for it's not possible to random a double number - we will increase the minBattery in one.
            if ((int)minBattery != minBattery)
            {
                minBattery++;
            }
            return rand.Next((int)minBattery, 100);
        }

        /// <summary>
        /// the function returns a list of all the customers that has supplied at least one parcel
        /// </summary>
        /// <returns></returns>
        private IEnumerable<BO.CustomerForList> CustomersWithSuppliedParcels()
        {
            List<BO.CustomerForList> customers = (List<BO.CustomerForList>)GetCustomersList();
            List<BO.CustomerForList> customerWithSuppliedParcels = new List<BO.CustomerForList>();
            foreach (BO.CustomerForList item in customers)
            {
                if (item.AmountOfSendAndSuppliedParcels > 0)
                {
                    customerWithSuppliedParcels.Add(item);
                }
            }
            return customerWithSuppliedParcels;
        }
    }
}
