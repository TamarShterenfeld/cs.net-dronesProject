using System;
using static PL.PO.POConverter;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL.PO
{
    public class ParcelInCustomer : INotifyPropertyChanged
    {
        int id;
        public int Id
        {
            get => id;
            set
            {
                id = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Id)));
            }
        }
        private WeightCategories weight;
        private Priorities priority;
        private ParcelStatuses parcelStatus;
        public WeightCategories Weight 
        {
            get => weight;
            set
            {
                weight = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Weight)));
            }
        }
        public Priorities Priority
        {
            get => priority;
            set
            {
                priority = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Priority)));
            }
        }
        public ParcelStatuses ParcelStatus
        {
            get => parcelStatus;
            set
            {
                parcelStatus = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ParcelStatus)));
            }
        }

        private CustomerInParcel sourceOrDest;
        public CustomerInParcel SourceOrDest 
        { 
            get => sourceOrDest; 
            set 
            { 
                sourceOrDest = value; 
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Id))); 
            } 
        }

        /// <summary>
        /// a constructor with parameters
        /// </summary>
        /// <param name="id"> DeliveryInCustomer's id </param>
        /// <param name="weight"> DeliveryInCustomer's weight </param>
        /// <param name="priority"> DeliveryInCustomer's priority </param>
        /// <param name="parcelStatus"> DeliveryInCustomer's parcelStatus </param>
        /// <param name="sourceOrDest"> DeliveryInCustomer's sourceOrDest </param>
        public ParcelInCustomer(int id, WeightCategories weight, Priorities priority, ParcelStatuses parcelStatus, CustomerInParcel sourceOrDest)
        {
            this.id = id;
            Id = id; Weight = weight; Priority = priority; ParcelStatus = parcelStatus; SourceOrDest = sourceOrDest;
        }

        // default constructor
        public ParcelInCustomer() { }

        /// <summary>
        /// override ToString function.
        /// </summary>
        /// <returns>description of the DeliveryInCustomer object</returns>
        public override string ToString()
        {
            return $"id: {Id} \n" +
                    $"weight: {Weight} \n" +
                    $"priority: {Priority}\n" +
                    $"parcelStatus: {ParcelStatus}\n";
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
