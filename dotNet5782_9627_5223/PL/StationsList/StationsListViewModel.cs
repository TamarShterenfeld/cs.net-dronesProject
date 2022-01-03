using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using BO;

namespace PL.BaseStations
{
    class StationsListViewModel:INotifyPropertyChanged
    {
        BLApi.IBL bl;
        public StationsListViewModel(BLApi.IBL bl)
        {
            this.bl = bl;
            Cancel = new(Button_ClickCancel, null);
            Add = new(Button_ClickAdd, null);
            Options = new List<string>() { "All BaseStations", "Group By Free ChargeSlots" };
            AllStations = Button_AllStations();
        }
        public RelayCommand Cancel { get; set; }
        public RelayCommand Add { get; set; }
        public List<string> Options { get; set; }
        private IEnumerable<BaseStationForList> allStations;
        public IEnumerable<BaseStationForList> AllStations 
        { 
            get => allStations; 
            set
            { 
                allStations = value;
                PropertyChanged?.Invoke(this,new PropertyChangedEventArgs(nameof(AllStations)));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        //---------------------------------Stations's Methods------------------------------
        /// <summary>
        /// the function treats the event of clicking on the button 'Cancel'.
        /// </summary>
        /// <param name="sender">the invoking object</param>
        /// <param name="e">the event</param>
        private void Button_ClickCancel(object sender)
        {
            (sender as Window).Close();
        }

        /// <summary>
        /// the function treats the event of clicking on the button 'Add'.
        /// </summary>
        private void Button_ClickAdd(object sender)
        {
            new StationView(bl).Show();
        }

        private IEnumerable<BaseStationForList> Button_AllStations()
        {
            return bl.GetBaseStationList();
        }

        // לבדוק אם ממומש בצורה טובה
        private IEnumerable<BaseStationForList> Button_GroupByChargeSlots()
        {
            var q = bl.GetBaseStationList().GroupBy(s => s.AvailableChargeSlots).ToList().ToList();
            return (IEnumerable<BaseStationForList>)q;
        }
    }
}
