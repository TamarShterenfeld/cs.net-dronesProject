using System;
using System.Collections.Generic;
using System.ComponentModel;
using static PL.PO.POConverter;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL.PO
{
    public class Customer : INotifyPropertyChanged
    {
        #region PrivateFields
        BLApi.IBL bl;
        public event PropertyChangedEventHandler PropertyChanged;
        string id;
        string name;
        string phone;
        Location myLocation = new();
        List<ParcelInCustomer> fromCustomer;
        List<ParcelInCustomer> toCustomer;
        #endregion

        #region Properties
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

        public Location Location
        {
            get => myLocation;
            set
            {
                myLocation = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Location)));
            }
        }
        public List<ParcelInCustomer> FromCustomer
        {
            set
            {
                fromCustomer = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(FromCustomer)));
            }
            get => fromCustomer;
        }
        public List<ParcelInCustomer> ToCustomer 
        { 
            get => toCustomer;
            set
            {
                toCustomer = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ToCustomer)));
            }
        }

        #endregion

        #region Constructors
        // default constructor
        public Customer() { }

        /// <summary>
        /// a constructor with parameters/// </summary>
        /// <param name="id"> Customer's id </param>
        /// <param name="name"> Customer's name </param>
        /// <param name="phone"> Customer's phone </param>
        /// <param name="location"> Customer's location </param>
        /// <param name="fromCustomer"> Customer's fromCustomer </param>
        /// <param name="toCustomer"> Customer's toCustomer </param>
        public Customer(string id, string name, string phone, Location location, List<ParcelInCustomer> fromCustomer, List<ParcelInCustomer> toCustomer)
        {
            this.id = id; this.name = name; this.phone = phone;
            Id = id; Name = name; Phone = phone; Location = location; FromCustomer = fromCustomer; ToCustomer = toCustomer;
        }

        /// <summary>
        /// constructor with params
        /// </summary>
        /// <param name="bl">bl object</param>
        /// <param name="customer">customerForList</param>
        public Customer(BLApi.IBL bl, PO.CustomerForList customer)
        {
            BO.Customer CurCustomer = bl.GetBLCustomer(customer.Id);
            this.bl = bl;
            id = CurCustomer.Id;name = CurCustomer.Name; phone = CurCustomer.Phone;
            Id = CurCustomer.Id; Name = CurCustomer.Name; Phone = CurCustomer.Phone; Location = LocationBOTOPO(CurCustomer.Location);
            FromCustomer = ParcelInCustomerListBOToPO(CurCustomer.FromCustomer).ToList(); ToCustomer = ParcelInCustomerListBOToPO(CurCustomer.ToCustomer).ToList();
        }

        #endregion

        #region Private_Methods
        /// <summary>
        /// collect all the details about the delivery in customer
        /// </summary>
        /// <returns> the details about the delivery in customer </returns>
        private static string DeliveryInCustomerDetails(List<ParcelInCustomer> DroneIC)
        {
            string deliveryDetails = "";
            foreach (ParcelInCustomer drone in DroneIC)
            {
                deliveryDetails += drone.ToString();
            }
            return deliveryDetails;
        }
        #endregion

        #region ToString
        /// <summary>
        /// override ToString function.
        /// </summary>
        /// <returns>description of the Customer object</returns>
        public override string ToString()
        {
            return $"id: {Id} \n" +
                    $"name: {Name} \n" +
                    $"phone: {Phone}\n" +
                    $"location: {Location}\n" +
                    $"FromCustomer: {DeliveryInCustomerDetails(FromCustomer)}\n" +
                    $"ToCustomer: {DeliveryInCustomerDetails(ToCustomer)}";
        }
        #endregion
    }
}

