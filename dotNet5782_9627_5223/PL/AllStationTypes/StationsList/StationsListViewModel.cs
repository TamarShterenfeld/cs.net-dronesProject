using BO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Data;

namespace PL
{
    public class StationsListViewModel : INotifyPropertyChanged
    {
        #region PrivateFields
        BLApi.IBL bl;
        PropertyGroupDescription allStationsGroupDescription;
        SortDescription allStationsSortFree;
        SortDescription allStationsSortId;
        private string selectedFilter;
        List<string> options;
        #endregion

        #region Properties
        public RelayCommand Cancel { get; set; }
        public RelayCommand Add { get; set; }
        public RelayCommand LeftDoubleClick { get; set; }
        public List<string> Options
        {
            get => options;
            set
            {
                options = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Options)));
            }
        }
        public ListCollectionView AllStations { get; set; }
      
        public string SelectedFilter
        {
            get => selectedFilter;
            set
            {
                selectedFilter = value;
                if (value == Options[0])
                    Button_AllStations();
                else Button_GroupByChargeSlots();
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SelectedFilter)));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Constructors
        public StationsListViewModel(BLApi.IBL bl)
        {
            this.bl = bl;
            allStationsGroupDescription = new PropertyGroupDescription(nameof(BO.BaseStationForList.AvailableChargeSlots));
            allStationsSortFree = new(nameof(BO.BaseStationForList.AvailableChargeSlots), ListSortDirection.Ascending);
            allStationsSortId = new(nameof(BO.BaseStationForList.Id), ListSortDirection.Ascending);
            Cancel = new(Button_ClickCancel, null);
            Add = new(Button_ClickAdd, null);
            Options = new List<string>() { "All BaseStations", "Group By Free ChargeSlots" };
            AllStations = new ListCollectionView(ListsModel.Instance.Stations);
            Button_AllStations();
            LeftDoubleClick = new(DroneListView_MouseDoubleClick, null);
        }
        #endregion

        #region Buttons_Events

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
                new StationView(new StationViewModel(bl)).Show();
            }

            private void DroneListView_MouseDoubleClick(object sender)
            {
                new StationView(new StationViewModel(bl, sender as PO.BaseStationForList)).Show();
            }

            private void Button_AllStations()
            {
                AllStations.GroupDescriptions.Remove(allStationsGroupDescription);
                AllStations.SortDescriptions.Remove(allStationsSortFree);
                AllStations.SortDescriptions.Add(allStationsSortId);
            }

            private void Button_GroupByChargeSlots()
            {
                AllStations.GroupDescriptions.Add(allStationsGroupDescription);
                AllStations.SortDescriptions.Remove(allStationsSortId);
                AllStations.SortDescriptions.Add(allStationsSortFree);
            }


            #endregion

    }
}
