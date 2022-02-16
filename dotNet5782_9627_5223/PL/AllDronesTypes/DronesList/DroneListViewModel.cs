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
      
        #region privateFields
        BLApi.IBL bl;
        POConverter.DroneStatuses selectedStatusFilter;
        private POConverter.WeightCategories selectedWeightFilter;
        #endregion

        #region Properties
        public event PropertyChangedEventHandler PropertyChanged;
        public POConverter.DroneStatuses SelectedStatusFilter
        {
            get => selectedStatusFilter;
            set
            {
                PropertyChanged ?.Invoke(this, new PropertyChangedEventArgs(nameof(SelectedStatusFilter)));
                selectedStatusFilter = value;
                DronesListView.Refresh();
            }
        }      
        public POConverter.WeightCategories SelectedWeightFilter
        {
            get => selectedWeightFilter;
            set
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SelectedWeightFilter)));
                selectedWeightFilter = value;
                DronesListView.Refresh();
            }
        }
        public ObservableCollection<POConverter.WeightCategories> WeightFilters { get; private set; }
        public ObservableCollection<POConverter.DroneStatuses> StatusFilters { get; private set; }
        public ListCollectionView DronesListView { get; set; }
        public RelayCommand Cancel { get; set; }
        public RelayCommand Add { get; set; }
        public RelayCommand DisplayDroneViewCommand { get; set; }
       

        #endregion

        #region Constructor
        public DroneListViewModel(BLApi.IBL bl)
        {
            this.bl = bl;
            Cancel = new(Button_ClickCancel, null);
            Add = new(Button_ClickAdd, null);
            DronesListView = new ListCollectionView(ListsModel.Instance.Drones);
            StatusFilters = new(Enum.GetValues(typeof(PO.POConverter.DroneStatuses)).Cast<PO.POConverter.DroneStatuses>().ToList());
            WeightFilters = new(Enum.GetValues(typeof(PO.POConverter.WeightCategories)).Cast<PO.POConverter.WeightCategories>().ToList());
            DisplayDroneViewCommand = new(DisplayDroneView, null);
        }
        #endregion

        #region ButtonEvents
        //---------------------------------ButtonsEvents------------------------------
        private bool DroneFilter(object obj)
        {
            if (obj is PO.DroneForList drone)
            {
                return (SelectedStatusFilter.ToString() == "" || drone.Status == SelectedStatusFilter)
                    && (SelectedWeightFilter.ToString() == "" || drone.MaxWeight == SelectedWeightFilter);
            }
            return false;
        }
       
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
        #endregion
    }
}
