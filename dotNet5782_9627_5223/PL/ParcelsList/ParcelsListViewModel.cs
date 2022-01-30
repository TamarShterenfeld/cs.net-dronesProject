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
        PropertyGroupDescription allParcels_groupTarget;
        PropertyGroupDescription allParcels_groupSender;
        SortDescription allParcels_sortStatus;
        SortDescription allParcels_sortWeight;
        SortDescription allParcels_sortPriority;
        SortDescription allParcels_sortId;

        public ParcelsListViewModel(BLApi.IBL bl)
        {
            this.bl = bl;
            allParcels_groupTarget = new PropertyGroupDescription(nameof(BO.ParcelForList.TargetId));
            allParcels_groupSender = new PropertyGroupDescription(nameof(BO.ParcelForList.SenderId));
            allParcels_sortStatus = new(nameof(BO.ParcelForList.Status), ListSortDirection.Ascending);
            allParcels_sortWeight = new(nameof(BO.ParcelForList.Weight), ListSortDirection.Ascending);
            allParcels_sortPriority = new(nameof(BO.ParcelForList.Priority), ListSortDirection.Ascending);
            allParcels_sortId = new(nameof(BO.ParcelForList.ParcelId), ListSortDirection.Ascending);
            Cancel = new(ButtonCancel_Click, null);
            Add = new(AddParcel, null);
            GroupOptions = new List<PropertyGroupDescription>() { "Group By Sender", "Group By Target" };
            SortOptions = new List<string>() { "Status", "Weight", "Priority" };
            AllParcels = new ListCollectionView(ListsModel.Instance.Parcels);
            Button_AllParcels();
            LeftDoubleClick = new(DroneListView_MouseDoubleClick, null);
        }
        public RelayCommand Cancel { get; set; }
        public RelayCommand Add { get; set; }
        public RelayCommand LeftDoubleClick { get; set; }
        public List<string> SortOptions { get; set; }
        public List<string> GroupOptions { get; set; }
        public ListCollectionView AllParcels { get; set; }

        //PropertyGroupDescription groupDescription = new PropertyGroupDescription("AvailableChargeSlots");
        //SortDescription sortFree = new("AvailableChargeSlots", 0);
        //SortDescription sortId = new("Id", 0);

        private string selectedGroup;
        public PropertyGroupDescription SelectedGroup
        {
            get => selectedGroup;
            set
            {
                selectedGroup = value;
                AllParcels.GroupDescriptions.Clear();
                AllParcels.GroupDescriptions.Add(value);
                if (value == GroupOptions[0])
                    Button_GroupBySender();
                else Button_GroupByTarget();
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(selectedGroup)));
            }
        }


        private PropertyGroupDescription selectedSort;
        public PropertyGroupDescription SelectedSort
        {
            get => selectedSort;
            set
            {
                selectedSort = value;
                switch (value)
                {
                    case SortOptions[0]:
                        {
                            break;
                        }
                }
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(selectedGroup)));
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
            new ParcelView(new ParcelViewModel(sender as BO.ParcelForList, bl)).Show();
        }


        private void Button_AllParcels()
        {
            AllParcels.GroupDescriptions.Remove(allParcels_groupTarget);
            AllParcels.GroupDescriptions.Remove(allParcels_groupSender);
            AllParcels.SortDescriptions.Remove(allParcels_sortStatus);
            AllParcels.SortDescriptions.Remove(allParcels_sortWeight);
            AllParcels.SortDescriptions.Remove(allParcels_sortPriority);
            AllParcels.SortDescriptions.Add(allParcels_sortId);
        }

        private void Button_GroupBySender()
        {
            AllParcels.GroupDescriptions.Remove(allParcels_groupTarget);
            AllParcels.GroupDescriptions.Add(allParcels_groupSender);
        }
        private void Button_GroupByTarget()
        {
            AllParcels.GroupDescriptions.Remove(allParcels_groupSender);
            AllParcels.GroupDescriptions.Add(allParcels_groupTarget);
        }

        private void Button_SortByStatus()
        {
            AllParcels.SortDescriptions.Add(allParcels_sortStatus);
        }

        private void Button_SortByWeight()
        {
            AllParcels.SortDescriptions.Add(allParcels_sortWeight);
        }
        private void Button_SortByPriority()
        {
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
