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
using static PL.Validation;
using static IBL.BL;
using DO;

namespace PL
{
    public class ParcelViewModel
    {
        BLApi.IBL bl;
        string selectedParcelStatus;
        IList<string> nullString = new List<string>() { "" };
        IList<string> statuses = Enum.GetNames(typeof(ParcelStatuses));
        IList<string> weights = Enum.GetNames(typeof(POConverter.WeightCategories));
        IList<string> priorities = Enum.GetNames(typeof(POConverter.Priorities));
        IList<string> parcelActions = Enum.GetNames(typeof(ParcelActions)).TakeLast<string>(2).ToList();
        public string Cursor { get; set; }
        public bool EnableUpdate { get; set; }
        public string State { get; set; }
        public PO.Parcel MyParcel { set; get; }
        public ListCollectionView Statuses { get; set; }
        public ListCollectionView Weights { get; set; }
        public ListCollectionView Priorities { get; set; }
        public ListCollectionView ParcelActions { get; set; }
        public string SelectedStatus { get; set; }
        public string SelectedWeight { get; set; }
        public string SelectedPriority { get; set; }
        public string SelectedParcelAction { get; set; }
        public RelayCommand AddOrUpdate { get; set; }
        public RelayCommand Delete { get; set; }
        public RelayCommand LeftDoubleClick_Sender { get; set; }
        public RelayCommand LeftDoubleClick_Target { get; set; }
        public RelayCommand LeftDoubleClick_Drone { get; set; }
        public RelayCommand Cancel { set; get; }

        public ParcelViewModel(BO.ParcelForList parcel, BLApi.IBL bl)
        {
            this.bl = bl;
            MyParcel = new PO.Parcel(bl, parcel);
            Statuses = new ListCollectionView(nullString.Concat<string>(statuses).ToList());
            Weights = new ListCollectionView(nullString.Concat<string>(weights).ToList());
            Priorities = new ListCollectionView(nullString.Concat<string>(priorities).ToList());
            ParcelActions = new ListCollectionView(nullString.Concat<string>(parcelActions).ToList());
            Cancel = new(ButtonClick_Cancel);
            EnableUpdate = false;
            State = "Update";
            Cursor = "Hand";
            Delete = new(Button_ClickDelete, null);
            LeftDoubleClick_Sender = new(DoubleClick_Sender, null);
            LeftDoubleClick_Target = new(DoubleClick_Target, null);
            LeftDoubleClick_Drone = new(DoubleClick_Drone, null);
        }
        public ParcelViewModel(BLApi.IBL bl)
        {
            this.bl = bl;
            MyParcel = new();
            MyParcel.ParcelId = GetParcelIndex()+1;
            MyParcel.Status = ParcelStatuses.Production;
            SelectedStatus = nameof(ParcelStatuses.Production);
            Cancel = new(ButtonClick_Cancel, null);
            AddOrUpdate = new(Button_ClickAdd, null);
            EnableUpdate = true;
            State = "Add";
            Cursor = "IBeam";
            Statuses = new ListCollectionView(nullString.Concat<string>(statuses).ToList());
            Weights = new ListCollectionView(nullString.Concat<string>(weights).ToList());
            Priorities = new ListCollectionView(nullString.Concat<string>(priorities).ToList());
            ParcelActions = new ListCollectionView(nullString.Concat<string>(parcelActions).ToList());
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
            if (!IsAllValid())
            {
                MessageBox.Show("Not all the fields are filled with correct values\nThis action is invalid!");
                return;
            }
            if (MyParcel.Status != ParcelStatuses.Production)
            {
                MessageBox.Show("Can not delete this parcel since \nit has been associated already.");
                return;
            }
            if (MyParcel == null)
            {
                MessageBox.Show("No parcel was chosen, \nnot possible deleting nothing.");
            }
            try
            {
                bl.Delete(ParcelPoToBo(MyParcel));
                ListsModel.Instance.DeleteParcel(MyParcel.ParcelId);
                MessageBox.Show("The parcel has been deleted successfully!\nPay attention - the last valid input is saved.");
                (sender as Window).Close();
            }
            catch
            {

            }
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
        private void Button_ClickAdd(object sender)
        {
            if (!IsAllValid())
            {
                MessageBox.Show("Not all the fields are filled with correct values\nThis action is invalid!");
                return;
            }
            try
            {
                bl.Add(ParcelPoToBo(MyParcel));
                ListsModel.Instance.AddParcel(MyParcel.ParcelId);
                MessageBox.Show("The parcel has been added successfully!\nPay attention - the last valid input is saved.");
            }
            catch(IntIdException exe)
            {
                MessageBox.Show($"the chosen id: {exe.Id} already exists in the database");
            }
            catch(StringIdException exe)
            {
                MessageBox.Show($"the chosen id: {exe.Id} doesn't exist in the database");
            }
        }

        //there's no option to update something in parcel entity.
        //private void Button_ClickUpdate(object sender)
        //{
        //    bl.UpdateParcel(ParcelPoToBo(Parcel));
        //    ListsModel.Instance.UpdateParcel(Parcel.ParcelId);
        //}

        bool IsAllValid()
        {
            NotEmptyRule n1 = new();
            NumberRule n2 = new();
            NameRule n3 = new();
            RealPositiveNumberRule n4 = new();
            PositiveNumberRule n5 = new();
            PositiveDoubleRule n6 = new();
            StringIdRule n7 = new();
            PositiveDoubleRule n8 = new();
            DoubleRule n9 = new();
            return IsValid(MyParcel.ParcelId, n1, n2, n4) &&
                IsValid(MyParcel.SenderId, n1, n2, n4, n7) &&
                IsValid(MyParcel.TargetId, n1, n2, n4, n7) &&
                IsValid(MyParcel.DroneId, n1, n2, n4);
        }
    }
}

