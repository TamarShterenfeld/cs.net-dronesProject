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
        BLApi.IBL bl;
        public event PropertyChangedEventHandler PropertyChanged;
        private int id;
        Location location = new();
        public int Id
        {
            get=> id;
            set
            {
                id = value;
                PropertyChanged?.Invoke(this,new PropertyChangedEventArgs(nameof(Id)));
            }
        }
        private string name;
        public string Name
        {
            get => name;
            set
            {
                name = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Name)));
            }
        }

        public Location Location
        {
            get
            {
                return location;
            }
            set
            {
                location = value;
            }
        }

        private int chargeSlots;

        public int ChargeSlots
        {
            get => chargeSlots;
            set
            {
                chargeSlots = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ChargeSlots)));
            }
        }
        public List<DroneInCharging> DroneCharging { get; set; }
        public bool IsDeleted { get; set; }
        public Customer(BLApi.IBL bl,BO.BaseStationForList station)
        {
            BO.BaseStation CurStation = bl.GetBLBaseStation(station.Id);
            this.bl = bl;
            Id = station.Id; Name = station.Name; Location = LocationBOTOPO(CurStation.Location); 
            ChargeSlots = station.AvailableChargeSlots + station.CaughtChargeSlots ; IsDeleted = CurStation.IsDeleted;
            DroneCharging = (List<PO.DroneInCharging>)DroneInChargingListBoToPo(CurStation.DroneCharging);
        }

        // default constructor
        public Customer() { }
        

    }
}
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;


//namespace BO
//{
//    /// <summary>
//    /// the class Customer contains the following details: id, name, phone, longitude, latitude.
//    /// </summary>
//    public class Customer : ILocatable
//    {
//        string id;
//        string name;
//        string phone;
//        Location myLocation = new();
//        public string Id
//        {
//            get
//            {
//                return id;
//            }
//            set
//            {
//                if (value.Length != 9)
//                {
//                    throw new BLStringIdException(value);
//                }
//                foreach (char digit in value)
//                {
//                    if (!Char.IsDigit(digit))
//                    {
//                        throw new BLStringIdException(value);

//                    }
//                }
//                id = value;
//            }
//        }
//        public string Name
//        {
//            get
//            {
//                return name;
//            }
//            set
//            {
//                foreach (char letter in value)
//                {
//                    if (letter != ' ')
//                    {
//                        if (!Char.IsLetter(letter))
//                        {
//                            throw new BLStringException(value);
//                        }
//                    }
//                }
//                name = value;
//            }
//        }

//        public string Phone
//        {
//            get
//            {
//                return phone;
//            }
//            set
//            {
//                if (value[0] != '0')
//                    throw new BLPhoneException(value);
//                foreach (char digit in value)
//                {
//                    if (!Char.IsDigit(digit))
//                    {
//                        throw new BLPhoneException(value);

//                    }
//                }
//                phone = value;
//            }
//        }


//        public Location Location
//        {
//            set
//            {
//                myLocation = value;
//            }
//            get
//            {
//                return myLocation;
//            }
//        }

//        public List<ParcelInCustomer> FromCustomer { get; set; }
//        public List<ParcelInCustomer> ToCustomer { get; set; }


//        /// <summary>
//        /// a constructor with parameters/// </summary>
//        /// <param name="id"> Customer's id </param>
//        /// <param name="name"> Customer's name </param>
//        /// <param name="phone"> Customer's phone </param>
//        /// <param name="location"> Customer's location </param>
//        /// <param name="fromCustomer"> Customer's fromCustomer </param>
//        /// <param name="toCustomer"> Customer's toCustomer </param>
//        public Customer(string id, string name, string phone, Location location, List<ParcelInCustomer> fromCustomer, List<ParcelInCustomer> toCustomer)
//        {
//            this.id = id; this.name = name; this.phone = phone;
//            Id = id; Name = name; Phone = phone; Location = location; FromCustomer = fromCustomer; ToCustomer = toCustomer;
//        }

//        //default constructor
//        public Customer() { }

//        /// <summary>
//        /// override ToString function.
//        /// </summary>
//        /// <returns>description of the Customer object</returns>
//        public override string ToString()
//        {
//            return $"id: {Id} \n" +
//                    $"name: {Name} \n" +
//                    $"phone: {Phone}\n" +
//                    $"location: {Location}\n" +
//                    $"FromCustomer: {DeliveryInCustomerDetails(FromCustomer)}\n" +
//                    $"ToCustomer: {DeliveryInCustomerDetails(ToCustomer)}";
//        }

//        /// <summary>
//        /// collect all the details about the delivery in customer
//        /// </summary>
//        /// <returns> the details about the delivery in customer </returns>
//        private static string DeliveryInCustomerDetails(List<ParcelInCustomer> DroneIC)
//        {
//            string deliveryDetails = "";
//            foreach (ParcelInCustomer drone in DroneIC)
//            {
//                deliveryDetails += drone.ToString();
//            }
//            return deliveryDetails;
//        }
//    }

//}
