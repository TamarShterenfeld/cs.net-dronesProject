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
        public DroneStatuses Status { get => status; set => status = value; }
        public UserStage(BO.UserStage stage)
        {
            Distance = stage.Distance;
            PickedUp = stage.PickedUp;
            Status = (DroneStatuses)stage.Status;
        }
        public UserStage() { }
        public override string ToString()
        {
            string stage = "";
            if(Status == DroneStatuses.Shipment)
            {
                if(PickedUp)
                {
                    stage = $"Distance frome sender to target: {Distance}.\n Parcel has already been picked up.";
                    if (Distance < 100)
                    {
                        stage += "\n Supplying parcel.";
                }
                }
                else
                {
                    stage = $"Distance till sender:{Distance}.\n Not Yet Picked Up.";
                }

                
            }
            return stage;
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
