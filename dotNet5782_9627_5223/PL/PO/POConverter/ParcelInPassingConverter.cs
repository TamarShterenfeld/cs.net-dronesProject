using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL.PO
{
    public static partial class POConverter
    {
        public static PO.ParcelInPassing ParcelInPassingBOTOPO(BO.ParcelInPassing parcel)
        {
            string toDest = parcel.ToDestination ? "yes" : "no";
            return new PO.ParcelInPassing(parcel.Id, toDest, parcel.Priority,
                parcel.Weight, parcel.Sender, parcel.Target, parcel.Collect, parcel.Destination, parcel.Distatnce);
        }

    }
}
