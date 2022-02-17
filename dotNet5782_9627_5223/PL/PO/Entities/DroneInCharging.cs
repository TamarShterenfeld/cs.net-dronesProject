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
        #region PrivateFields

        int id;
        double battery;

        #endregion

        #region Properties

        public event PropertyChangedEventHandler PropertyChanged;

        public int Id
        {
            get => id;
            set
            {
                id = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Id)));
            }

        }
        public double Battery
        {
            set
            {
                battery = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Battery)));
            }
            get => battery;
        }

        #endregion

        #region Constructors

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

        #endregion

        #region ToString
        public override string ToString()
        {
            String strBattery = String.Format("%.2F", Battery);
            return $"id: { Id } \n" +
                   $"battery: {Math.Round(Battery,2)}\n";
        }

        #endregion
    }

}
