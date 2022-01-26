using System;
using System.Collections.Generic;
using System.ComponentModel;
using static PL.PO.POConverter;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using Microsoft.VisualBasic;
using System.Diagnostics;

namespace PL.PO
{
    public class Station :  IDataErrorInfo, INotifyPropertyChanged
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
                //ValidateProperty(value);
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

        public bool HasErrors => throw new NotImplementedException();

        public Station(BLApi.IBL bl,BO.BaseStationForList station)
        {
            BO.BaseStation CurStation = bl.GetBLBaseStation(station.Id);
            this.bl = bl;
            Id = station.Id; Name = station.Name; Location = LocationBOTOPO(CurStation.Location); 
            ChargeSlots = station.AvailableChargeSlots + station.CaughtChargeSlots ; IsDeleted = CurStation.IsDeleted;
            DroneCharging = (List<PO.DroneInCharging>)DroneInChargingListBoToPo(CurStation.DroneCharging);
        }

        // default constructor
        public Station() { }

        public IEnumerable GetErrors(string propertyName)
        {
            throw new NotImplementedException();
        }

        string IDataErrorInfo.Error { get { return null; } }

        string IDataErrorInfo.this[string propertyName]
        {
            get { return this.GetValidationError(propertyName); }
        }

        /// <summary>
        /// Returns true if this object has no validation errors.
        /// </summary>
        public bool IsValid
        {
            get
            {
                foreach (string property in ValidatedProperties)
                    if (GetValidationError(property) != null)
                        return false;

                return true;
            }
        }

        static readonly string[] ValidatedProperties =
        {
            "Id",
            "Name",
            "ChargeSlots",
        };

        string GetValidationError(string propertyName)
        {
            if (Array.IndexOf(ValidatedProperties, propertyName) < 0)
                return null;

            string error = null;

            switch (propertyName)
            {
                case "Id":
                    error = this.ValidateEmail();
                    break;

                case "Name":
                    error = this.ValidateFirstName();
                    break;

                case "ChargeSlots":
                    error = this.ValidateLastName();
                    break;

                default:
                    Debug.Fail("Unexpected property being validated on Station: " + propertyName);
                    break;
            }

            return error;
        }

        string ValidateEmail()
        {
            if (IsStringMissing(this.Email))
            {
                return Strings.Customer_Error_MissingEmail;
            }
            else if (!IsValidEmailAddress(this.Email))
            {
                return Strings.Customer_Error_InvalidEmail;
            }
            return null;
        }

        string ValidateFirstName()
        {
            if (IsStringMissing(this.FirstName))
            {
                return Strings.Customer_Error_MissingFirstName;
            }
            return null;
        }

        string ValidateLastName()
        {
            if (this.IsCompany)
            {
                if (!IsStringMissing(this.LastName))
                    return Strings.Customer_Error_CompanyHasNoLastName;
            }
            else
            {
                if (IsStringMissing(this.LastName))
                    return Strings.Customer_Error_MissingLastName;
            }
            return null;
        }

        static bool IsStringMissing(string value)
        {
            return
                String.IsNullOrEmpty(value) ||
                value.Trim() == String.Empty;
        }

        static bool IsValidEmailAddress(string email)
        {
            if (IsStringMissing(email))
                return false;

            // This regex pattern came from: http://haacked.com/archive/2007/08/21/i-knew-how-to-validate-an-email-address-until-i.aspx
            string pattern = @"^(?!\.)(""([^""\r\\]|\\[""\r\\])*""|([-a-z0-9!#$%&'*+/=?^_`{|}~]|(?<!\.)\.)*)(?<!\.)@[a-z0-9][\w\.-]*[a-z0-9]\.[a-z][a-z\.]*[a-z]$";

            return Regex.IsMatch(email, pattern, RegexOptions.IgnoreCase);
        }
    }
}
