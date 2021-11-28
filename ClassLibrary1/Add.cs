using IDal.DO;
using System;
using System.Collections.Generic;
using System.Text;
using static DalObject.DataSource;
using System.Linq;
using  DalObject;
using IBL.BO;



namespace IBL
{
    public partial class BL : IBL
    {
        // < inheritdoc  />
        public void Add(BO.BaseStation baseStation)
        {
            dal.Add(new IDal.DO.BaseStation() { Id = baseStation.Id, Name = baseStation.Name, Longitude = CoordinateBoToDo(baseStation.Location.CoorLongitude), Latitude = CoordinateBoToDo(baseStation.Location.CoorLatitude), ChargeSlots = baseStation.ChargeSlots });

        }

        // < inheritdoc  />
        public void Add(BO.Drone drone, int baseStationId)
        {
            IDal.DO.Drone drone1 = new() { Id = drone.Id, MaxWeight = (IDal.DO.WeightCategories)drone.MaxWeight , Model = drone.Model};
            dal.Add(drone1);
            dronesForList.Add(GetDroneForList(drone));
        }


        public void Add(BO.Customer customer)
        {
            dal.Add(new IDal.DO.Customer() { Id = customer.Id, Name = customer.Name, Phone = customer.Phone,Longitude = CoordinateBoToDo(customer.Location.CoorLongitude) , Latitude = CoordinateBoToDo(customer.Location.CoorLatitude) }); 
        }


        public void Add(BO.Parcel parcel)
        {
            dal.Add(new IDal.DO.Parcel() { Id = parcel.Id, SenderId = parcel.Sender.Id, TargetId = parcel.Target.Id, DroneId = 0 , Weight = (IDal.DO.WeightCategories)parcel.Weight, Priority = (IDal.DO.Priorities)parcel.Priority, ProductionDate = parcel.ProductionDate, AssociationDate = parcel.AssociationDate, PickUpDate = parcel.PickUpDate, SupplyDate = parcel.SupplyDate});
        }
    }
}
