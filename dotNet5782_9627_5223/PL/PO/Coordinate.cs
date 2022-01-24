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
        public event PropertyChangedEventHandler PropertyChanged;
        private double degrees;
        public double Degrees
        {
            get => degrees;
            set
            {
                degrees = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Degrees)));
            }
        }
        private double minutes;
        public double Minutes
        {
            get => minutes;
            set
            {
                minutes = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Minutes)));
            }
        }

        private double seconds;
        public double Seconds
        {
            get => minutes;
            set
            {
                seconds = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Seconds)));
            }
        }

        double inputCoorValue;
        public double InputCoorValue
        {
            get => inputCoorValue;
            set
            {
                inputCoorValue = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(InputCoorValue)));
            }
        }

        Directions direction;
        public Directions Direction
        {
            get => direction;
            set
            {
                direction = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Direction)));
            }
        }

        Locations myLocation;
        public Locations MyLocation
        {
            get => myLocation;
            set
            {
                myLocation = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(MyLocation)));
            }
        }




        /// <summary>
        /// converts a double value of position to a Coordinate object.
        /// it contains a progress of calaulatios based on the location parameter value.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="position"></param>
        /// <returns>a coordinate object which calculated based on the double value parameter.</returns>
        public void CastDoubleToCoordinante()
        {
            if (InputCoorValue < 0 && MyLocation == Locations.Longitude)
                Direction = Directions.SOUTH;

            if (InputCoorValue > 0 && MyLocation == Locations.Longitude)
                Direction = Directions.NORTH;
            if (InputCoorValue < 0 && MyLocation == Locations.Latitude)
                Direction = Directions.WEST;
            if (InputCoorValue > 0 && MyLocation == Locations.Latitude)
                Direction = Directions.EAST;

            //the absolute num of the decimal converted num.
            var decimalNum = Math.Abs(Convert.ToDecimal(InputCoorValue));
            var degrees = Decimal.Truncate(decimalNum);
            decimalNum = (decimalNum - degrees) * 60;
            var minutes = Decimal.Truncate(decimalNum);
            var seconds = (decimalNum - minutes) * 60;
            Degrees = Convert.ToDouble(degrees);
            Minutes = Convert.ToDouble(minutes);
            Seconds = Convert.ToDouble(seconds);
        }

        /// <summary>
        /// constructor which gets degree and direction (longitude ot latitude)
        /// </summary>
        /// <param name="degree">place in degrees</param>
        /// <param name="longOrLat">longitude ot latitude</param>
        public Coordinate(double degree, Locations longOrLat)
        {
            InputCoorValue = degree;
            MyLocation = longOrLat;
            CastDoubleToCoordinante();
        }

        /// <summary>
        /// default constructor
        /// </summary>
        public Coordinate() { }

        /// <summary>
        /// override ToString function.
        /// </summary>
        /// <returns>description of the Coordinate object</returns>
        public override string ToString()
        {
            return Degrees + "º " + Minutes + "' " + Seconds + "'' " + Direction.ToString()[0];
        }

    }
}
