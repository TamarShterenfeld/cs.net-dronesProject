using BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLApi;
using System.ComponentModel;


namespace PL
{
    namespace PO
    {
        //there's no need to create the event PropertyChangedEventHandler - for this window is passive
        //(without changes).
        public class ParcelInPassing /* : INotifyPropertyChanged */
        {
            #region Properties
            public int Id { get; set; }
            public string ToDestinition { set; get; }
            public Priorities Priority { get; set; }
            public WeightCategories Weight { get; set; }
            public CustomerInParcel Sender { get; set; }
            public CustomerInParcel Target { get; set; }
            public Location Collect { get; set; }
            public Location Destination { get; set; }
            public double Distance { get; set; }

            //public event PropertyChangedEventHandler PropertyChanged;
            #endregion

            #region Constructors
            public ParcelInPassing(BO.ParcelInPassing parcel, BLApi.IBL bL)
            {
                BO.ParcelForList parcel1 = bL.GetParcelForList(parcel.Id);
                BO.Customer sender = bL.GetBOCustomersList().First(item => item.Id == parcel1.SenderId);
                BO.Customer target = bL.GetBOCustomersList().First(item => item.Id == parcel1.TargetId);
                Id = parcel.Id;
                ToDestinition = parcel1.Status == ParcelStatuses.PickedUp ? "yes" : "false";
                Priority = parcel.Priority;
                Weight = parcel.Weight;
                string senderName = sender.Name;
                string targetName = target.Name;
                Sender = new CustomerInParcel(sender.Id, senderName);
                Target = new CustomerInParcel(target.Id, targetName);
                Collect = POConverter.LocationBOTOPO(sender.Location);
                Destination = POConverter.LocationBOTOPO(target.Location);
                Distance = sender.Distance(target);
            }

            public ParcelInPassing() { }

            public ParcelInPassing(int id, string toDest, Priorities priority, WeightCategories weight, BO.CustomerInParcel sender,
                BO.CustomerInParcel target, BO.Location collect, BO.Location destination, double distance)
            {
                Id = id; ToDestinition = toDest; Priority = priority; Weight = weight; Sender = POConverter.CustomerInParcelBOTOPO(sender);
                Target = POConverter.CustomerInParcelBOTOPO(target); Collect = POConverter.LocationBOTOPO(collect);
                Destination = POConverter.LocationBOTOPO(destination); Distance = distance;
            }
            #endregion'

        }
    }

}
