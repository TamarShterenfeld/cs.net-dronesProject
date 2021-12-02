using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IBL.BO;

namespace IBL
{
    public partial class BL : IBL
    {
        /// <summary>
        /// the function converts BO.Parcel object to DO.Parcel object.
        /// </summary>
        /// <param name="parcel">the object to convert</param>
        /// <returns>a DO.Parcel converted object</returns>
        static IDal.DO.Parcel ConvertBoToDoParcel(BO.Parcel parcel)
        {
            IDal.DO.Parcel doParcel = new()
            {
                Id = parcel.Id,
                DroneId = parcel.MyDrone.Id,
                Priority = (IDal.DO.Priorities)(parcel.Priority),
                SenderId = parcel.Sender.Id,
                TargetId = parcel.Target.Id,
                Weight = (IDal.DO.WeightCategories)(parcel.Weight)
            };
            return doParcel;
        }

        /// <summary>
        /// the function converts ParcelForList object to BO.Parcel object.
        /// </summary>
        /// <param name="parcel">the object to convert</param>
        /// <returns>a BO.Parcel converted object</returns>
        static Parcel ConvertParcelForListToParcel(ParcelForList parcelForList)
        {
            Parcel parcel = new()
            {
                Id = parcelForList.ParcelId,
                MyDrone = new()
                {
                    Id = parcelForList.DroneId
                },
                Priority = parcelForList.Priority,
                Sender = new()
                {
                    Id = parcelForList.SenderId
                },
                Target = new()
                {
                    Id = parcelForList.TargetId
                },
                Weight = parcelForList.Weight
            };
            return parcel;
        }

        /// <summary>
        /// the function converts a BO.Drone object to a DO.Drone object
        /// </summary>
        /// <param name="boDrone">the object to convert</param>
        /// <returns>a DO.Drone converted object</returns>
       static IDal.DO.Drone ConvertBoToDoDrone(BO.Drone boDrone)
        {
            IDal.DO.Drone doDrone = new()
            {
                Id = boDrone.Id,
                MaxWeight = (IDal.DO.WeightCategories)(boDrone.MaxWeight),
                Model = boDrone.Model,
            };
            return doDrone;
        }

        /// <summary>
        /// the function converts a DroneForList object to a BO.Drone object.
        /// </summary>
        /// <param name="droneForList">the object to convert</param>
        /// <returns>a BO.Drone converted object</returns>
        static Drone ConvertDroneForListToDrone(DroneForList droneForList)
        {
            ParcelInPassing parcel = new() { Id = droneForList.ParcelId };
            Drone drone = new (droneForList.Id, droneForList.Model, droneForList.MaxWeight, droneForList.Battery, droneForList.Status, parcel, droneForList.Location);
            return drone;
        }

        /// <summary>
        /// the function converts a Customer object to a CustomerInParcel object
        /// </summary>
        /// <param name="customer">the objext to convert</param>
        /// <returns>the converted Customer object</returns>
        public CustomerInParcel ConvertCustomerDoToCustomerInParcel(IDal.DO.Customer customer)
        {
            CustomerInParcel BOCustomrInParcel = new()
            {
                Id = customer.Id,
                Name = customer.Name
            };
            return BOCustomrInParcel;
        }

        /// <summary>
        /// the function converts a DO.Customer object to a BO.Customer object.
        /// </summary>
        /// <param name="customer">the object to convert</param>
        /// <returns>the converted BO.Customer object</returns>
        public Customer ConvertCustomerDoToBo(IDal.DO.Customer customer)
        {
            Customer BOCustomer = new()
            {
                Id = customer.Id,
                Name = customer.Name,
                Phone = customer.Phone,
                Location = new Location(CoordinateDoToBo(customer.Longitude), CoordinateDoToBo(customer.Latitude)),
                FromCustomer = (List<ParcelInCustomer>)GetParcelInCustomerList(FromOrTo.From, customer.Id),
                ToCustomer = (List<ParcelInCustomer>)GetParcelInCustomerList(FromOrTo.To, customer.Id),
            };
            return BOCustomer;
        }

        /// <summary>
        /// the function converts a list of BaseStationForList type to BaseStation type.
        /// </summary>
        /// <param name="baseStationForLists"></param>
        /// <returns></returns>
        IEnumerable<BaseStation> ConvertBaseStationsForListToBaseStation(List<BaseStationForList> baseStationForLists)
        {
            List<BaseStation> baseStations = new();
            foreach (BaseStationForList item in baseStationForLists)
            {
                baseStations.Add(GetBLBaseStation(item.Id));
            }
            return baseStations;
        }
        /// <summary>
        /// the function converts a DO.Parcel object to a ParcelInCustomer object.
        /// </summary>
        /// <param name="parcel">the parcel to convert</param>
        /// <param name="fromOrTo">an enum value - for filling the SourceOrDest field</param>
        /// <returns></returns>
        public ParcelInCustomer ConvertParcelDoToParcelInCustomer(IDal.DO.Parcel parcel, FromOrTo fromOrTo)
        {
            ParcelInCustomer BOCustomerInParcel = new()
            {
                Id = parcel.Id,
                Weight = (BO.WeightCategories)parcel.Weight,
                Priority = (BO.Priorities)parcel.Priority,
                ParcelStatus = ParcelStatus(parcel),
                SourceOrDest = fromOrTo == FromOrTo.From ? GetCustomrInParcel(parcel.SenderId) : GetCustomrInParcel(parcel.TargetId)
            };
            return BOCustomerInParcel;
        }


        /// <summary>
        /// the function converts a BO.Drone object to a DO.Drone object.
        /// </summary>
        /// <param name="drone">the object to convert</param>
        /// <returns>the converted DO.Drone object</returns>
        public DroneForList ConvertDroneBoToDroneForList(BO.Drone drone)
        {
            DroneForList current = new()
            {
                Id = drone.Id,
                MaxWeight = drone.MaxWeight,
                Model = drone.Model,
                Battery = drone.Battery,
                Status = drone.Status,
                Location = drone.Location,
                ParcelId = drone.Parcel != null ? drone.Parcel.Id : 0,
            };
            return current;
        }

        /// <summary>
        /// the function converts a DO.Drone object to a DroneForList object.
        /// </summary>
        /// <param name="drone">the object to convert</param>
        /// <returns>the converted DroneForList object</returns>
        public DroneForList ConvertDroneDoToDroneForList(IDal.DO.Drone drone)
        {
            DroneForList current = new()
            {
                Id = drone.Id,
                MaxWeight = (BO.WeightCategories)drone.MaxWeight,
                Model = drone.Model,

            };
            return current;
        }

        /// <summary>
        /// the function converts a DO.Drone object to a BO.Drone object
        /// </summary>
        /// <param name="drone">the object to convert</param>
        /// <returns>the converted BO.Drone object</returns>
        public Drone ConvertDroneDOtOBO(IDal.DO.Drone drone)
        {
            Drone bODrone = new (drone.Id, drone.Model, (WeightCategories)(drone.MaxWeight), 0, DroneStatuses.Available, new ParcelInPassing(), new Location(new Coordinate(10.1234, Locations.Longitude), new Coordinate(10.1234, Locations.Latitude)));
            return bODrone;
        }

        /// <summary>
        /// the function converts a DO.BaseStation object to BO.BaseStation object.
        /// </summary>
        /// <param name="baseStation"></param>
        /// <returns></returns>
        public BaseStation ConvertBaseStationDOtOBO(IDal.DO.BaseStation baseStation)
        {
            BaseStation BOBaseStation = new()
            {
                Id = baseStation.Id,
                Name = baseStation.Name,
                Location = new Location(CoordinateDoToBo(baseStation.Longitude), CoordinateDoToBo(baseStation.Latitude)),
                ChargeSlots = baseStation.ChargeSlots,
                DroneCharging = (List<DroneInCharging>)GetDronesInMe(baseStation.Id)
            };
            return BOBaseStation;
        }

    }
}
