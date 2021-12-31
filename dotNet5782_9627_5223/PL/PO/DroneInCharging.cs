using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL.PO
{
    public class DroneInCharging: INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        int id;
        public int Id
        {
            set
            {
                id = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Id)));
            }
            get =>id; 
        }

        double battery;
        public double Battery
        {
            set
            {
                battery = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Battery)));
            }
            get => battery;
        }

        /// <summary>
        /// a constructor with parameters
        /// </summary>
        /// <param name="id"> DroneInPaecel's id </param>
        /// <param name="battery"> DroneInPaecel's battery </param>
        public DroneInCharging(int id, double battery)
        {
            this.id = id;
            Id = id; Battery = battery;
        }

        // default constructor
        public DroneInCharging() { }
    }

}
