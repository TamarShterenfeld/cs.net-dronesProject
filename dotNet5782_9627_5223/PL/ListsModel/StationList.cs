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
        // StationViewModel Lists

        /// <summary>
        /// private constructor
        /// </summary>
        /// 
        BLApi.IBL bl;
        ObservableCollection<BaseStationForList> stations;

        private ListsModel()
        {
            bl = BLApi.BLFactory.GetBl();
            Stations = new ObservableCollection<BaseStationForList>(bl.GetBaseStationList().ToList());
        }

        public ObservableCollection<BO.BaseStationForList> Stations
        {
            get => stations;
            private set
            {
                stations = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Stations)));
            }
        }
        //ListCollectionView allStations;
        //public ListCollectionView AllStations
        //{
        //    get => allStations;
        //    set
        //    {
        //        allStations = value;
        //        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(AllStations)));
        //    }
        //}

        public void UpdateStation(int stationId)
        {
            var updatedStation= Stations.FirstOrDefault(s => s.Id == stationId);
            Stations.Remove(updatedStation);
            AddStation(stationId);
        }
        public void AddStation(int stationId)
        {
            Stations.Add(bl.GetBaseStationForList(stationId));
        }
        
        public event PropertyChangedEventHandler PropertyChanged;

    }
}
