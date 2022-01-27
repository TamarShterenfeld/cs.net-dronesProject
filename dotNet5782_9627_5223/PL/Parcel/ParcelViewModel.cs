using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL
{
    public class ParcelViewModel 
    {
        BLApi.IBL Bl;
        public PO.ParcelForList Parcel { set; get; }
        public Array PrioritiesArr { get; set; }
        public Array WeightArr { get; set; }
        public Array DroneStatusesList { get; set; }

        public ParcelViewModel(BO.ParcelForList parcel, BLApi.IBL bl)
        {
            Bl = bl;
            Parcel = new PO.ParcelForList(bl, parcel);
            PrioritiesArr = typeof(BO.Priorities).GetEnumValues();
            WeightArr = typeof(BO.WeightCategories).GetEnumValues();
            DroneStatusesList = typeof(BO.DroneStatuses).GetEnumValues();
        }

        public ParcelViewModel(BLApi.IBL bl)
        {
            Bl = bl;
        }
    }
}

