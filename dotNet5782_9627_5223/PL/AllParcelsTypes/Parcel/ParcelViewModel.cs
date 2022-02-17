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
using System.ComponentModel;
using BO;

namespace PL
{
    public class ParcelViewModel : INotifyPropertyChanged
    {
        #region Private Fields
        //---------------------------------------PrivateFields-----------------------------------------
        BLApi.IBL bl;
        string selectedParcelAction, selectedWeight, selectedPriority, selectedParcelStatus;
        IList<string> nullString = new List<string>() { "" };
        IList<string> statuses = Enum.GetNames(typeof(POConverter.ParcelStatuses));
        IList<string> weights = Enum.GetNames(typeof(POConverter.WeightCategories));
        IList<string> priorities = Enum.GetNames(typeof(POConverter.Priorities));
        IList<string> parcelActions = Enum.GetNames(typeof(POConverter.DroneActions)).TakeLast<string>(2).ToList();
        #endregion

        #region Properties
        //---------------------------------------Properties-----------------------------------------
        public event PropertyChangedEventHandler PropertyChanged;
        public string Cursor { get; set; }
        public bool EnableUpdate { get; set; }
        public string State { get; set; }
        public PO.Parcel MyParcel { set; get; }
        public ListCollectionView Statuses { get; set; }
        public ListCollectionView Weights { get; set; }
        public ListCollectionView Priorities { get; set; }
        public ListCollectionView MyParcelActions { get; set; }
        public string SelectedWeight { set { selectedWeight = value; MyParcel.Weight = (POConverter.WeightCategories)Enum.Parse(typeof(POConverter.WeightCategories), value); PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SelectedWeight))); } get => selectedWeight; }
        public string SelectedPriority { set { selectedPriority = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SelectedPriority))); } get => selectedPriority; }
        public string SelectedParcelStatus { set { selectedParcelStatus = value; MyParcel.Status = (POConverter.ParcelStatuses)Enum.Parse(typeof(POConverter.ParcelStatuses), value); PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SelectedParcelStatus))); } get => selectedParcelStatus; }
        public RelayCommand AddOrUpdate { get; set; }
        public RelayCommand Delete { get; set; }
        public RelayCommand LeftDoubleClick_Sender { get; set; }
        public RelayCommand LeftDoubleClick_Target { get; set; }
        public RelayCommand LeftDoubleClick_Drone { get; set; }
        public RelayCommand Cancel { set; get; }
        public string SelectedParcelAction
        {
            set
            {
                selectedParcelAction = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SelectedParcelAction)));
                NotInitalizeRule n1 = new();
                if (!(IsAllValid() && IsValid(MyParcel.DroneId, n1)) && value!= "")
                {
                    MessageBox.Show("Not all the fields are filled with correct values\nThis action is invalid!");
                    return;
                }

                switch (selectedParcelAction)
                {
                    case nameof(POConverter.DroneActions.PickUp):
                        {

                            try
                            {
                                bl.PickUpParcel(MyParcel.DroneId);
                                MessageBox.Show("Parcel has been pickedUp successfully!");
                            }
                            catch (ParcelStatusException exe)
                            {
                                MessageBox.Show($"Parcel's status: {exe.ParcelStatus} isn't valid for picking it up.");
                            }
                            catch (DroneStatusException exe)
                            {
                                MessageBox.Show($"Drone's status: {exe.Status} isn't valid for picking it up.");
                            }
                            catch (ParcelActionsException exe)
                            {
                                MessageBox.Show($"Parcel Action: {exe.Action} wasn't succeeded in being completed.");
                            }
                        }
                        break;

                    case nameof(POConverter.DroneActions.Supply):
                        {
                            try
                            {
                                bl.SupplyParcel(MyParcel.DroneId);
                                MessageBox.Show("Parcel is supplied successfully!");
                            }
                            catch (ParcelStatusException exe)
                            {
                                MessageBox.Show($"Parcel's status: {exe.ParcelStatus} isn't valid for supplying it.");
                            }
                            catch (ParcelActionsException exe)
                            {
                                MessageBox.Show($"Parcel Action: {exe.Action} wasn't succeeded in being completed.");
                            }
                            break;
                        }
                }
            }
            get
            {
                return selectedParcelStatus;
            }
        }

        #endregion

        #region Constructors
        //-----------------------------------Constructors-----------------------------------------
        public ParcelViewModel(BO.ParcelForList parcel, BLApi.IBL bl)
        {
            this.bl = bl;
            MyParcel = new PO.Parcel(bl, parcel);
            SelectedWeight = ((object)MyParcel.Weight).ToString();
            SelectedParcelStatus = ((object)MyParcel.Status).ToString();
            SelectedPriority = ((object)MyParcel.Priority).ToString();
            Statuses = new ListCollectionView(nullString.Concat<string>(statuses).ToList());
            Weights = new ListCollectionView(nullString.Concat<string>(weights).ToList());
            Priorities = new ListCollectionView(nullString.Concat<string>(priorities).ToList());
            MyParcelActions = new ListCollectionView(nullString.Concat<string>(parcelActions).ToList());
            Cancel = new(ButtonClick_Cancel);
            EnableUpdate = false;
            State = "Update";
            Cursor = "Hand";
            Delete = new(Button_ClickDelete, null);
            AddOrUpdate = new(Button_ClickUpdate, null);
            LeftDoubleClick_Sender = new(DoubleClick_Sender, null);
            LeftDoubleClick_Target = new(DoubleClick_Target, null);
            LeftDoubleClick_Drone = new(DoubleClick_Drone, null);
        }
        public ParcelViewModel(BLApi.IBL bl)
        {
            this.bl = bl;
            MyParcel = new();
            MyParcel.ParcelId = GetParcelIndex() + 1;
            MyParcel.Status = POConverter.ParcelStatuses.Production;
            Cancel = new(ButtonClick_Cancel, null);
            AddOrUpdate = new(Button_ClickAdd, null);
            EnableUpdate = true;
            State = "Add";
            Cursor = "IBeam";
            SelectedWeight = ((object)MyParcel.Weight).ToString();
            SelectedParcelStatus = ((object)MyParcel.Status).ToString();
            SelectedPriority = ((object)MyParcel.Priority).ToString();
            Statuses = new ListCollectionView(nullString.Concat<string>(statuses).ToList());
            Weights = new ListCollectionView(nullString.Concat<string>(weights).ToList());
            Priorities = new ListCollectionView(nullString.Concat<string>(priorities).ToList());
            MyParcelActions = new ListCollectionView(nullString.Concat<string>(parcelActions).ToList());
            LeftDoubleClick_Sender = new(DoubleClick_Sender, null);
            LeftDoubleClick_Target = new(DoubleClick_Target, null);
            LeftDoubleClick_Drone = new(DoubleClick_Drone, null);
        }
        #endregion

        #region ButtonsEvents
        //---------------------------------------Buttons-----------------------------------------
        public void ButtonClick_Cancel(object sender)
        {
            (sender as Window).Close();
        }

        #endregion

        #region DoubleClick_Events
        //---------------------------------------DoubleClick_Events-----------------------------------------
        private void DoubleClick_Sender(object sender)
        {
            new CustomerView(new CustomerViewModel(bl,CustomerForListBOToPO( bl.GetCustomerForList(MyParcel.SenderId)))).Show();
        }

        private void DoubleClick_Target(object sender)
        {          
            new CustomerView(new CustomerViewModel(bl, CustomerForListBOToPO(bl.GetCustomerForList(MyParcel.TargetId)))).Show();
        }
        private void DoubleClick_Drone(object sender)
        {
            new DroneView(new DroneViewModel(bl, DroneForListBOToPO(bl.GetDroneForList(MyParcel.DroneId)))).Show();
        }
        #endregion

        #region CRUD Events
        //------------------------------------CRUD Events------------------------------------------------

        private void Button_ClickDelete(object sender)
        {
            if (!IsAllValid())
            {
                MessageBox.Show("Not all the fields are filled with correct values\nThis action is invalid!");
                return;
            }
            if (MyParcel.Status != POConverter.ParcelStatuses.Production)
            {
                MessageBox.Show("Can not delete this parcel since \nit has been associated already.");
                return;
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
        private void Button_ClickAdd(object sender)
        {
            if (!IsAllValid())
            {
                MessageBox.Show("Not all the fields are filled with correct values\nThis action is invalid!");
                return;
            }
            try
            {
                MyParcel.Priority = (POConverter.Priorities)Enum.Parse(typeof(POConverter.Priorities), SelectedPriority);
                bl.Add(ParcelPoToBo(MyParcel));
                ListsModel.Instance.AddParcel(MyParcel.ParcelId);
                MessageBox.Show("The parcel has been added successfully!\nPay attention - the last valid input is saved.");
            }
            catch (IntIdException exe)
            {
                MessageBox.Show($"the chosen id: {exe.Id} already exists in the database");
            }
            catch (StringIdException exe)
            {
                MessageBox.Show($"the chosen id: {exe.Id} doesn't exist in the database");
            }
        }

        private void Button_ClickUpdate(object sender)
        {
            if (!IsAllValid())
            {
                MessageBox.Show("Not all the fields are filled with correct values\nThis action is invalid!");
                return;
            }
            try
            {
                int droneId = MyParcel.DroneId;
                NotInitalizeRule n1 = new();
                if (!IsValid(MyParcel.DroneId, n1))
                {
                    if (SelectedPriority != ((object)MyParcel.Priority).ToString())
                    {
                        MyParcel.Priority = (POConverter.Priorities)Enum.Parse(typeof(POConverter.Priorities), SelectedPriority);
                        bl.UpdateParcel(ParcelPoToBo(MyParcel));
                        ListsModel.Instance.UpdateParcel(MyParcel.ParcelId);
                        MessageBox.Show("The parcel has been updated successfully!\nPay attention - the last valid input is saved.");
                        return;
                    }
                    else
                    {
                        MessageBox.Show("No changes have been done.\nThere's no what to update.");
                        return;
                    }
                }
            }
            catch (IntIdException exe)
            {
                MessageBox.Show($"the chosen id: {exe.Id} doesn't exists in the database");
                return;
            }
        }
        #endregion

        #region Validation
        //---------------------------------------Validation-----------------------------------
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
                IsValid(MyParcel.DroneId, n1, n2, n6);
        }
        #endregion
    }
}

