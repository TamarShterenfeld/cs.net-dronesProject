using System;
using System.Collections.Generic;
using System.Text;

namespace DO
{
    /// <summary>
    /// an enum which contains all the different directions.
    /// </summary>
    public enum Directions
    {
        NORTH, EAST, WEST, SOUTH
    }

    /// <summary>
    /// 
    /// an enum which contains the two oprtions for a location - longitude / latitude.
    /// </summary>
    public enum Locations
    {
        Latitude, Longitude
    }

    /// <summary>
    /// the class coordinate contains: degrees, minutes, seconds
    /// and methods which describe the base 60 - sexagesimal.
    /// this class creats objects with location in base 60.
    /// </summary>
    public struct Coordinate
    {
        double inputCoorValue;
        public double Degrees { get; set; }
        public double Minutes { get; set; }
        public double Seconds { get; set; }
        public double InputCoorValue
        {
            set
            {
                if (value < -180 || value > 180)
                    throw new LocationException(value);
                inputCoorValue = value;
            }
            get
            {
                return inputCoorValue;
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
                throw new LocationException(InputCoorValue);
            if (InputCoorValue < 0 && MyLocation == Locations.Longitude)
                Direction = Directions.SOUTH;

            if (InputCoorValue > 0 && MyLocation == Locations.Longitude)
            {
                Direction = Directions.NORTH;
            }
            if (InputCoorValue < 0 && MyLocation == Locations.Latitude)
            {
                Direction = Directions.WEST;
            }
            if (InputCoorValue > 0 && MyLocation == Locations.Latitude)
            {
                Direction = Directions.EAST;
            }

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
        /// override ToString function.
        /// </summary>
        /// <returns>description of the Coordinate object</returns>
        public override string ToString()
        {
            return Degrees + "º " + Minutes + "' " + Seconds + "'' " + Direction.ToString()[0];
        }
    }
}



