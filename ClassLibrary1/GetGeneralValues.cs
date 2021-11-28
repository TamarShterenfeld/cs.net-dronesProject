﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IBL.BO;
using IDal.DO;
using IDal;



namespace IBL
{
    public partial class BL : IBL
    {
        //----------------------------------Other GetObject Methods---------------------------------
        private double GetDroneBattery(int droneId)
        {
            return dronesForList.Find(drone => drone.Id == droneId).Battery;
        }

        private DroneStatuses GetDroneStatus(int droneId)
        {
            return dronesForList.Find(drone => drone.Id == droneId).Status;
        }

        private Location GetDroneLocation(int droneId)
        {
            return dronesForList.Find(drone => drone.Id == droneId).Location;
        }

        private int GetDroneParcelId(int droneId)
        {
            if(((List<BO.Parcel>)GetBOParcelsList()).FindIndex(parcel => { if (parcel.MyDrone != null && parcel.MyDrone.Id == droneId) return true; else return false; }) != -1)
            {
                return 0;
            }
            BO.Parcel parcel = ((List<BO.Parcel>)GetBOParcelsList()).First(parcel=> { if (parcel.MyDrone != null && parcel.MyDrone.Id == droneId) return true;else return false; });
            return parcel.Id;
        }


        private BO.Parcel ParcelDOtOBO(IDal.DO.Parcel parcel)
        {
            BO.Parcel BOParcel = new()
            {
                Id = parcel.Id,
                Sender = GetBLCustomrInParcel(parcel.SenderId),
                Target = GetBLCustomrInParcel(parcel.TargetId),
                Weight = (BO.WeightCategories)parcel.Weight,
                Priority = (BO.Priorities)parcel.Priority,
                MyDrone = GetBLDroneInParcel(parcel.DroneId),
                ProductionDate = parcel.ProductionDate,
                AssociationDate = parcel.AssociationDate,
                PickUpDate = parcel.PickUpDate,
                SupplyDate = parcel.SupplyDate
            };
            return BOParcel;
        }

        /// <summary>
        /// returns the number of caught charge slots in a specific base station
        /// </summary>
        /// <param name="stationId">station's id</param>
        /// <returns>the number of caught charge slots</returns>
        int CatchAvailableChargeSlots(int stationId)
        {
            int caught = dal.CaughtChargeSlots(stationId);
            return caught;
        }

        private IDal.DO.Coordinate CoordinateBoToDo(BO.Coordinate coor)
        {
            return new IDal.DO.Coordinate() { InputCoorValue = coor.InputCoorValue, Degrees = coor.Degrees, Direction = (IDal.DO.Directions)coor.Direction, MyLocation = (IDal.DO.Locations)coor.MyLocation , Minutes = coor.Minutes, Seconds = coor.Seconds};
        }

        private BO.Coordinate CoordinateDoToBo(IDal.DO.Coordinate coor)
        {
            return new BO.Coordinate() { InputCoorValue = coor.InputCoorValue, Degrees = coor.Degrees, Direction = (BO.Directions)coor.Direction, MyLocation = (BO.Locations)coor.MyLocation, Minutes = coor.Minutes, Seconds = coor.Seconds };
        }
    }
}
