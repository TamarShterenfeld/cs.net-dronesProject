using DO;
using PL.PO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using static PL.PO.POConverter;
using static PL.Validation;

namespace PL
{
    public class StationViewModel : INotifyPropertyChanged
    {
       
        #region PrivateFields
        BLApi.IBL bl;
        object coorLon, coorLat;
        string state;
        bool enableUpdate;
        List<string> listOfCurrFiles;
        private void refresh(object sender,EventArgs e)
        {
            BaseStation = new(bl,StationForListBOToPO( bl.GetBaseStationForList(BaseStation.Id)));
            coorLon = BaseStation.Location.CoorLongitude.ToString();
            coorLat = BaseStation.Location.CoorLatitude.ToString();
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(BaseStation)));
        }
        #endregion

        #region Properties

        public event PropertyChangedEventHandler PropertyChanged;
        public PO.Station BaseStation { get; set; }
        public bool EnableUpdate
        {
            get => enableUpdate;
            set
            {
                enableUpdate = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(State)));
            }
        }
        public List<string> ListOfCurrFields
        { 
            get=> listOfCurrFiles;
            set
            {
                listOfCurrFiles = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ListOfCurrFields)));
            }
        }
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
                if (IsValidDouble(coorLon + ""))
                {
                    if (!IsValidLocation(coorLon + ""))
                    {
                        MessageBox.Show("Location must be in range of -90º to 90º");
                        return;
                    }
                    coorLon = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CoorLat)));
                    BaseStation.Location.CoorLongitude = new PO.Coordinate((double)value, POConverter.Locations.Longitude);
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
                if (IsValidDouble(coorLat + ""))
                {
                    if (!IsValidLocation(coorLat + ""))
                    {
                        MessageBox.Show("Location must be in range of -90º to 90º");
                        return;
                    }
                    coorLat = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CoorLat)));
                    BaseStation.Location.CoorLatitude = new PO.Coordinate((double)value, POConverter.Locations.Latitude);
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

        #endregion

        #region Constructors
        public StationViewModel(BLApi.IBL bl, PO.BaseStationForList station)
            : this(bl)
        {
            BaseStation = new(bl, station);
            State = "Update";
            AddOrUpdate = new(Button_ClickUpdate, null);
            Delete = new(Button_ClickDelete, null);
            EnableUpdate = false;
            coorLon = BaseStation.Location.CoorLongitude.ToString();
            coorLat = BaseStation.Location.CoorLatitude.ToString();
            ListsModel.Instance.Refresh += refresh;
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


        #endregion

        #region Buttons_Events
        /// <summary>
        /// the function treats the event of clicking on the button 'Cancel'.
        /// </summary>
        /// <param name="sender">the invoking object</param>
        /// <param name="e">the event</param>
        private void Button_ClickCancel(object sender)
        {
            (sender as Window).Close();
            ListsModel.Instance.Refresh -= refresh;
        }
        private void doubleClickDrone(object sender)
        {
           new DroneView(new DroneViewModel(bl, DroneForListBOToPO(bl.GetDroneForList((sender as PO.DroneInCharging).Id)))).Show();
        }

        #endregion

        #region CRUD_Events
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
                MessageBox.Show("The station has been deleted successfully!\nPay attention - the last valid input is saved.");
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
                MessageBox.Show("The station has been added successfully!\nPay attention - the last valid input is saved.");
                (sender as Window).Close();
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
                MessageBox.Show("The station has been updated successfully!\nPay attention - the last valid input is saved.");
            }
            catch (IntIdException exe)
            {
                MessageBox.Show($"the chosen id: {exe.Id} doesn't exist in the database");
            }

        }
        #endregion

        #region Validation

        bool IsAllValid()
        {
            NotEmptyRule n1 = new();
            NumberRule n2 = new();
            NameRule n3 = new();
            RealPositiveNumberRule n4 = new();
            PositiveNumberRule n5 = new();
            return IsValid(BaseStation.Id.ToString(), n1, n2, n4) &&
                IsValid(BaseStation.Name, n1, n3) &&
                IsValid(BaseStation.ChargeSlots.ToString(), n1, n2, n5) &&
                IsValid(CoorLon, n1) && IsValid(CoorLat, n1);
        }
        #endregion

    }
}
