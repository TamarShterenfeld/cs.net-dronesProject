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
        Action refreshDroneList;
        public PO.Customer Customer { get; set; }
        public bool EnableUpdate { get; set; }
        public string State { get; set; }
        public RelayCommand Cancel { get; set; }
        public RelayCommand AddOrUpdate { get; set; }
        public RelayCommand Delete { get; set; }
        public RelayCommand LeftDoubleClick { get; set; }
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
        public CustomerViewModel(BLApi.IBL bl)
        {
            this.bl = bl;
            Customer = new();
            Cancel = new(Button_ClickCancel, null);
            AddOrUpdate = new(Button_ClickAdd, null);
            EnableUpdate = true;
            State = "Add";
            LeftDoubleClick = new(doubleClickDrone, null);
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

        private void Button_ClickDelete(object sender)
        {
            (sender as Window).Close();
        }

        private void Button_ClickAdd(object sender)
        {
            //bl.Add(StationPoToBo(BaseStation));
            //ListsModel.Instance.AddStation(BaseStation.Id);
        }
        private void Button_ClickUpdate(object sender)
        {
            bl.UpdateCustomer(Customer.Id, Customer.Name, Customer.Phone);
            ListsModel.Instance.UpdateCustomers(Customer.Id);
        }
        private void doubleClickDrone(object sender)
        {
            //new DroneView(bl.GetDroneForList((sender as PO.DroneInCharging).Id), bl, refreshDroneList).Show();
        }
    }
}
