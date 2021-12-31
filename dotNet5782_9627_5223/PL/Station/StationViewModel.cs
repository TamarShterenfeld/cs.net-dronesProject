using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PL
{
    public class StationViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        BLApi.IBL bl;
        public PO.StationModel BaseStation { get; set; }
        public RelayCommand Cancel { get; set; }
        public RelayCommand Add { get; set; }
        public StationViewModel(BLApi.IBL bl, BO.BaseStationForList station)
        {
            this.bl = bl;
            BaseStation = new PO.StationModel(station.Id, station.Name, null, station.CaughtChargeSlots, null);
            Cancel = new(Button_ClickCancel, null);
            Add = new(Button_ClickAdd, null);
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

        }
    }
}
