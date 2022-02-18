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
    sealed partial class ListsModel 
    {
        // DroneViewModel Lists
        #region PrivateFields
        ObservableCollection<PL.PO.DroneForList> drones;
        #endregion

        #region Properties
        public ObservableCollection<PL.PO.DroneForList> Drones
        {
            get => drones;
            private set
            {
                drones = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Drones)));
            }
        }

        #endregion

        #region CRUD_Methods
        /// <summary>
        /// update drone
        /// </summary>
        /// <param name="droneId">drone's id</param>
        public void UpdateDrone(int droneId)
        {
            var droneForList = Drones.FirstOrDefault(drone => drone.Id == droneId);
            int index = Drones.IndexOf(droneForList);
            DeleteDrone(droneForList.Id);
            Drones.Insert(index,DroneForListBOToPO(bl.GetDroneForList(droneId)));
        }

        /// <summary>
        /// delete drone
        /// </summary>
        /// <param name="droneId">drone's id</param>
        public void DeleteDrone(int droneId)
        {
            var updatedDrone = Drones.FirstOrDefault(s => s.Id == droneId);
            Drones.Remove(updatedDrone);
        }

        /// <summary>
        /// add drone
        /// </summary>
        /// <param name="droneId">drone's id</param>
        public void AddDrone(int droneId)
        {
            Drones.Add(PO.POConverter.DroneForListBOToPO(bl.GetDroneForList(droneId)));
        }
        #endregion
    }
}
