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
        #region PrivateFields

        Coordinate coorLongitude;
        Coordinate coorLatitude;

        #endregion

        #region Properties
        public Coordinate CoorLongitude
        {
            set
            {
                coorLongitude = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CoorLongitude)));
            }
            get => coorLongitude;
        }

        public Coordinate CoorLatitude
        {
            set
            {
                coorLatitude = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CoorLatitude)));
            }
            get => coorLatitude;
        }

        public event PropertyChangedEventHandler PropertyChanged;


        #endregion

        #region Constructors

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

        #endregion

        #region ToString

        /// <summary>
        /// ovveride ToString function.
        /// </summary>
        /// <returns>description of the Location object</returns>
        public override string ToString()
        {
            return $"longitude: {CoorLongitude} , latitude: {CoorLatitude}";
        }

        #endregion
    }
}
