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
        BLApi.IBL bl;
        PropertyGroupDescription allStations_groupDescription;
        SortDescription allStations_sortFree;
        SortDescription allStations_sortId;

        public StationsListViewModel(BLApi.IBL bl)
        {
            this.bl = bl;
            allStations_groupDescription = new PropertyGroupDescription(nameof(BO.BaseStationForList.AvailableChargeSlots));
            allStations_sortFree = new(nameof(BO.BaseStationForList.AvailableChargeSlots), ListSortDirection.Ascending);
            allStations_sortId = new(nameof(BO.BaseStationForList.Id), ListSortDirection.Ascending);

            Cancel = new(Button_ClickCancel, null);
            Add = new(Button_ClickAdd, null);
            Options = new List<string>() { "All BaseStations", "Group By Free ChargeSlots" };
            AllStations = new ListCollectionView(ListsModel.Instance.Stations);
            Button_AllStations();
            LeftDoubleClick = new(DroneListView_MouseDoubleClick, null);
        }
        public RelayCommand Cancel { get; set; }
        public RelayCommand Add { get; set; }
        public RelayCommand LeftDoubleClick { get; set; }
        public List<string> Options { get; set; }
        public ListCollectionView AllStations { get; set; }

        //PropertyGroupDescription groupDescription = new PropertyGroupDescription("AvailableChargeSlots");
        //SortDescription sortFree = new("AvailableChargeSlots", 0);
        //SortDescription sortId = new("Id", 0);

        private string selectedFilter;
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
            new StationView(new StationViewModel(bl, sender as BaseStationForList)).Show();
        }


        private void Button_AllStations()
        {
            AllStations.GroupDescriptions.Remove(allStations_groupDescription);
            AllStations.SortDescriptions.Remove(allStations_sortFree);
            AllStations.SortDescriptions.Add(allStations_sortId);
        }

        private void Button_GroupByChargeSlots()
        {
            AllStations.GroupDescriptions.Add(allStations_groupDescription);
            AllStations.SortDescriptions.Remove(allStations_sortId);
            AllStations.SortDescriptions.Add(allStations_sortFree);
        }

    }
}
