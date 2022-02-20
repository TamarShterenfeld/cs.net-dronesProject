using System;
using System.Collections.Generic;
using System.ComponentModel;
using static PL.PO.POConverter;
using System.Text;
using System.Threading.Tasks;

namespace PL.PO
{
    /// <summary>
    /// an entity for reports from the simulator
    /// </summary>
    public class UserStage : INotifyPropertyChanged
    {
        private double distance;
        public double Distance
        {
            get => distance;
            set
            {
                distance = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Distance)));
            }
        }
        private bool pickedUp;
        public bool PickedUp
        {
            get => pickedUp;
            set
            {
                pickedUp = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(PickedUp)));
            }
        }

        private DroneStatuses status;

        private bool inWayToMaintenance;
        public bool InWayToMaintenance 
        { 
            get => inWayToMaintenance;
            set
            { 
                inWayToMaintenance = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(InWayToMaintenance)));
            }
        }
        public DroneStatuses Status { get => status; set => status = value; }
        public UserStage(BO.UserStage stage)
        {
            Distance = stage.Distance;
            PickedUp = stage.PickedUp;
            Status = (DroneStatuses)stage.Status;
            InWayToMaintenance = stage.InWayToMaintenance;
        }
        public UserStage() { }
        public override string ToString()
        {
            string stage = "";
            if (Status == DroneStatuses.Shipment)
            {
                if (PickedUp)
                {
                    stage = $"Distance till to target: {Distance}.\nParcel has already been picked up.";
                }
                else
                {
                    stage = $"Distance till sender:{Distance}.\nNot Yet Picked Up.";
                }


            }
            return stage;
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
