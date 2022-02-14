using DO;
using PL.PO;
using System;
using System.ComponentModel;
using System.Windows;
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
        public string State
        {
            get => state;
            set
            {
                state = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(State)));
            }
        }
        public RelayCommand Cancel { get; set; }
        public RelayCommand AddOrUpdate { get; set; }
        public RelayCommand Delete { get; set; }
        public RelayCommand LeftDoubleClick { get; set; }

        public StationViewModel(BLApi.IBL bl, BO.BaseStationForList station)
            : this(bl)
        {
            try
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
            catch(IntIdException exe )
            {
                MessageBox.Show(exe.ToString());
            }
        }
        public StationViewModel(BLApi.IBL bl)
        {
            try
            {
                this.bl = bl;
                BaseStation = new Station();
                Cancel = new(Button_ClickCancel, null);
                State = "Add";
                AddOrUpdate = new(Button_ClickAdd, null);
                EnableUpdate = true;
                LeftDoubleClick = new(doubleClickDrone, null);
            }
            catch (IntIdException exe)
            { 
                MessageBox.Show(exe.ToString());
            }
        }

        public object CoorLon
        {
            get => coorLon;
            set
            {
                if (double.TryParse(value.ToString(), out double longitude))
                {
                    coorLon = value;
                    BaseStation.Location.CoorLongitude = new PO.Coordinate(longitude, POConverter.Locations.Longitude);
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
                    BaseStation.Location.CoorLatitude = new PO.Coordinate(latitude, POConverter.Locations.Latitude);
                }
            }
        }

        //---------------------------------BaseStation's Methods------------------------------
        /// <summary>
        /// the function treats the event of clicking on the button 'Cancel'.
        /// </summary>
        /// <param name="sender">the invoking object</param>
        /// <param name="e">the event</param>
        private void Button_ClickCancel(object sender, EventArgs e)
        {
            (sender as Window).Close();
        }

        private void Button_ClickDelete(object sender, EventArgs e)
        {
            if (BaseStation.DroneCharging.Count != 0)
            {
                MessageBox.Show("Can not delete this station since it charges drones\n release the drones and try again.");
                return;
            }
            bl.Delete(StationPoToBo(BaseStation));
            ListsModel.Instance.DeleteStation(BaseStation.Id);
            (sender as Window).Close();
        }

        private void Button_ClickAdd(object sender, EventArgs e)
        {
            bl.Add(StationPoToBo(BaseStation));
            ListsModel.Instance.AddStation(BaseStation.Id);
        }
        private void Button_ClickUpdate(object sender, EventArgs e)
        {
            bl.UpdateBaseStation(BaseStation.Id, BaseStation.Name, BaseStation.ChargeSlots.ToString());
            ListsModel.Instance.UpdateStation(BaseStation.Id);

        }
        private void doubleClickDrone(object sender)
        {
            new DroneView(new DroneViewModel(bl, DroneForListBOToPO(bl.GetDroneForList((sender as PO.DroneInCharging).Id)))).Show();
        }


    }
}
