using System;
using System.Collections.Generic;
using System.ComponentModel;
using static PL.PO.POConverter;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL.PO
{
    public class CustomerForList:INotifyPropertyChanged
    {
        #region PrivateFields
        private string id;
        private string name;
        private string phone;
        int amountOfSendAndSuppliedParcels;
        int amountOfSendAndNotSuppliedParcels;
        int amountOfGetParcels;
        int amountOfInPassingParcels;
        #endregion

        #region Properties
        public event PropertyChangedEventHandler PropertyChanged;
        public string Id
        {
            get => id;
            set
            {
                id = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Id)));
            }
        }

        public string Name
        {
            get => name;
            set
            {
                name = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Name)));
            }
        }

        public string Phone
        {
            get => phone;
            set
            {
                phone = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Phone)));
            }
        }

        public int AmountOfSendAndSuppliedParcels
        {
            get=> amountOfSendAndSuppliedParcels;
            set
            {
                amountOfSendAndSuppliedParcels = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(AmountOfSendAndSuppliedParcels)));
            }

        }
        public int AmountOfGetParcels
        {
            get=> amountOfGetParcels;
            set
            {
                amountOfGetParcels = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(AmountOfGetParcels)));
            }
           
        }
        public int AmountOfSendAndNotSuppliedParcels
        {
            get=> amountOfSendAndNotSuppliedParcels;
            set
            {
                amountOfSendAndNotSuppliedParcels = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(AmountOfSendAndNotSuppliedParcels)));
            }

        }
        public int AmountOfInPassingParcels
        {
            get => amountOfInPassingParcels;
            set
            {
                amountOfInPassingParcels = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(AmountOfInPassingParcels)));
            }

        }
        #endregion

        #region Constructor
        /// <summary>
        /// default constructor
        /// </summary>
        public CustomerForList() { }

        /// <summary>
        /// a constructor with parameters
        /// </summary>
        /// <param name="id">customer's id</param>
        /// <param name="name">customer's name</param>
        /// <param name="phone">customer's phone</param>
        /// <param name="amountOfSendAndSuppliedParcels">customer's amountOfSendAndSuppliedParcels</param>
        /// <param name="amountOfSendAndNotSuppliedParcels">customer's amountOfSendAndNotSuppliedParcels</param>
        /// <param name="amountOfGetParcels">customer's amountOfGetParcels</param>
        /// <param name="amountOfInPassingParcels">customer's amountOfInPassingParcels</param>
        public CustomerForList(BO.CustomerForList customer)
        {
            Id = customer.Id; Name = customer.Name; Phone = customer.Phone; AmountOfSendAndNotSuppliedParcels = customer.AmountOfSendAndSuppliedParcels;
            AmountOfSendAndNotSuppliedParcels = customer.AmountOfSendAndNotSuppliedParcels; AmountOfGetParcels = customer.AmountOfGetParcels;
            AmountOfInPassingParcels = customer.AmountOfInPassingParcels;
        }


        #endregion

        #region ToString
        /// <summary>
        /// override ToString function.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"id: {Id} \n" +
                    $"name: {Name} \n" +
                    $"phone:  {Phone}\n" +
                    $"amount of send and supplied parcels: {AmountOfSendAndSuppliedParcels}\n" +
                    $"amount of send and not supplied parcels: {AmountOfSendAndNotSuppliedParcels}\n" +
                    $"amount of get parcels: { AmountOfGetParcels}\n" +
                    $"amount of In Passing parcels: {AmountOfInPassingParcels}";
        }

        #endregion

    }
}