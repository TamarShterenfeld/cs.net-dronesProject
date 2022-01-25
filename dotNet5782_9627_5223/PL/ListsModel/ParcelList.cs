using BO;
using Singleton;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace PL
{
    sealed partial class ListsModel : Singleton<ListsModel>, INotifyPropertyChanged
    {
        // ParcelsViewModel Lists
 
        ObservableCollection<ParcelForList> parcels;

        

        public ObservableCollection<BO.ParcelForList> Parcels
        {
            get => parcels;
            private set
            {
                parcels = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Parcels)));
            }
        }

        public void UpdateParcel(int parcelId)
        {
            DeleteParcel(parcelId);
            AddParcel(parcelId);
        }

        public void DeleteParcel(int parcelId)
        {
            var updatedParcel = Parcels.FirstOrDefault(s => s.ParcelId == parcelId);
            Parcels.Remove(updatedParcel);
        }
        public void AddParcel (int parcelId)
        {
            Stations.Add(bl.GetBaseStationForList(parcelId));
        }
        

    }
}
