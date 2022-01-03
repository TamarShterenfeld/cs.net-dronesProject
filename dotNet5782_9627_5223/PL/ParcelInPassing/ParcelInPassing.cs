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
        public class ParcelInPassing
        {
            public ParcelInPassing(BO.ParcelInPassing parcel, BLApi.IBL bL)
            {
                ParcelForList parcel1 = bL.GetParcelForList(parcel.Id);
                BO.Customer sender = bL.GetBOCustomersList().First(item => item.Id == parcel1.SenderId);
                BO.Customer target = bL.GetBOCustomersList().First(item => item.Id == parcel1.TargetId);
                Id = parcel.Id;          
                ToDestinition = parcel1.Status == ParcelStatuses.PickedUp ? true : false;
                Priority = parcel.Priority;
                Weight = parcel.Weight;
                string senderName = sender.Name;
                string targetName = target.Name;
                Sender = new CustomerInParcel(sender.Id, senderName);
                Target = new CustomerInParcel(target.Id, targetName);
                Collect = BoToPo.LocationBOTOPO(sender.Location);
                Destination = BoToPo.LocationBOTOPO(target.Location);
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
        }
    }

}
