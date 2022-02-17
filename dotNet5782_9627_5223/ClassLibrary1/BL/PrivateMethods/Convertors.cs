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
                Priority = (DO.Priorities)Enum.Parse(typeof(DO.Priorities), parcel.Priority.ToString()),
                SenderId = parcel.Sender.Id,
                TargetId = parcel.Target.Id,
                Weight = (DO.WeightCategories)Enum.Parse(typeof(DO.WeightCategories), parcel.Weight.ToString()),
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
                MaxWeight = (DO.WeightCategories)Enum.Parse(typeof(DO.WeightCategories), boDrone.MaxWeight.ToString()),
                Model = boDrone.Model,
                IsDeleted = boDrone.IsDeleted,
            };
            return doDrone;
        }

        /// <summary>
        /// the function converts a DroneForList object to a BO.Drone object.
        /// </summary>
        /// <param name="droneForList">the object to convert</param>
        /// <returns>a BO.Drone converted object</returns>
         Drone ConvertDroneForListToDrone(DroneForList drone)
         {
            ParcelInPassing parcel = new() { Id = drone.ParcelId };
            return new (drone.Id, drone.Model, drone.MaxWeight, drone.Battery, drone.Status, parcel, drone.Location,drone.IsDeleted );
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
        public IEnumerable<BaseStation> ConvertBaseStationsForListToBaseStation(List<BaseStationForList> baseStationForLists)
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
                Weight = (BO.WeightCategories)Enum.Parse(typeof(BO.WeightCategories), parcel.Weight.ToString()),
                Priority = (BO.Priorities)Enum.Parse(typeof(BO.Priorities), parcel.Priority.ToString()),
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
                IsDeleted = drone.IsDeleted,
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
                MaxWeight = (BO.WeightCategories)Enum.Parse(typeof(BO.WeightCategories), drone.MaxWeight.ToString()),
                Model = drone.Model,
                IsDeleted = drone.IsDeleted,
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
            DroneForList droneForList = dronesForList.FirstOrDefault(item => item.Id == drone.Id);
            return new(drone.Id, drone.Model,(BO.WeightCategories)drone.MaxWeight, droneForList.Battery, (BO.DroneStatuses)droneForList.Status, GetParcelInPassing(droneForList.ParcelId), droneForList.Location, drone.IsDeleted);
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

        /// <summary>
        /// the function converts a IDal.DO.Parcel object to a BO.Parcel object.
        /// </summary>
        /// <param name="parcel">the IDal.DO parcel object</param>
        /// <returns>a BO.Parcel object</returns>
        Parcel ParcelDOtOBO(DO.Parcel parcel)
        {
            Parcel BOParcel = new()
            {
                Id = parcel.Id,
                Sender = GetCustomrInParcel(parcel.SenderId),
                Target = GetCustomrInParcel(parcel.TargetId),
                Weight = (BO.WeightCategories)Enum.Parse(typeof(BO.WeightCategories), parcel.Weight.ToString()),
                Priority = (BO.Priorities)Enum.Parse(typeof(BO.Priorities), parcel.Priority.ToString()),
                MyDrone = GetBLDroneInParcel(parcel.DroneId),
                ProductionDate = parcel.ProductionDate,
                AssociationDate = parcel.AssociationDate,
                PickUpDate = parcel.PickUpDate,
                SupplyDate = parcel.SupplyDate
            };
            return BOParcel;
        }

        /// <summary>
        /// convert Coordinate object from BO to DO
        /// </summary>
        /// <param name="coor">BO coordinate</param>
        /// <returns>DO coordinate</returns>
        DO.Coordinate CoordinateBoToDo(BO.Coordinate coor)
        {
            return new DO.Coordinate() { InputCoorValue = coor.InputCoorValue, Degrees = coor.Degrees, Direction = (DO.Directions)Enum.Parse(typeof(DO.Directions), coor.Direction.ToString()), MyLocation = (DO.Locations)Enum.Parse(typeof(DO.Locations), coor.MyLocation.ToString()), Minutes = coor.Minutes, Seconds = coor.Seconds };
        }

        /// <summary>
        /// convert Coordinate object from DO to BO
        /// </summary>
        /// <param name="coor">DO coordinate</param>
        /// <returns>BO coordinate</returns>
        static BO.Coordinate CoordinateDoToBo(DO.Coordinate coor)
        {
            return new BO.Coordinate() { InputCoorValue = coor.InputCoorValue, Degrees = coor.Degrees, Direction = (BO.Directions)Enum.Parse(typeof(BO.Directions), coor.Direction.ToString()), MyLocation = (BO.Locations)Enum.Parse(typeof(BO.Locations), coor.MyLocation.ToString()), Minutes = coor.Minutes, Seconds = coor.Seconds };
        }

    }
}
