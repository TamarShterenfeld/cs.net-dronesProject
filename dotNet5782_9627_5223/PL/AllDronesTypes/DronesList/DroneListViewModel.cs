﻿using BO;
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
                DronesListView.Filter = DroneFilter;
            }
        }      
        public string SelectedWeightFilter
        {
            get => selectedWeightFilter;
            set
            {
                selectedWeightFilter = value;
                DronesListView.Filter = DroneFilter;
            }
        }
        public ListCollectionView Weights { get; private set; }
        public ListCollectionView Statuses { get; private set; }
        public ListCollectionView DronesListView { get; set; }
        public RelayCommand Cancel { get; set; }
        public RelayCommand Add { get; set; }
        public RelayCommand LeftDoubleClick { get; set; }
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
            LeftDoubleClick = new(DroneListView_MouseDoubleClick, null);
        }
        #endregion

        #region ButtonEvents
        //---------------------------------ButtonsEvents------------------------------
        private bool DroneFilter(object obj)
        {
            if (obj is PO.DroneForList drone)
            {
                return (SelectedStatusFilter == null || drone.Status.ToString() == SelectedStatusFilter)
                    && (SelectedWeightFilter == null || drone.MaxWeight.ToString() == SelectedWeightFilter);
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

        private void DroneListView_MouseDoubleClick(object sender)
        {
            new DroneView(new DroneViewModel(bl, sender as PO.DroneForList)).Show();
        }
        #endregion
    }
}
