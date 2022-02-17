using BO;
using Singleton;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using static PL.PO.POConverter;
using System.Threading.Tasks;
using System.Windows.Data;

namespace PL
{
    sealed partial class ListsModel : Singleton<ListsModel>, INotifyPropertyChanged
    {
        // CustomerViewModel Lists
        
        ObservableCollection<PO.CustomerForList> customers;
        public ObservableCollection<PO.CustomerForList> Customers
        {
            get => customers;
            private set
            {
                customers = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Customers)));
            }
        }

        /// <summary>
        /// update customer
        /// </summary>
        /// <param name="id">customer's id</param>
        public void UpdateCustomer(string id)
        {
            var customerForList = Customers.FirstOrDefault(customer => customer.Id == id);
            int index = Customers.IndexOf(customerForList);
            DeleteCustomer(customerForList.Id);
            Customers.Insert(index,CustomerForListBOToPO(bl.GetCustomerForList(id)));
        }

        /// <summary>
        /// delete customer
        /// </summary>
        /// <param name="id">customer's id</param>
        public void DeleteCustomer(string id)
        {
            var updatedCustomers = Customers.FirstOrDefault(s => s.Id == id);
            Customers.Remove(updatedCustomers);
        }

        /// <summary>
        /// add customer
        /// </summary>
        /// <param name="id">customer's id</param>
        public void AddCustomer(string id)
        {
            Customers.Add(CustomerForListBOToPO(bl.GetCustomerForList(id)));
        }
    }
}

