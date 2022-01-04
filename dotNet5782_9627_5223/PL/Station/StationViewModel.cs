using System.ComponentModel;
using System.Windows;

namespace PL
{
    public class StationViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        BLApi.IBL bl;
        public PO.Station BaseStation { get; set; }
        public bool EnableUpdate { get; set; }
        public string State { get; set; }
        public RelayCommand Cancel { get; set; }
        public RelayCommand AddOrUpdate { get; set; }
        
        public StationViewModel(BLApi.IBL bl, BO.BaseStationForList station): this(bl)
        {
            BaseStation = new(bl, station);
            AddOrUpdate = new(Button_ClickUpdate, null);
            EnableUpdate = false;
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
        private void Button_ClickUpdate(object sender)
        {
            
        }
    }
}
