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
        string selectedParcelStatus;
        public bool EnableUpdate { get; set; }
        public PO.Parcel MyParcel { set; get; }
        public ListCollectionView Statuses { get; set; }
        public ListCollectionView Weights { get; set; }
        public ListCollectionView Priorities { get; set; }
        public DroneStatuses SelectedStatus { get; set; }
        public WeightCategories SelectedWeight { get; set; }
        public Priorities SelectedPriority { get; set; }
        public RelayCommand Delete { get; set; }
        public RelayCommand LeftDoubleClick_Sender { get; set; }
        public RelayCommand LeftDoubleClick_Target { get; set; }
        public RelayCommand LeftDoubleClick_Drone { get; set; }
        public RelayCommand Cancel { set; get; }

        public ParcelViewModel(BO.ParcelForList parcel, BLApi.IBL bl)
        {
            this.bl = bl;
            MyParcel = new PO.Parcel(bl, parcel);
            Statuses = new ListCollectionView(Enum.GetNames(typeof(DroneStatuses)));
            Weights = new ListCollectionView(Enum.GetNames(typeof(WeightCategories)));
            Priorities = new ListCollectionView(Enum.GetNames(typeof(Priorities)));
            Cancel = new(ButtonClick_Cancel);
            Delete = new(Button_ClickDelete, null);
            LeftDoubleClick_Sender = new(DoubleClick_Sender, null);
            LeftDoubleClick_Target = new(DoubleClick_Target, null);
            LeftDoubleClick_Drone = new(DoubleClick_Drone, null);
        }

        public string SelectedParcelStatus
        {
            set
            {
                selectedParcelStatus = value;   
                if(selectedParcelStatus == "PickUp")
                {
                    if (MyParcel.Status == ParcelStatuses.Associated)
                    {
                        bl.PickUpParcel(MyParcel.DroneId);
                        //צריך לטפל במקרה של חריגות.
                        MessageBox.Show("Parcel is pickedUp successfully!");
                    }
                    else
                    {
                        MessageBox.Show("Parcel's status isn't valid for picking it up.");
                    }
                }
                if (selectedParcelStatus == "Supply")
                {
                    if (MyParcel.Status == ParcelStatuses.PickedUp)
                    {
                        bl.SupplyParcel(MyParcel.DroneId);
                        //צריך לטפל במקרה של חריגות.
                        MessageBox.Show("Parcel is supplied successfully!");
                    }
                    else
                    {
                        MessageBox.Show("Parcel's status isn't valid for suppling it.");
                    }
                }
            }
            get
            {
                return selectedParcelStatus;
            }
        }
        public ParcelViewModel(BLApi.IBL bl)
        {
            this.bl = bl;
            MyParcel = new Parcel();
        }

        public void ButtonClick_Cancel(object sender)
        {
            (sender as Window).Close();
        }

        private void DoubleClick_Sender(object sender)
        {
            new CustomerView(new CustomerViewModel(bl, bl.GetCustomerForList(MyParcel.SenderId))).Show();
        }

        private void DoubleClick_Target(object sender)
        {
            new CustomerView(new CustomerViewModel(bl, bl.GetCustomerForList(MyParcel.TargetId))).Show();
        }

        private void DoubleClick_Drone(object sender)
        {
            new DroneView(new DroneViewModel(bl,DroneForListBOToPO( bl.GetDroneForList(MyParcel.DroneId)))).Show();
        }
        private void Button_ClickDelete(object sender)
        {
            if (MyParcel.Status != ParcelStatuses.Production)
            {
                MessageBox.Show("Can not delete this parcel since \nit has been associated already.");
                return;
            }
            if (MyParcel == null)
            {
                MessageBox.Show("No parcel was chosen, \nnot possible deleting nothing.");
            }
            bl.Delete(ParcelPoToBo(MyParcel));
            ListsModel.Instance.DeleteParcel(MyParcel.ParcelId);
            MessageBox.Show("Parcel was deleted succesfully!");
            (sender as Window).Close();
        }

        private void CheckStatus_Changed(ParcelStatuses status)
        {
            ParcelStatuses originalStatus = MyParcel.Status;         
            CheckValidationOfStatus(originalStatus, status);
        }

        private void CheckValidationOfStatus(ParcelStatuses originalStatus, ParcelStatuses status)
        {
            BO.DroneForList drone = bl.GetDroneForList(MyParcel.DroneId);
           
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

