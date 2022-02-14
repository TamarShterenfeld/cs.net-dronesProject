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

        /// <summary>
        /// update parcel
        /// </summary>
        /// <param name="parcelId">parcel's id</param>
        public void UpdateParcel(int parcelId)
        {
            DeleteParcel(parcelId);
            AddParcel(parcelId);
        }

        /// <summary>
        /// delete parcel
        /// </summary>
        /// <param name="parcelId">parcel's id</param>
        public void DeleteParcel(int parcelId)
        {
            var updatedParcel = Parcels.FirstOrDefault(s => s.ParcelId == parcelId);
            Parcels.Remove(updatedParcel);
        }

        /// <summary>
        /// add parcel
        /// </summary>
        /// <param name="parcelId">parcel's id</param>
        public void AddParcel (int parcelId)
        {
            Parcels.Add(bl.GetParcelForList(parcelId));
        }
        

    }
}
