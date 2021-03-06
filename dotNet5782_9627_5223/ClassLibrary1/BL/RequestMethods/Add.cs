
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Runtime.CompilerServices;



namespace IBL
{

    public partial class BL
    {
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void Add(BO.BaseStation baseStation)
        {
            lock (dal)
            {
                dal.Add(new DO.BaseStation()
                {
                    Id = baseStation.Id,
                    Name = baseStation.Name,
                    Longitude = CoordinateBoToDo(baseStation.Location.CoorLongitude),
                    Latitude = CoordinateBoToDo(baseStation.Location.CoorLatitude),
                    ChargeSlots = baseStation.ChargeSlots,
                    IsDeleted = baseStation.IsDeleted
                });
            }

        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void Add(BO.Drone drone, int baseStationId)
        {
            lock (dal)
            {
                DO.Drone drone1 = new()
                {
                    Id = drone.Id,
                    MaxWeight = (DO.WeightCategories)drone.MaxWeight,
                    Model = drone.Model,
                    IsDeleted = drone.IsDeleted,
                };
                dal.Add(drone1);
                dronesForList.Add(ConvertDroneBoToDroneForList(drone));
            }

        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void Add(BO.Customer customer)
        {
            lock (dal)
            {
                dal.Add(new DO.Customer()
                {
                    Id = customer.Id,
                    Name = customer.Name,
                    Phone = customer.Phone,
                    Longitude = CoordinateBoToDo(customer.Location.CoorLongitude),
                    Latitude = CoordinateBoToDo(customer.Location.CoorLatitude),
                    IsDeleted = customer.IsDeleted
                });
            }
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void Add(BO.Parcel parcel)
        {
            lock (dal)
            {
                dal.Add(new DO.Parcel()
                {
                    Id = parcel.Id,
                    SenderId = parcel.Sender.Id,
                    TargetId = parcel.Target.Id,
                    DroneId = 0,
                    Weight = (DO.WeightCategories)parcel.Weight,
                    Priority = (DO.Priorities)parcel.Priority,
                    ProductionDate = parcel.ProductionDate,
                    AssociationDate = parcel.AssociationDate,
                    PickUpDate = parcel.PickUpDate,
                    SupplyDate = parcel.SupplyDate,
                    IsDeleted = parcel.IsDeleted,
                });
            }         
        }
    }
}
