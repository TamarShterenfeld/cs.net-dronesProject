using BO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Data;

namespace PL
{
    public class CustomersListViewModel : INotifyPropertyChanged
    {
        BLApi.IBL bl;
        public CustomersListViewModel(BLApi.IBL bl)
        {
            this.bl = bl;
            Cancel = new(Button_ClickCancel, null);
            Add = new(Button_ClickAdd, null);
            AllCustomers = new ListCollectionView(ListsModel.Instance.Customers);
            LeftDoubleClick = new(DroneListView_MouseDoubleClick, null);
        }
        public RelayCommand Cancel { get; set; }
        public RelayCommand Add { get; set; }
        public RelayCommand LeftDoubleClick { get; set; }
        public ListCollectionView AllCustomers { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        //---------------------------------Customers's Methods------------------------------
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
        /// the function treats the event of clicking on the button 'Add'.
        /// </summary>
        private void Button_ClickAdd(object sender)
        {
            new CustomerView(new CustomerViewModel(bl)).Show();
        }
        private void DroneListView_MouseDoubleClick(object sender)
        {
            new CustomerView(new CustomerViewModel(bl, sender as CustomerForList)).Show();
        }
    }
}
