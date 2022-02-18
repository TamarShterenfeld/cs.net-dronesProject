using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace PL.PO
{
    public static partial class POConverter
    {
        //-----------------------------ParcelConvertong----------------------------
        public static BO.Parcel ParcelPoToBo(PO.Parcel parcel)
        {
            BO.Parcel boParcel = new BO.Parcel();
            switch (parcel.Status)
            {

                case ParcelStatuses.Associated:
                    {
                        boParcel.AssociationDate = DateTime.Now;
                        break;
                    }
                case ParcelStatuses.PickedUp:
                    {
                        boParcel.PickUpDate = DateTime.Now;
                        break;
                    }
                case ParcelStatuses.Supplied:
                    {
                        boParcel.SupplyDate = DateTime.Now;
                        break;
                    }
            }

            boParcel.Id = parcel.ParcelId;
            boParcel.IsDeleted = false;
            boParcel.Weight = (BO.WeightCategories)Enum.Parse(typeof(BO.WeightCategories),parcel.Weight.ToString());
            boParcel.Target = new BO.CustomerInParcel { Id = parcel.TargetId };
            boParcel.Sender = new BO.CustomerInParcel { Id = parcel.SenderId };
            boParcel.MyDrone = new BO.DroneInParcel { Id = parcel.DroneId };
            boParcel.Priority = (BO.Priorities)Enum.Parse(typeof(BO.Priorities), parcel.Priority.ToString());
            return boParcel;
        }


    };
}

    


