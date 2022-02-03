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
        SortDescription allParcelsSortStatus;
        SortDescription allParcelsSortWeight;
        SortDescription allParcelsSortPriority;
        SortDescription allParcelsSortId;
        public RelayCommand Cancel { get; set; }
        public RelayCommand Add { get; set; }
        public RelayCommand LeftDoubleClick { get; set; }
        public ListCollectionView AllParcels { get; set; }

        public ListCollectionView GroupOptions { get; set; }
        public ListCollectionView SortOptions { get; set; }
        //PropertyGroupDescription groupDescription = new PropertyGroupDescription("AvailableChargeSlots");
        //SortDescription sortFree = new("AvailableChargeSlots", 0);
        //SortDescription sortId = new("Id", 0);

        private PropertyGroupDescription selectedGroup;


        public ParcelsListViewModel(BLApi.IBL bl)
        {
            this.bl = bl;
            GroupOptions = new ListCollectionView(Enum.GetValues(typeof(PO.POConverter.GroupOptions)));
            SortOptions = new ListCollectionView(Enum.GetValues(typeof(PO.POConverter.SortOptions)));
            allParcels_groupTarget = new PropertyGroupDescription(nameof(BO.ParcelForList.TargetId));
            allParcels_groupSender = new PropertyGroupDescription(nameof(BO.ParcelForList.SenderId));
            allParcelsSortStatus = new(nameof(BO.ParcelForList.Status), ListSortDirection.Ascending);
            allParcelsSortWeight = new(nameof(BO.ParcelForList.Weight), ListSortDirection.Ascending);
            allParcelsSortPriority = new(nameof(BO.ParcelForList.Priority), ListSortDirection.Ascending);
            allParcelsSortId = new(nameof(BO.ParcelForList.ParcelId), ListSortDirection.Ascending);
            Cancel = new(ButtonCancel_Click, null);
            Add = new(AddParcel, null);
            AllParcels = new ListCollectionView(ListsModel.Instance.Parcels);
            Button_AllParcels();
            LeftDoubleClick = new(DroneListView_MouseDoubleClick, null);
        }
        public PropertyGroupDescription SelectedGroup
        {
            get => selectedGroup;
            set
            {
                selectedGroup = value;
                AllParcels.GroupDescriptions.Clear();
                AllParcels.GroupDescriptions.Add(value);
                if (value == allParcels_groupSender)
                    Button_GroupBySender();
                else Button_GroupByTarget();
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(selectedGroup)));
            }
        }

        private SortDescription selectedSort;
        
        public SortDescription SelectedSort
        {
            get => selectedSort;
            set
            {
                selectedSort = value;
                AllParcels.SortDescriptions.Clear();
                AllParcels.SortDescriptions.Add(value);
                if(value == allParcelsSortId)
                    Button_AllParcels();
                else if(value == allParcelsSortStatus)
                       Button_SortByStatus();
                else if(value == allParcelsSortWeight)
                    Button_SortByWeight();
                else if(value == allParcelsSortPriority)
                    Button_SortByPriority();
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(selectedSort)));
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
            AllParcels.SortDescriptions.Remove(allParcelsSortStatus);
            AllParcels.SortDescriptions.Remove(allParcelsSortWeight);
            AllParcels.SortDescriptions.Remove(allParcelsSortPriority);
            AllParcels.SortDescriptions.Add(allParcelsSortId);
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
            AllParcels.SortDescriptions.Add(allParcelsSortStatus);
        }

        private void Button_SortByWeight()
        {
            AllParcels.SortDescriptions.Add(allParcelsSortWeight);
        }
        private void Button_SortByPriority()
        {
            AllParcels.SortDescriptions.Add(allParcelsSortPriority);
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
