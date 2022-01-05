using System;
using System.Collections.Generic;
using System.ComponentModel;
using static PL.PO.POConverter;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL.PO
{
    public class Station : INotifyPropertyChanged
    {
        BLApi.IBL bl;
        public event PropertyChangedEventHandler PropertyChanged;
        private int id;
        Location location = new();
        public int Id
        {
            get=> id;
            set
            {
                id = value;
                PropertyChanged?.Invoke(this,new PropertyChangedEventArgs(nameof(Id)));
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

        public Location Location
        {
            get
            {
                return location;
            }
            set
            {
                location = value;
            }
        }

        private int chargeSlots;

        public int ChargeSlots
        {
            get => chargeSlots;
            set
            {
                chargeSlots = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ChargeSlots)));
            }
        }
        public List<DroneInCharging> DroneCharging { get; set; }

        public Station(BLApi.IBL bl,BO.BaseStationForList station)
        {
            BO.BaseStation CurStation = bl.GetBLBaseStation(station.Id);
            this.bl = bl;
            Id = station.Id; Name = station.Name; Location = LocationBOTOPO(CurStation.Location); ChargeSlots = station.AvailableChargeSlots + station.CaughtChargeSlots ; DroneCharging = (List<PO.DroneInCharging>)DroneInChargingListBoToPo(CurStation.DroneCharging);
        }

        // default constructor
        public Station() { }
        

    }
}
