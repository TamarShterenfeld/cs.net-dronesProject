using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL.PO
{
    public class Location:INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        Coordinate coorLongitude;
        public Coordinate CoorLongitude
        {
            set
            {
                coorLongitude = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CoorLongitude)));
            }
            get => coorLongitude;
        }
        Coordinate coorLatitude;
        public Coordinate CoorLatitude
        {
            set
            {
                coorLatitude = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CoorLatitude)));
            }
            get => coorLatitude;
        }

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="longitude">location's longitude</param>
        /// <param name="latitude">location's latitude</param>
        public Location(PO.Coordinate longitude, PO.Coordinate latitude)
        {
            CoorLongitude = longitude;
            CoorLatitude = latitude;
        }

        // default constructor
        public Location()
        { }

        
    }
}
