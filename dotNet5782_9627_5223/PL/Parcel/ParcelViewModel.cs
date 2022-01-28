using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PL
{
    public class ParcelViewModel 
    {
        BLApi.IBL Bl;
        public PO.ParcelForList Parcel { set; get; }
        public Array PrioritiesArr { get; set; }
        public Array WeightArr { get; set; }
        public Array DroneStatusesList { get; set; }

        public RelayCommand Cancel { set; get; }

        public ParcelViewModel(BO.ParcelForList parcel, BLApi.IBL bl)
        {
            Bl = bl;
            Parcel = new PO.ParcelForList(bl, parcel);
            PrioritiesArr = typeof(BO.Priorities).GetEnumValues();
            WeightArr = typeof(BO.WeightCategories).GetEnumValues();
            DroneStatusesList = typeof(BO.DroneStatuses).GetEnumValues();
            Cancel = new(ButtonClick_Cancel);
        }

        public ParcelViewModel(BLApi.IBL bl)
        {
            Bl = bl;
        }

        public void ButtonClick_Cancel(object sender)
        {
            (sender as Window).Close(); 
        }
    }
}

