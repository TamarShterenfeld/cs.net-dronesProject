using BO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Data;

namespace PL
{
    public class ParcelsListViewModel : INotifyPropertyChanged
    {
        BLApi.IBL bl;
        PropertyGroupDescription allParcels_groupDescription;
        SortDescription allParcels_sortStatus;
        SortDescription allParcels_sortWeight;
        SortDescription allParcels_sortPriority;
        SortDescription allParcels_sortId;

        public ParcelsListViewModel(BLApi.IBL bl)
        {
            this.bl = bl;
            allParcels_groupDescription = new PropertyGroupDescription(nameof(BO.ParcelForList.SenderId));
            allParcels_sortStatus = new(nameof(BO.ParcelForList.Status), ListSortDirection.Ascending);
            allParcels_sortWeight = new(nameof(BO.ParcelForList.Weight), ListSortDirection.Ascending);
            allParcels_sortPriority = new(nameof(BO.ParcelForList.Priority), ListSortDirection.Ascending);
            allParcels_sortId = new(nameof(BO.ParcelForList.ParcelId), ListSortDirection.Ascending);
            Cancel = new(ButtonCancel_Click, null);
            Add = new(AddParcel, null);
            Options = new List<string>() { "Group By Sender", "Group By Target" };
            AllParcels = new ListCollectionView(ListsModel.Instance.Parcels);
            Button_AllParcels();
            LeftDoubleClick = new(DroneListView_MouseDoubleClick, null);
        }
        public RelayCommand Cancel { get; set; }
        public RelayCommand Add { get; set; }
        public RelayCommand LeftDoubleClick { get; set; }
        public List<string> Options { get; set; }
        public ListCollectionView AllParcels { get; set; }

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
                    Button_AllParcels();
                else Button_GroupByStatus();
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
       
        /// <summary>
        /// the function treats the event of clicking on the button 'Add'.
        /// </summary>
      
        private void DroneListView_MouseDoubleClick(object sender)
        {
            new StationView(new StationViewModel(bl, sender as BaseStationForList)).Show();
        }


        private void Button_AllParcels()
        {
            AllParcels.GroupDescriptions.Remove(allParcels_groupDescription);
            AllParcels.SortDescriptions.Remove(allParcels_sortStatus);
            AllParcels.SortDescriptions.Add(allParcels_sortId);
        }

        private void Button_GroupByStatus()
        {
            AllParcels.GroupDescriptions.Add(allParcels_groupDescription);
            AllParcels.SortDescriptions.Remove(allParcels_sortId);
            AllParcels.SortDescriptions.Add(allParcels_sortStatus);
        }

        private void Button_GroupByWeight()
        {
            AllParcels.GroupDescriptions.Add(allParcels_groupDescription);
            AllParcels.SortDescriptions.Remove(allParcels_sortId);
            AllParcels.SortDescriptions.Add(allParcels_sortWeight);
        }
        private void Button_GroupByPriority()
        {
            AllParcels.GroupDescriptions.Add(allParcels_groupDescription);
            AllParcels.SortDescriptions.Remove(allParcels_sortId);
            AllParcels.SortDescriptions.Add(allParcels_sortPriority);
        }
        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            (sender as Window).Close();
        }

        private void AddParcel(object sender, RoutedEventArgs e)
        {
            new ParcelView(new ParcelViewModel(this.bl)).Show();
        }

    }
}
