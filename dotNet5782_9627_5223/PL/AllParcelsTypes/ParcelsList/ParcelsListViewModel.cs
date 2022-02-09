using BO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using static PL.PO.POConverter;

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
        private PO.POConverter.GroupOptions selectedGroup;
        private PO.POConverter.SortOptions selectedSort;
        public RelayCommand Cancel { get; set; }
        public RelayCommand Add { get; set; }
        public RelayCommand LeftDoubleClick { get; set; }
        public ListCollectionView AllParcels { get; set; }

        public ListCollectionView GroupOptions { get; set; }
        public ListCollectionView SortOptions { get; set; }

        //PropertyGroupDescription groupDescription = new PropertyGroupDescription("AvailableChargeSlots");
        //SortDescription sortFree = new("AvailableChargeSlots", 0);
        //SortDescription sortId = new("Id", 0);



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
            Cancel = new(Button_ClickCancel, null);
            Add = new(Button_ClickAdd, null);
            AllParcels = new ListCollectionView(ListsModel.Instance.Parcels);
            Button_AllParcels();
            LeftDoubleClick = new(DroneListView_MouseDoubleClick, null);
        }
        public GroupOptions SelectedGroup
        {
            get => selectedGroup;
            set
            {
                selectedGroup = value;
                AllParcels.GroupDescriptions.Clear();
                if (value == PL.PO.POConverter.GroupOptions.GroupBySender)
                {
                    Button_GroupBySender();
                    AllParcels.GroupDescriptions.Add(allParcels_groupSender);
                }
                else
                {
                    Button_GroupByTarget();
                    AllParcels.GroupDescriptions.Add(allParcels_groupSender);
                }
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(selectedGroup)));
            }
        }

      
        
        public SortOptions SelectedSort
        {
            get => selectedSort;
            set
            {
                selectedSort = value;
                AllParcels.SortDescriptions.Clear();
                if (value == PO.POConverter.SortOptions.SortedId)
                    Button_AllParcels();
                else
                {
                    if (value == PO.POConverter.SortOptions.SortedStatus)
                        Button_SortByStatus();
                    else 
                    {
                        if (value == PO.POConverter.SortOptions.SortedWeight)
                            Button_SortByWeight();
                        else
                        {
                            if (value == PO.POConverter.SortOptions.SortedPriority)
                                Button_SortByPriority();
                        }
                    }
                   
                }
                
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
            AllParcels.SortDescriptions.Add(allParcelsSortId);
        }

        private void Button_GroupBySender()
        {
            AllParcels.GroupDescriptions.Add(allParcels_groupSender);
        }
        private void Button_GroupByTarget()
        {
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
        private void Button_ClickCancel(object sender)
        {
            (sender as Window).Close();
        }

        private void Button_ClickAdd(object sender)
        {
            new ParcelView(new ParcelViewModel(bl)).Show();
        }

    }
}
