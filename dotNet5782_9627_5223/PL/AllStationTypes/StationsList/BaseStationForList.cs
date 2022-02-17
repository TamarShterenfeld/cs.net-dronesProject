using System;
using System.Collections.Generic;
using System.ComponentModel;
using static PL.PO.POConverter;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using Microsoft.VisualBasic;
using System.Diagnostics;

namespace PL.PO
{

    /// <summary>
    /// the class BaseStationForList contains all the baseStation's details
    /// that we want to show to the client.
    /// </summary>
    public class BaseStationForList:INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private int id;
        public int Id
        {
            get => id;
            set
            {
                id = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Id)));
            }
        }
        private string name;
        public string Name
        {
            get => name;
            set
            {
                name = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Name)));
            }
        }

        private int availableChargeSlots;

        public int AvailableChargeSlots
        {
            get => availableChargeSlots;
            set
            { 
                availableChargeSlots = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(AvailableChargeSlots)));
            }
        }

        private int caughtChargeSlots;
        public int CaughtChargeSlots
        {
            get => caughtChargeSlots;
            set
            {
                caughtChargeSlots = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CaughtChargeSlots)));
            }
        }

        /// <summary>
        /// a constructor with parameters
        /// </summary>
        /// <param name="id">base station's id</param>
        /// <param name="name">base station's name</param>
        /// <param name="availableChargeSlots">available charge slots in the base station</param>
        /// <param name="caughtChargeSlots">caught charge slots in the base station</param>
        public BaseStationForList(BO.BaseStationForList stations)
        {
            Id = stations.Id; Name = stations.Name; AvailableChargeSlots = stations.AvailableChargeSlots; CaughtChargeSlots = stations.CaughtChargeSlots;
        }

        /// <summary>
        /// default constructor
        /// </summary>
        public BaseStationForList() { }

        /// <summary>
        /// override ToString function.
        /// </summary>
        /// <returns>description of the BaseStationForList object</returns>
        public override string ToString()
        {
            return $"id: {Id} \n" +
                   $"name: {Name} \n" +
                   $"number of free charge slots: {availableChargeSlots}\n" +
                   $"number of caught charge slots: {caughtChargeSlots}";
        }
    }


}
