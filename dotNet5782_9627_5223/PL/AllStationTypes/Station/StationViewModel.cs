using DO;
using PL.PO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using static PL.PO.POConverter;

namespace PL
{
    public class StationViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        BLApi.IBL bl;
        object coorLon, coorLat;
        string state;
        public PO.Station BaseStation { get; set; }
        public bool EnableUpdate { get; set; }
        public bool EnableAdd { get; set; }

        public List<string> ListOfCurrFields { get; set; }
        public string State
        {
            get => state;
            set
            {
                state = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(State)));
            }
        }

        public object CoorLon
        {
            get => coorLon;
            set
            {
                if (double.TryParse(value.ToString(), out double longitude))
                {
                    if (!Validation.IsValidLocation(longitude))
                    {
                        MessageBox.Show("Location must be in range of -180º to 180º");
                        return;
                    }
                    coorLon = value;
                    BaseStation.Location.CoorLongitude = new PO.Coordinate(longitude, POConverter.Locations.Longitude);
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
                if (double.TryParse(value.ToString(), out double latitude))
                {
                    if (!Validation.IsValidLocation(latitude))
                    {
                        MessageBox.Show("Location must be in range of -180º to 180º");
                        return;
                    }
                    coorLat = value;
                    BaseStation.Location.CoorLatitude = new PO.Coordinate(latitude, POConverter.Locations.Latitude);
                }
                else
                {
                    MessageBox.Show("Location must be a double value type");
                }
            }
        }

        public RelayCommand Cancel { get; set; }
        public RelayCommand AddOrUpdate { get; set; }
        public RelayCommand Delete { get; set; }
        public RelayCommand LeftDoubleClick { get; set; }

        public StationViewModel(BLApi.IBL bl, BO.BaseStationForList station)
            : this(bl)
        {
            BaseStation = new(bl, station);
            State = "Update";
            AddOrUpdate = new(Button_ClickUpdate, null);
            Delete = new(Button_ClickDelete, null);
            EnableUpdate = false;
            EnableAdd = false;
            coorLon = BaseStation.Location.CoorLongitude.ToString();
            coorLat = BaseStation.Location.CoorLatitude.ToString();
        }
        public StationViewModel(BLApi.IBL bl)
        {
            this.bl = bl;
            BaseStation = new Station();
            Cancel = new(Button_ClickCancel, null);
            State = "Add";
            AddOrUpdate = new(Button_ClickAdd, null);
            EnableUpdate = true;
            LeftDoubleClick = new(doubleClickDrone, null);
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
            if (!IsAllValid())
            {
                MessageBox.Show("Not all the fields are filled with correct values\nThis action is invalid!");
                return;
            }
            if (BaseStation.DroneCharging.Count != 0)
            {
                MessageBox.Show("Can not delete this station since it charges drones\n release the drones and try again.");
                return;
            }
            try
            {
                bl.Delete(StationPoToBo(BaseStation));
                ListsModel.Instance.DeleteStation(BaseStation.Id);
                MessageBoxResult message = MessageBox.Show("The station has been deleted successfully!\nPay attention - the last valid input is saved.");
                (sender as Window).Close();
            }
            catch (IntIdException exe)
            {
                MessageBox.Show($"the chosen id: {exe.Id} doesn't exist in the database");
            }
        }

        private void Button_ClickAdd(object sender)
        {
            if (!IsAllValid())
            {
                MessageBox.Show("Not all the fields are filled with correct values\nThis action is invalid!");
                return;
            }
            try
            {
                bl.Add(StationPoToBo(BaseStation));
                ListsModel.Instance.AddStation(BaseStation.Id);
                MessageBoxResult message = MessageBox.Show("The station has been added successfully!\nPay attention - the last valid input is saved.");
            }
            catch (IntIdException exe)
            {
                MessageBox.Show($"the chosen id: {exe.Id} already exists in the database");
            }
        }
        private void Button_ClickUpdate(object sender)
        {
            if (!IsAllValid())
            {
                MessageBox.Show("Not all the fields are filled with correct values\nThis action is invalid!");
                return;
            }
            try
            {
                bl.UpdateBaseStation(BaseStation.Id, BaseStation.Name, BaseStation.ChargeSlots.ToString());
                ListsModel.Instance.UpdateStation(BaseStation.Id);
                MessageBoxResult message = MessageBox.Show("The station has been updated successfully!\nPay attention - the last valid input is saved.");
            }
            catch (IntIdException exe)
            {
                MessageBox.Show($"the chosen id: {exe.Id} doesn't exist in the database");
            }

        }
        private void doubleClickDrone(object sender)
        {
            new DroneView(new DroneViewModel(bl, DroneForListBOToPO(bl.GetDroneForList((sender as PO.DroneInCharging).Id)))).Show();
        }

        bool IsAllValid()
        {
            return IsValidId() && IsValidName() && IsValidChargeSlots() && IsValidLocation();
        }

        bool IsValidId()
        {
            NotEmptyRule n1 = new();
            NumberRule n2 = new();
            RealPositiveNumberRule n3 = new();
            CultureInfo c1 = new(1);
            return n1.Validate(BaseStation.Id.ToString(), c1).IsValid &&
                n2.Validate(BaseStation.Id.ToString(),c1).IsValid && 
                n3.Validate(BaseStation.Id.ToString(),c1).IsValid;
        }

        bool IsValidName()
        {
            NotEmptyRule n1 = new();
            NameRule n2 = new();
            CultureInfo c1 = new(2);
            return n1.Validate(BaseStation.Name, c1).IsValid &&
                n2.Validate(BaseStation.Name, c1).IsValid;
        }

        bool IsValidChargeSlots()
        {
            NotEmptyRule n1 = new();
            NumberRule n2 = new();
            PositiveNumberRule n3 = new();
            CultureInfo c1 = new(3);
            return n1.Validate(BaseStation.ChargeSlots.ToString(), c1).IsValid &&
                n2.Validate(BaseStation.ChargeSlots.ToString(), c1).IsValid
                && n3.Validate(BaseStation.ChargeSlots.ToString(),c1).IsValid;
        }

        bool IsValidLocation()
        {
            NotEmptyRule n1 = new();
            CultureInfo c1 = new(4);
            CultureInfo c2 = new(5);
            return n1.Validate(CoorLon, c1).IsValid &&
                n1.Validate(CoorLat, c1).IsValid;
        }
    }
}
