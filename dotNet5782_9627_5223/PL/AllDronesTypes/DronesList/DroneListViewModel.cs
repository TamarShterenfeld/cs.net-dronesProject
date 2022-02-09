using BO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Data;

namespace PL
{
    public class DroneListViewModel : INotifyPropertyChanged
    {
        BLApi.IBL bl;

        public DroneListViewModel(BLApi.IBL bl)
        {
            this.bl = bl;
            Cancel = new(Button_ClickCancel, null);
            Add = new(Button_ClickAdd, null);
            FilterCategory = new List<string>{"status", "weight"};
            SelectFilter = new(comboBox_SelectFilter, null);
            allDrones = new ObservableCollection<PL.PO.DroneForList>(ListsModel.Instance.Drones);
            statusFilter = Enum.GetValues(typeof(PO.POConverter.DroneStatuses)).Cast<PO.POConverter.DroneStatuses>() .ToList().ConvertAll(f => f.ToString()); 
            weightFilter = Enum.GetValues(typeof(PO.POConverter.WeightCategories)).Cast<PO.POConverter.WeightCategories>().ToList().ConvertAll(f => f.ToString());
            SelectSecondFilter = new(comboBox_SelectSecondFilter, null);
            LeftDoubleClick = new(DroneListView_MouseDoubleClick, null);
        }
        public RelayCommand Cancel { get; set; }
        public RelayCommand Add { get; set; }
        public RelayCommand LeftDoubleClick { get; set; }
        public RelayCommand SelectFilter { get; set; }
        public RelayCommand SelectSecondFilter{ get; set; }
        public List<string> FilterCategory { get; set; }
        public List<string> StatusOrWeightFilter { get; set; }
        public List<string> statusFilter { get; set; } 
        private List<string> weightFilter { get; set; }

        public ObservableCollection<PL.PO.DroneForList> AllDrones {get => allDrones; set { allDrones = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(AllDrones))); } }

        private ObservableCollection<PL.PO.DroneForList> allDrones;

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

        /// <summary>
        /// shows full details of a specific drone.
        /// </summary>
        /// <param name="sender">the selected drone</param>
        private void DroneListView_MouseDoubleClick(object sender)
        {
            new DroneView(new DroneViewModel(bl, sender as DroneForList)).Show();
        }

        /// <summary>
        /// shows the menu how to filter the drones' list according to the user's choice: status or weight.
        /// </summary>
        /// <param name="sender">the selected item</param>
        private void comboBox_SelectFilter(object sender)
        {
            StatusOrWeightFilter = (string)sender == FilterCategory[0] ? statusFilter : weightFilter;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(StatusOrWeightFilter)));
        }
        private void comboBox_SelectSecondFilter(object sender)
        {
            AllDrones = allDrones;
            if (StatusOrWeightFilter == statusFilter)
            {
               
                foreach (var item in allDrones)
                {
                    if (item.Status == (PO.POConverter.DroneStatuses)Enum.Parse(typeof(PO.POConverter.DroneStatuses), (string)sender, true))
                    {
                        var curDrone = AllDrones.FirstOrDefault(drone => drone.Id == item.Id);
                        AllDrones.Remove(curDrone);
                    }
                }
            }
            else
            {
                foreach (var item in AllDrones)
                {
                    if (item.MaxWeight == (PO.POConverter.WeightCategories)Enum.Parse(typeof(PO.POConverter.WeightCategories), (string)sender, true))
                    {
                        var curDrone = AllDrones.FirstOrDefault(drone => drone.Id == item.Id);
                        AllDrones.Remove(curDrone);
                    }
                }
            }
           
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
