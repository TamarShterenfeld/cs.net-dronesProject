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
        string selectedStatusFilter;
        string selectedWeightFilter;
        IList<string> nullString = new List<string>() { "" };
        IList<string> statuses = Enum.GetNames(typeof(POConverter.DroneStatuses));
        IList<string> weights = Enum.GetNames(typeof(POConverter.WeightCategories));
        #endregion

        #region Properties
        public event PropertyChangedEventHandler PropertyChanged;
        public string SelectedStatusFilter
        {
            get => selectedStatusFilter;
            set
            {
                selectedStatusFilter = value;
                DronesListView.Refresh();
            }
        }
        public string SelectedWeightFilter
        {
            get => selectedWeightFilter;
            set
            {
                selectedWeightFilter = value;
                DronesListView.Refresh();
            }
        }
        public ListCollectionView Weights { get; private set; }
        public ListCollectionView Statuses { get; private set; }
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
            Statuses = new ListCollectionView(nullString.Concat<string>(statuses).ToList());
            Weights = new ListCollectionView(nullString.Concat<string>(weights).ToList());
            DisplayDroneViewCommand = new(DisplayDroneView, null);
            DronesListView.Filter = DroneFilter;
        }
        #endregion

        #region ButtonEvents
        //---------------------------------ButtonsEvents------------------------------
        private bool DroneFilter(object obj)
        {
            if (obj is PO.DroneForList drone)
            {
                return (SelectedStatusFilter == null || SelectedStatusFilter == "" || drone.Status.ToString() == SelectedStatusFilter)
                    && (SelectedWeightFilter == null || SelectedWeightFilter == "" || drone.MaxWeight.ToString() == SelectedWeightFilter);
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
