using System;
using static PL.PO.POConverter;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL.PO
{
    public class Coordinate : INotifyPropertyChanged
    {
        #region PrivateFields
        private double degrees;
        private double minutes;
        private double seconds;
        double inputCoorValue;
        Directions direction;
        Locations myLocation;
        #endregion

        #region Properties
       
        public event PropertyChangedEventHandler PropertyChanged;
       
        public double Degrees
        {
            get => degrees;
            set
            {
                degrees = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Degrees)));
            }
        }
        
        public double Minutes
        {
            get => minutes;
            set
            {
                minutes = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Minutes)));
            }
        }
       
        public double Seconds
        {
            get => seconds;
            set
            {
                seconds = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Seconds)));
            }
        }
       
        public double InputCoorValue
        {
            get => inputCoorValue;
            set
            {
                inputCoorValue = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(InputCoorValue)));
            }
        }
      
        public Directions Direction
        {
            get => direction;
            set
            {
                direction = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Direction)));
            }
        }
      
        public Locations MyLocation
        {
            get => myLocation;
            set
            {
                myLocation = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(MyLocation)));
            }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// constructor which gets degree and direction (longitude ot latitude)
        /// </summary>
        /// <param name="degree">place in degrees</param>
        /// <param name="longOrLat">longitude ot latitude</param>
        public Coordinate(double degree, Locations longOrLat)
        {
            InputCoorValue = degree;
            MyLocation = longOrLat;
        }

        /// <summary>
        /// default constructor
        /// </summary>
        public Coordinate() { }

        #endregion

        #region ToString
        /// <summary>
        /// override ToString function.
        /// </summary>
        /// <returns>description of the Coordinate object</returns>
        public override string ToString()
        {
            if (MyLocation == Locations.Longitude)
            {
                Direction = Directions.EAST;
                if (InputCoorValue < 0)
                {
                    Direction = Directions.WEST;
                    InputCoorValue = -InputCoorValue;
                }
            }
            else if (MyLocation == Locations.Latitude)
            {
                Direction = Directions.NORTH;
                if (InputCoorValue < 0)
                {
                    Direction = Directions.SOUTH;
                    InputCoorValue = -InputCoorValue;
                }
            }

            Degrees = (int)InputCoorValue;
            Minutes = (int)(60 * (InputCoorValue - Degrees));
            Seconds = (InputCoorValue - Degrees) * 3600 - Minutes * 60;
            return $"{Degrees}°{Minutes}′{Seconds:0.0}″{Direction.ToString()[0]}";
        }

        #endregion

    }
}
