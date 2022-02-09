using BO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Data;

namespace PL
{
    public class DroneListViewModel : INotifyPropertyChanged
    {
        BLApi.IBL bl;
        //PropertyGroupDescription allStationsGroupDescription;
        //SortDescription allStationsSortFree;
        //SortDescription allStationsSortId;

        public DroneListViewModel(BLApi.IBL bl)
        {
            this.bl = bl;
            //allStationsGroupDescription = new PropertyGroupDescription(nameof(BO.BaseStationForList.AvailableChargeSlots));
            //allStationsSortFree = new(nameof(BO.BaseStationForList.AvailableChargeSlots), ListSortDirection.Ascending);
            //allStationsSortId = new(nameof(BO.BaseStationForList.Id), ListSortDirection.Ascending);

            Cancel = new(Button_ClickCancel, null);
            Add = new(Button_ClickAdd, null);
            FilterCategory = new List<string>{"status", "weight"};
            SelectFilter = new(comboBox_SelectFilter, null);
            AllDrones = new ListCollectionView(ListsModel.Instance.Drones);
            statusFilter = Enum.GetValues(typeof(PO.POConverter.DroneStatuses))
                            .Cast<PO.POConverter.DroneStatuses>()
                            .ToList().ConvertAll(f => f.ToString()); 
            weightFilter = Enum.GetValues(typeof(PO.POConverter.WeightCategories))
                            .Cast<PO.POConverter.WeightCategories>()
                            .ToList().ConvertAll(f => f.ToString());
            //Button_AllStations();
            LeftDoubleClick = new(DroneListView_MouseDoubleClick, null);
        }
        public RelayCommand Cancel { get; set; }
        public RelayCommand Add { get; set; }
        public RelayCommand LeftDoubleClick { get; set; }
        public RelayCommand SelectFilter { get; set; }
        public List<string> FilterCategory { get; set; }
        public List<string> StatusOrWeightFilter { get; set; }
        private List<string> statusFilter { get; set; } 
        private List<string> weightFilter { get; set; }
        public ListCollectionView AllDrones { get; set; }

        private string selectedFilter;
        public string SelectedFilter
        {
            get => selectedFilter;
            set
            {
                selectedFilter = value;
                //StatusOrWeightFilter = value == FilterCategory[0] ?
                //     (List<string>)Enum.GetValues(typeof(PO.POConverter.DroneStatuses)).Cast<PO.POConverter.DroneStatuses>() :
                //     (List<string>)Enum.GetValues(typeof(PO.POConverter.WeightCategories)).Cast<PO.POConverter.WeightCategories>();
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
            new DroneView(new DroneViewModel(bl)).Show();
        }
        private void DroneListView_MouseDoubleClick(object sender)
        {
            new DroneView(new DroneViewModel(bl, sender as DroneForList)).Show();
        }
        private void comboBox_SelectFilter(object sender)
        {
            StatusOrWeightFilter = (string)sender == FilterCategory[0] ? statusFilter : weightFilter;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(StatusOrWeightFilter)));
        }



        //private void Button_AllStations()
        //{
        //    AllStations.GroupDescriptions.Remove(allStationsGroupDescription);
        //    AllStations.SortDescriptions.Remove(allStationsSortFree);
        //    AllStations.SortDescriptions.Add(allStationsSortId);
        //}

        //private void Button_GroupByChargeSlots()
        //{
        //    AllStations.GroupDescriptions.Add(allStationsGroupDescription);
        //    AllStations.SortDescriptions.Remove(allStationsSortId);
        //    AllStations.SortDescriptions.Add(allStationsSortFree);
        //}

    }
}
