using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL.ParcelInPassing
{
    class ParcelInPassingViewModel
    {
        BLApi.IBL Bl;
        
        public PO.ParcelInPassing ParcelInPass { get; set; }
        public Array PrioritiesArr { get; set; }
        public Array WeightArr { get; set; }
        public ParcelInPassingViewModel(BO.ParcelInPassing parcel, BLApi.IBL bl)
        {
            Bl = bl;
            ParcelInPass = new PO.ParcelInPassing(parcel, bl);
            PrioritiesArr = typeof(BO.Priorities).GetEnumValues();
            WeightArr = typeof(BO.WeightCategories).GetEnumValues();
        }
    }
}
