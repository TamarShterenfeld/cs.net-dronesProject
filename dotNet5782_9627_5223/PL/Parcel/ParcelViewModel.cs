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
using static PL.PO.POConverter;
using PL.PO;

namespace PL
{
    public class ParcelViewModel 
    {
        BLApi.IBL Bl;

        public string State { get; set; }   
        public RelayCommand AddOrUpdate { get; set; }
        public PO.Parcel Parcel { set; get; }
        public Array PrioritiesArr { get; set; }
        public Array WeightArr { get; set; }
        public Array DroneStatusesList { get; set; }

        public RelayCommand Cancel { set; get; }

        public ParcelViewModel(BO.ParcelForList parcel, BLApi.IBL bl)
        {
            Bl = bl;
            Parcel = new PO.Parcel(bl, parcel);
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

        private void Button_ClickDelete(object sender)
        {
            if (Parcel.Status != POConverter.ParcelStatuses.Production)
            {
                MessageBox.Show("Can not delete this parcel since \nit has been associated already.");
                return;
            }
            Bl.Delete(ParcelPoToBo(Parcel));
            ListsModel.Instance.DeleteParcel(Parcel.ParcelId);
            (sender as Window).Close();
        }

        private void Button_ClickAdd(object sender)
        {
            Bl.Add(ParcelPoToBo(Parcel));
            ListsModel.Instance.AddParcel(Parcel.ParcelId);
        }
        private void Button_ClickUpdate(object sender)
        {
            Bl.UpdateParcel(POConverter.ParcelPoToBo(Parcel));
            ListsModel.Instance.UpdateParcel(Parcel.ParcelId);
        }
        

    }
}

