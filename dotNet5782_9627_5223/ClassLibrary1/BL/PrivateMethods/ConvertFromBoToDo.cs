using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO;
using DalApi;
using System.Runtime.CompilerServices;


namespace IBL
{
    public partial class BL 
    {
        /// <summary>
        /// the function converts BO.Parcel object to DO.Parcel object.
        /// </summary>
        /// <param name="parcel">the object to convert</param>
        /// <returns>a DO.Parcel converted object</returns>
         DO.Parcel ConvertBoToDoParcel(BO.Parcel parcel)
        {
            DO.Parcel doParcel = new()
            {
                Id = parcel.Id,
                DroneId = parcel.MyDrone.Id,
                Priority = (DO.Priorities)(parcel.Priority),
                SenderId = parcel.Sender.Id,
                TargetId = parcel.Target.Id,
                Weight = (DO.WeightCategories)(parcel.Weight),
                ProductionDate = parcel.ProductionDate,
                AssociationDate = parcel.AssociationDate,
                PickUpDate = parcel.PickUpDate,
                SupplyDate = parcel.SupplyDate,
            };
            return doParcel;
        }

        /// <summary>
        /// the function converts ParcelForList object to BO.Parcel object.
        /// </summary>
        /// <param name="parcel">the object to convert</param>
        /// <returns>a BO.Parcel converted object</returns>
         Parcel ConvertParcelForListToParcel(ParcelForList parcelForList)
        {
            Parcel parcel = new()
            {

                Id = parcelForList.ParcelId,

                Priority = parcelForList.Priority,
                Sender = new()
                {
                    Id = parcelForList.SenderId
                },
                Target = new()
                {
                    Id = parcelForList.TargetId
                },
                Weight = parcelForList.Weight,
                MyDrone = new() { Id = parcelForList.DroneId}
            };

            switch (parcelForList.Status)
            {
                
                case ParcelStatuses.Associated:
                    {
                        parcel.AssociationDate = DateTime.Now;
                        break;
                    }
                case ParcelStatuses.PickedUp:
                    {
                        parcel.PickUpDate = DateTime.Now;
                        break;
                    }
                case ParcelStatuses.Supplied:
                    {
                        parcel.SupplyDate = DateTime.Now;
                        break;
                    }
            }
           
            return parcel;
        }

        /// <summary>
        /// the function converts a BO.Drone object to a DO.Drone object
        /// </summary>
        /// <param name="boDrone">the object to convert</param>
        /// <returns>a DO.Drone converted object</returns>
        DO.Drone ConvertBoToDoDrone(BO.Drone boDrone)
        {
            DO.Drone doDrone = new()
            {
                Id = boDrone.Id,
                MaxWeight = (DO.WeightCategories)boDrone.MaxWeight,
                Model = boDrone.Model,
            };
            return doDrone;
        }

        /// <summary>
        /// the function converts a DroneForList object to a BO.Drone object.
        /// </summary>
        /// <param name="droneForList">the object to convert</param>
        /// <returns>a BO.Drone converted object</returns>
         Drone ConvertDroneForListToDrone(DroneForList droneForList)
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
         CustomerInParcel ConvertCustomerDoToCustomerInParcel(DO.Customer customer)
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
        Customer ConvertCustomerDoToBo(DO.Customer customer)
        {
            Customer BOCustomer = new()
            {
                Id = customer.Id,
                Name = customer.Name,
                Phone = customer.Phone,
                Location = new Location(CoordinateDoToBo(customer.Longitude), CoordinateDoToBo(customer.Latitude)),
                FromCustomer = (List<ParcelInCustomer>)GetParcelInCustomerList(FromOrTo.From, customer.Id),
                ToCustomer = (List<ParcelInCustomer>)GetParcelInCustomerList(FromOrTo.To, customer.Id),
                IsDeleted = customer.IsDeleted
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
        ParcelInCustomer ConvertParcelDoToParcelInCustomer(DO.Parcel parcel, FromOrTo fromOrTo)
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
        DroneForList ConvertDroneBoToDroneForList(BO.Drone drone)
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
        DroneForList ConvertDroneDoToDroneForList(DO.Drone drone)
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
        Drone ConvertDroneDOtOBO(DO.Drone drone)
        {
            Drone bODrone = new (drone.Id, drone.Model, (WeightCategories)drone.MaxWeight,dronesForList.FirstOrDefault(item => item.Id == drone.Id).Battery, DroneStatuses.Available, ParcelInPassing.Empty, new Location());
            return bODrone;
        }

        /// <summary>
        /// the function converts a DO.BaseStation object to BO.BaseStation object.
        /// </summary>
        /// <param name="baseStation"></param>
        /// <returns></returns>
        BaseStation ConvertBaseStationDOtOBO(DO.BaseStation baseStation)
        {
            BaseStation BOBaseStation = new()
            {
                Id = baseStation.Id,
                Name = baseStation.Name,
                Location = new Location(CoordinateDoToBo(baseStation.Longitude), CoordinateDoToBo(baseStation.Latitude)),
                ChargeSlots = baseStation.ChargeSlots,
                DroneCharging = (List<DroneInCharging>)GetDronesInMe(baseStation.Id),
                IsDeleted = baseStation.IsDeleted 
            };
            return BOBaseStation;
        }

        /// <summary>
        /// the function converts a BO.BaseStation object to DO.BaseStation object.
        /// </summary>
        /// <param name="baseStation"></param>
        /// <returns></returns>
        DO.BaseStation ConvertBaseStationBOtODO(BO.BaseStation baseStation)
        {
            DO.BaseStation DOBaseStation = new()
            {
                Id = baseStation.Id,
                Name = baseStation.Name,
                ChargeSlots = baseStation.ChargeSlots,
                Longitude = CoordinateBoToDo(baseStation.Location.CoorLongitude),
                Latitude = CoordinateBoToDo(baseStation.Location.CoorLatitude),
                IsDeleted = baseStation.IsDeleted
            };
            return DOBaseStation;
        }

    }
}
