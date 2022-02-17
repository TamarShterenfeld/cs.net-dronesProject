using BO;
using DO;
using PL.PO;
using System;
using System.ComponentModel;
using System.Windows;
using static PL.PO.POConverter;
using static PL.Validation;

namespace PL
{
    public class CustomerViewModel : INotifyPropertyChanged
    {
        #region PrivateFields
        BLApi.IBL bl;
        object coorLon, coorLat;
        bool enableUpdate;
        string state;
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region Properties
        public PO.Customer Customer { get; set; }
        public bool EnableUpdate
        {
            get => enableUpdate;
            set
            {
                enableUpdate = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(EnableUpdate)));
            }

        }
        public string State
        {
            get => state;
            set
            {
                state = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(EnableUpdate)));
            }
        }
        public RelayCommand Cancel { get; set; }
        public RelayCommand AddOrUpdate { get; set; }
        public RelayCommand Delete { get; set; }
        public RelayCommand LeftDoubleClick { get; set; }
        public object CoorLon
        {
            get => coorLon;
            set
            {
                if (value.ToString()[0] == '-')
                {
                    value = double.Parse(value.ToString().Substring(1)) * -1;
                }

                if (IsValidDouble(value + ""))
                {
                    if (!IsValidLocation(value + ""))
                    {
                        MessageBox.Show("Location must be in range of -90º to 90º");
                        return;
                    }
                    coorLon = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CoorLon)));
                    Customer.Location.CoorLongitude = new PO.Coordinate(double.Parse(value.ToString()), POConverter.Locations.Longitude);
                }
                else
                {
                    MessageBox.Show("Location must be a double value type");
                }
            }
        }
        public object CoorLat
        {
            get => coorLat;
            set
            {
                if (value.ToString()[0] == '-')
                {
                    value = double.Parse(value.ToString().Substring(1)) * -1;
                }
                if (IsValidDouble(value + ""))
                {
                    if (!IsValidLocation(value + ""))
                    {
                        MessageBox.Show("Location must be in range of -90º to 90º");
                        return;
                    }
                    coorLat = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CoorLat)));
                    Customer.Location.CoorLatitude = new PO.Coordinate(double.Parse(value.ToString()), POConverter.Locations.Latitude);
                }
                else
                {
                    MessageBox.Show("Location must be a double value type");
                }
            }
        }
        #endregion

        #region constructors
        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="bl">BL object</param>
        /// <param name="customer">CustomerForList object</param>
        public CustomerViewModel(BLApi.IBL bl, PO.CustomerForList customer)
            : this(bl)
        {
            Customer = new PO.Customer(bl, customer);
            AddOrUpdate = new(Button_ClickUpdate, null);
            Delete = new(Button_ClickDelete, null);
            EnableUpdate = false;
            coorLon = Customer.Location.CoorLongitude.ToString();
            coorLat = Customer.Location.CoorLatitude.ToString();
            State = "Update";
        }



        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="bl">BL object</param>
        public CustomerViewModel(BLApi.IBL bl)
        {
            this.bl = bl;
            Customer = new();
            Cancel = new(Button_ClickCancel, null);
            AddOrUpdate = new(Button_ClickAdd, null);
            EnableUpdate = true;
            State = "Add";
            LeftDoubleClick = new(doubleClickParcel, null);
        }
        #endregion

        #region Button_Events
        /// <summary>
        /// the function treats the event of clicking on the button 'Cancel'.
        /// </summary>
        /// <param name="sender">the invoking object</param>
        /// <param name="e">the event</param>
        private void Button_ClickCancel(object sender)
        {
            (sender as Window).Close();
        }

        /// <summary>
        /// show full details of parcelInCustomer object
        /// </summary>
        /// <param name="sender">the event</param>
        private void doubleClickParcel(object sender)
        {
            new ParcelView(new ParcelViewModel(ParcelForListBOToPO(bl.GetParcelForList((sender as PO.ParcelInCustomer).Id)), bl)).Show();
        }

        #endregion

        #region CRUD_Events
        /// <summary>
        /// delete a customer.
        /// </summary>
        /// <param name="sender">the event</param>
        private void Button_ClickDelete(object sender)
        {
            if (!IsAllValid())
            {
                MessageBox.Show("Not all the fields are filled with correct values\nThis action is invalid!");
                return;
            }
            try
            {
                if (Customer.ToCustomer.Count != 0 || Customer.FromCustomer.Count != 0)
                {
                    MessageBox.Show("Can not delete this customer since he has parcels\nfinish with the parcels and try again.");
                    return;
                }
                bl.Delete(CustomerPoToBo(Customer));
                ListsModel.Instance.DeleteCustomer(Customer.Id);
                MessageBox.Show("The customer has been deleted successfully!\nPay attention - the last valid input is saved.");
                (sender as Window).Close();
            }
            catch (StringIdException exe)
            {
                MessageBox.Show($"the chosen id: {exe.Id} doesn't exist in the database");
            }
            catch (BLStringIdException exe)
            {
                MessageBox.Show($"the chosen id: {exe.Id} doesn't exist in the database");
            }
        }

        /// <summary>
        /// add a new customer.
        /// </summary>
        /// <param name="sender">the event</param>
        private void Button_ClickAdd(object sender)
        {

            if (!IsAllValid())
            {
                MessageBox.Show("Not all the fields are filled with correct values\nThis action is invalid!");
                return;
            }
            try
            {
                bl.Add(CustomerPoToBo(Customer));
                ListsModel.Instance.AddCustomer(Customer.Id);
                MessageBox.Show("The customer has been added successfully!\nPay attention - the last valid input is saved.");
                (sender as Window).Close();
            }
            catch (StringIdException exe)
            {
                MessageBox.Show($"the chosen id: {exe.Id} already exists in the database");
            }
            catch (BLStringIdException exe)
            {
                MessageBox.Show($"the chosen id: {exe.Id} already exists in the database");
            }
        }

        /// <summary>
        /// update details of a customer.
        /// </summary>
        /// <param name="sender">the event</param>
        private void Button_ClickUpdate(object sender)
        {

            if (!IsAllValid())
            {
                MessageBox.Show("Not all the fields are filled with correct values\nThis action is invalid!");
                return;
            }
            try
            {
                bl.UpdateCustomer(Customer.Id, Customer.Name, Customer.Phone);
                ListsModel.Instance.UpdateCustomer(Customer.Id);
                MessageBox.Show("The customer has been updated successfully!\nPay attention - the last valid input is saved.");
            }
            catch (IntIdException exe)
            {
                MessageBox.Show($"the chosen id: {exe.Id} doesn't exist in the database");
            }
            catch (BLStringIdException exe)
            {
                MessageBox.Show($"the chosen id: {exe.Id} doesn't exist in the database");
            }
        }
        #endregion

        #region Validation
        bool IsAllValid()
        {
            NotEmptyRule n1 = new();
            NumberRule n2 = new();
            NameRule n3 = new();
            RealPositiveNumberRule n4 = new();
            PositiveNumberRule n5 = new();
            StringIdRule n6 = new();
            PhoneRule n7 = new();
            return IsValid(Customer.Id, n1, n2, n4, n6) &&
                IsValid(Customer.Name, n1, n3) &&
                IsValid(Customer.Phone, n1, n2, n5, n7) &&
                IsValid(CoorLon, n1) && IsValid(CoorLon, n1);
        }
        #endregion
    }
}
