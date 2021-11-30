using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IBL.BO;

namespace IBL.BO
{
 

    /// <summary>
    /// the class coordinate contains: degrees, minutes, seconds
    /// and methods which describe the base 60 - sexagesimal.
    /// this class creats objects with location in base 60.
    /// </summary>
    public class Coordinate
    {
        public double Degrees { get; set; }
        public double Minutes { get; set; }
        public double Seconds { get; set; }
        private double  inputCoorValue;
        public double InputCoorValue
        {
            get
            {
                return inputCoorValue;
            }
            set
            {
                if (value < -180 || value > 180)
                    throw new BLLocationException(value);
                inputCoorValue = value;
            }
        }


        public Directions Direction { get; set; }
        public Locations MyLocation { set; get; }




        /// <summary>
        /// converts a double value of position to a Coordinate object.
        /// it contains a progress of calaulatios based on the location parameter value.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="position"></param>
        /// <returns>a coordinate object which calculated based on the double value parameter.</returns>
        public void CastDoubleToCoordinante()
        {
            if (InputCoorValue < -180 || InputCoorValue > 180)
                throw new BLLocationException(InputCoorValue);
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
            inputCoorValue = degree;
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

