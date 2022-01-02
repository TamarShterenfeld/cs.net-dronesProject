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
                Id = parcel.ParcelId;
                //ToDestinition =  parcel.T
                Priority = parcel.Priority;
                Weight = parcel.Weight;
                string senderName = bL.GetCustomersList().First(item => item.Id == parcel.SenderId).Name;
                string targetName = bL.GetCustomersList().First(item => item.Id == parcel.TargetId).Name;
                Sender = new CustomerInParcel { Id = parcel.SenderId, Name = senderName };
                Target = new CustomerInParcel { Id = parcel.TargetId, Name = targetName };
                BO.Location senderLocation = bL.GetBOCustomersList().First(item => item.Id == parcel.SenderId).Location;
                BO.Location targetLocation = bL.GetBOCustomersList().First(item => item.Id == parcel.TargetId).Location;
                Collect = ConvertBoLocationToPoLocation(senderLocation);
                Destination = ConvertBoLocationToPoLocation(targetLocation);
                Distance = senderLocation.Distance(senderLocation);
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
