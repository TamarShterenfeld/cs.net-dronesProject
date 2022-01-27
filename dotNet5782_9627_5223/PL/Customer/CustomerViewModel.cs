using PL.PO;
using System;
using System.ComponentModel;
using System.Windows;
using static PL.PO.POConverter;

namespace PL
{
    public class CustomerViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        BLApi.IBL bl;
        object coorLon, coorLat;
        public PO.Customer Customer { get; set; }
        public bool EnableUpdate { get; set; }
        public string State { get; set; }
        public RelayCommand Cancel { get; set; }
        public RelayCommand AddOrUpdate { get; set; }
        public RelayCommand Delete { get; set; }
        public RelayCommand LeftDoubleClick { get; set; }

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="bl">BL object</param>
        /// <param name="customer">CustomerForList object</param>
        public CustomerViewModel(BLApi.IBL bl, BO.CustomerForList customer)
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
        public object CoorLon
        {
            get => coorLon;
            set
            {
                if (double.TryParse(value.ToString(), out double longitude))
                {
                    coorLon = value;
                    Customer.Location.CoorLongitude = new Coordinate(longitude, Locations.Longitude);
                }
            }
        }
        public object CoorLat
        {
            get => coorLat;
            set
            {
                if (double.TryParse(value.ToString(), out double latitude))
                {
                    coorLat = value;
                    Customer.Location.CoorLatitude = new Coordinate(latitude, Locations.Latitude);
                }
            }
        }

        //---------------------------------BaseStation's Methods------------------------------
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
        /// delete a customer.
        /// </summary>
        /// <param name="sender">the event</param>
        private void Button_ClickDelete(object sender)
        {
            if (Customer.ToCustomer.Count != 0 || Customer.FromCustomer.Count != 0)
            {
                MessageBox.Show("Can not delete this customer since he has parcels\nfinish with the parcels and try again.");
                return;
            }
            bl.Delete(CustomerPoToBo(Customer));
            ListsModel.Instance.DeleteCustomer(Customer.Id);
            (sender as Window).Close();
        }

        /// <summary>
        /// add a new customer.
        /// </summary>
        /// <param name="sender">the event</param>
        private void Button_ClickAdd(object sender)
        {
            bl.Add(CustomerPoToBo(Customer));
            ListsModel.Instance.AddCustomer(Customer.Id);
        }

        /// <summary>
        /// update details of a customer.
        /// </summary>
        /// <param name="sender">the event</param>
        private void Button_ClickUpdate(object sender)
        {
            bl.UpdateCustomer(Customer.Id, Customer.Name, Customer.Phone);
            ListsModel.Instance.UpdateCustomer(Customer.Id);
        }

        /// <summary>
        /// show full details of parcelInCustomer object
        /// </summary>
        /// <param name="sender">the event</param>
        private void doubleClickParcel(object sender)
        {
           //new ParcelView(new ParcelViewModel(sender as PO.ParcelForList,bl));
        }
    }
}
