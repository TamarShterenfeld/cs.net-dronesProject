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
    public class Station : INotifyPropertyChanged
    {
        #region PrivateFields
        BLApi.IBL bl;
        public event PropertyChangedEventHandler PropertyChanged;
        private int id;
        Location location = new();
        private string name;
        private int chargeSlots;
        List<DroneInCharging> droneCharging;
        #endregion

        #region Properties
        public int Id
        {
            get => id;
            set
            {
                id = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Id)));
            }
        }
        public string Name
        {
            get => name;
            set
            {
                name = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Name)));
            }
        }
        public Location Location
        {
            get =>location;
            set   
            {
                location = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Location)));
            }
        }
        public int ChargeSlots
        {
            get => chargeSlots;
            set
            {
                chargeSlots = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ChargeSlots)));
            }
        }
        public bool IsDeleted { get; set; }
        #endregion

        #region Constructors
        public List<DroneInCharging> DroneCharging
        {
            get => droneCharging;
            set
            {
                droneCharging = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(DroneCharging)));
            }
        }
        public Station(BLApi.IBL bl, PO.BaseStationForList station)
        {
            BO.BaseStation CurStation = bl.GetBLBaseStation(station.Id);
            this.bl = bl;
            Id = station.Id; Name = station.Name; Location = LocationBOTOPO(CurStation.Location);
            ChargeSlots = station.AvailableChargeSlots + station.CaughtChargeSlots; IsDeleted = CurStation.IsDeleted;
            DroneCharging = (List<PO.DroneInCharging>)DroneInChargingListBoToPo(CurStation.DroneCharging);
        }

        // default constructor
        public Station() { }
        #endregion

    }
}
