using BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL.PO
{
    public static partial class POConverter
    {

        public static BO.Drone DronePOToBo(PO.Drone drone)
        {
            PO.Parcel parcel = drone.Parcel;
            return new BO.Drone
            {
                Id = drone.Id,
                Battery = drone.Battery,
                Location = LocationPOTOBO(drone.Location),
                MaxWeight = (BO.WeightCategories)Enum.Parse(typeof(BO.WeightCategories), drone.Weight.ToString()),
                Model = drone.Model,
                Parcel = new ParcelInPassing
                {
                    Id = parcel.ParcelId,
                    Weight = (BO.WeightCategories)Enum.Parse(typeof(BO.WeightCategories), parcel.Weight.ToString()),
                    Priority = (BO.Priorities)Enum.Parse(typeof(BO.Priorities), parcel.Weight.ToString()),
                    Sender = new BO.CustomerInParcel { Id = parcel.SenderId },
                    Target = new BO.CustomerInParcel { Id = parcel.TargetId },
                },
                Status = (BO.DroneStatuses)Enum.Parse(typeof(BO.DroneStatuses), drone.Status.ToString()),
            };
        }
        public static PO.Drone DroneBOToPO(BO.Drone drone, BLApi.IBL bl)
        {
            return new PO.Drone()
            {
                Id = drone.Id,
                Battery = drone.Battery,
                Location = LocationBOTOPO(drone.Location),
                Model = drone.Model,
                Status = (DroneStatuses)Enum.Parse(typeof(DroneStatuses), drone.Status.ToString()),
                Weight = (WeightCategories)Enum.Parse(typeof(WeightCategories), drone.MaxWeight.ToString()),
                Parcel = drone.Parcel != null && drone.Parcel.Id != default ? new PO.Parcel(bl, bl.GetParcelForList(drone.Parcel.Id)) : null,

            };
        }

    }
}
