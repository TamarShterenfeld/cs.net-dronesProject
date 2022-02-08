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
        BLApi.IBL bl;     
        public bool EnableUpdate { get; set; }  
        public PO.Parcel Parcel { set; get; }       
        public Array PrioritiesArr { get; set; }
        public Array WeightArr { get; set; }
        public Array DroneStatusesList { get; set; }
        public RelayCommand Delete { get; set; }
        public RelayCommand LeftDoubleClick { get; set; }
        public RelayCommand Cancel { set; get; }

        public ParcelViewModel(BO.ParcelForList parcel, BLApi.IBL bl)
        {
            this.bl = bl;
            Parcel = new PO.Parcel(bl, parcel);
            PrioritiesArr = typeof(BO.Priorities).GetEnumValues();
            WeightArr = typeof(BO.WeightCategories).GetEnumValues();
            DroneStatusesList = typeof(BO.DroneStatuses).GetEnumValues();
            Cancel = new(ButtonClick_Cancel);
            Delete = new(Button_ClickDelete, null);
            LeftDoubleClick = new(DoubleClick_Customer, null);
        }

        public ParcelViewModel(BLApi.IBL bl)
        {
            this.bl = bl;
            Parcel = new Parcel();
        }

        public void ButtonClick_Cancel(object sender)
        {
            (sender as Window).Close(); 
        }

        private void DoubleClick_Customer(object sender)
        {
            new CustomerInParcelView(new CustomerInParcelViewModel()).Show();
        }
        private void Button_ClickDelete(object sender)
        {
            if (Parcel.Status != ParcelStatuses.Production)
            {
                MessageBox.Show("Can not delete this parcel since \nit has been associated already.");
                return;
            }
            if(Parcel == null)
            {
                MessageBox.Show("No parcel was chosen, \nnot possible deleting nothing.");
            }
            bl.Delete(ParcelPoToBo(Parcel));
            ListsModel.Instance.DeleteParcel(Parcel.ParcelId);
            (sender as Window).Close();
        }

        //private void Button_ClickAdd(object sender)
        //{
        //    bl.Add(ParcelPoToBo(Parcel));
        //    ListsModel.Instance.AddParcel(Parcel.ParcelId);
        //}

        //there's no option to update something in parcel entity.
        //private void Button_ClickUpdate(object sender)
        //{
        //    bl.UpdateParcel(ParcelPoToBo(Parcel));
        //    ListsModel.Instance.UpdateParcel(Parcel.ParcelId);
        //}
    }
}

