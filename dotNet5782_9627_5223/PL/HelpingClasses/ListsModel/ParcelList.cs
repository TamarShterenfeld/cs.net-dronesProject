using BO;
using Singleton;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using static PL.PO.POConverter;
using System.Threading.Tasks;
using System.Windows.Data;

namespace PL
{
    sealed partial class ListsModel : Singleton<ListsModel>, INotifyPropertyChanged
    {
        #region PrivateFields
        ObservableCollection<PO.ParcelForList> parcels;
        #endregion

        #region Properties
        public ObservableCollection<PO.ParcelForList> Parcels
        {
            get => parcels;
            private set
            {
                parcels = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Parcels)));
            }
        }

        #endregion

        #region CRUD_Methods

        /// <summary>
        /// update parcel
        /// </summary>
        /// <param name="parcelId">parcel's id</param>
        public void UpdateParcel(int parcelId)
        {
            PO.ParcelForList parcel = Parcels.FirstOrDefault(item => item.ParcelId == parcelId);
            int index = Parcels.IndexOf(parcel);
            DeleteParcel(parcel.ParcelId);
            Parcels.Insert(index, ParcelForListBOToPO(bl.GetParcelForList(parcelId)));
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
        public void AddParcel(int parcelId)
        {
            Parcels.Add(ParcelForListBOToPO(bl.GetParcelForList(parcelId)));
        }

        #endregion
    }
}
