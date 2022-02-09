using BO;
using PL.PO;
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
        public event PropertyChangedEventHandler PropertyChanged;

        BLApi.IBL bl;
        POConverter.DroneStatuses selectedStatusFilter;
        private POConverter.WeightCategories selectedWeightFilter;

        public DroneListViewModel(BLApi.IBL bl)
        {
            this.bl = bl;
            Cancel = new(Button_ClickCancel, null);
            Add = new(Button_ClickAdd, null);
            DronesListView = new ListCollectionView(ListsModel.Instance.Drones);
            DronesListView.Filter = DroneFilter;
            StatusFilter = new(Enum.GetValues(typeof(PO.POConverter.DroneStatuses)).Cast<PO.POConverter.DroneStatuses>().ToList());
            WeightFilter = new(Enum.GetValues(typeof(PO.POConverter.WeightCategories)).Cast<PO.POConverter.WeightCategories>().ToList());
            DisplayDroneViewCommand = new(DisplayDroneView, null);
        }

        private bool DroneFilter(object obj)
        {
            if (obj is PO.DroneForList drone)
            {
                return (SelectedStatusFilter == POConverter.DroneStatuses.None || drone.Status == SelectedStatusFilter)
                    && (SelectedWeightFilter == POConverter.WeightCategories.None || drone.MaxWeight == SelectedWeightFilter);
            }
            return false;
        }

        public RelayCommand Cancel { get; set; }
        public RelayCommand Add { get; set; }
        public RelayCommand DisplayDroneViewCommand { get; set; }
        public ObservableCollection<POConverter.DroneStatuses> StatusFilter { get; private set; }
        public POConverter.DroneStatuses SelectedStatusFilter
        {
            get => selectedStatusFilter;
            set
            {
                selectedStatusFilter = value;
                DronesListView.Refresh();
            }
        }
        public ObservableCollection<POConverter.WeightCategories> WeightFilter { get; private set; }
        public POConverter.WeightCategories SelectedWeightFilter
        {
            get => selectedWeightFilter;
            set
            {
                selectedWeightFilter = value;
                DronesListView.Refresh();
            }
        }
        public ListCollectionView DronesListView { get; set; }


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
        private void DisplayDroneView(object sender)
        {
            new DroneView(new DroneViewModel(bl, sender as PL.PO.DroneForList)).Show();
        }
    }
}
