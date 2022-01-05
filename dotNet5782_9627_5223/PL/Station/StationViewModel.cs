using PL.PO;
using System;
using System.ComponentModel;
using System.Windows;

namespace PL
{
    public class StationViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        BLApi.IBL bl;
        object coorLon, coorLat;
        Action refreshDroneList;

        public PO.Station BaseStation { get; set; }
        public bool EnableUpdate { get; set; }
        public string State { get; set; }
        public RelayCommand Cancel { get; set; }
        public RelayCommand AddOrUpdate { get; set; }
        public RelayCommand LeftDoubleClick { get; set; }
        public StationViewModel(BLApi.IBL bl, BO.BaseStationForList station) : this(bl)
        {
            BaseStation = new(bl, station);
            AddOrUpdate = new(Button_ClickUpdate, null);
            EnableUpdate = false;
            coorLon = BaseStation.Location.CoorLongitude.ToString();
            coorLat = BaseStation.Location.CoorLatitude.ToString();
            State = "Update";
        }
        public StationViewModel(BLApi.IBL bl)
        {
            this.bl = bl;
            BaseStation = new PO.Station();
            Cancel = new(Button_ClickCancel, null);
            AddOrUpdate = new(Button_ClickAdd, null);
            EnableUpdate = true;
            State = "Add";
            LeftDoubleClick = new(doubleClickDrone ,null);
        }
        public object CoorLon
        {
            get => coorLon;
            set
            {
                if (double.TryParse(value.ToString(), out double longitude))
                {
                    coorLon = value;
                    BaseStation.Location.CoorLongitude = new Coordinate(longitude, Locations.Longitude);
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
                    BaseStation.Location.CoorLatitude = new Coordinate(latitude, Locations.Latitude);
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

        private void Button_ClickAdd(object sender)
        {
            bl.Add(POConverter.StationPoToBo(BaseStation));
        }
        private void Button_ClickUpdate(object sender)
        {

        }
        private void doubleClickDrone(object sender)
        {
            new DroneView(bl.GetDroneForList((sender as PO.DroneInCharging).Id),bl, refreshDroneList).Show();
        }        
    }
}
