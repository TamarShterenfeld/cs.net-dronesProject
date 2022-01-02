using BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLApi;


namespace PL
{
    namespace PO
    {
        class ParcelInPassing
        {
            public ParcelInPassing(BO.ParcelForList parcel, BLApi.IBL bL)
            {
                BO.Customer sender = bL.GetBOCustomersList().First(item => item.Id == parcel.SenderId);
                BO.Customer target = bL.GetBOCustomersList().First(item => item.Id == parcel.TargetId);
                Id = parcel.ParcelId;
                ToDestinition = parcel.Status == ParcelStatuses.PickedUp ? true : false;
                Priority = parcel.Priority;
                Weight = parcel.Weight;
                string senderName = sender.Name;
                string targetName = target.Name;
                Sender = new CustomerInParcel { Id = parcel.SenderId, Name = senderName };
                Target = new CustomerInParcel { Id = parcel.TargetId, Name = targetName };              
                Collect = ConvertBoLocationToPoLocation(sender.Location);
                Destination = ConvertBoLocationToPoLocation(target.Location);
                Distance = sender.Distance(target);
            }
            public int Id { get; set; }
            public bool ToDestinition { set; get; }
            public Priorities Priority { get; set; }
            public WeightCategories Weight { get; set; }
            public CustomerInParcel Sender { get; set; }
            public CustomerInParcel Target { get; set; }
            public Location Collect { get; set; }
            public Location Destination { get; set; }
            public double Distance { get; set; }
            private PO.Location ConvertBoLocationToPoLocation(BO.Location location)
            {
                PO.Coordinate longitude = new(location.CoorLongitude.Degrees, Locations.Longitude);
                PO.Coordinate latitude = new(location.CoorLatitude.Degrees, Locations.Latitude);
                return new(longitude, latitude);
            }
        }
    }
    
}
