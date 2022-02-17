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

        ObservableCollection<PL.PO.DroneForList> drones;
        public ObservableCollection<PL.PO.DroneForList> Drones
        {
            get => drones;
            private set
            {
                drones = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Drones)));
            }
        }

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
    }
}
