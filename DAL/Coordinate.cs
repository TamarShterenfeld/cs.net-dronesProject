using IDAL.DO;
using System;
using System.Collections.Generic;
using System.Text;
using static IDAL.DO.IDAL;

namespace DAL
{
    public enum PositionOfCoordinates
    {
        NORTH, EAST, SOUTH, WEST
    }
    /// <summary>
    /// the class coordinate contains: degrees, minutes, seconds
    /// and methods which describe th base 60 - sexagesimal.
    /// </summary>
    public class Coordinate
    {
        public double Degrees { get; set; }
        public double Minutes { get; set; }
        public double Seconds { get; set; }
        

        public PositionOfCoordinates Position { get; set; }

        public Coordinate() { }
        public Coordinate(double value, PositionOfCoordinates position = default)
        {
            CastDoubleToCoordinante(value, position);
        }

        //a constructor with parameters.
        public Coordinate(double degrees, double minutes, double seconds, PositionOfCoordinates position)
        {
            Degrees = degrees;
            Minutes = minutes;
            Seconds = seconds;
            Position = position;
        }

        /// <summary>
        /// convert double value of position to a longitude or latitude format
        /// </summary>
        /// <param name="value"></param>
        /// <param name="position"></param>
        /// <returns>this</returns>
        public Coordinate CastDoubleToCoordinante(double value, PositionOfCoordinates position = default)
        {

            //sanity
            if (position == PositionOfCoordinates.NORTH && value < 0 )
                position = PositionOfCoordinates.SOUTH;
            //sanity
            if ( position == PositionOfCoordinates.EAST && value < 0)
                position = PositionOfCoordinates.WEST;
            //sanity
            if (position == PositionOfCoordinates.SOUTH && value > 0)
                position = PositionOfCoordinates.NORTH;
            //sanity
            if (position == PositionOfCoordinates.WEST && value > 0 )
                position = PositionOfCoordinates.EAST;
            //the absolute value of the decimal converted num.
            var decimalNum = Math.Abs(Convert.ToDecimal(value));
          
            var degrees = Decimal.Truncate(decimalNum);
            decimalNum = (decimalNum - degrees) * 60;

            var minutes = Decimal.Truncate(decimalNum);
            var seconds = (decimalNum - minutes) * 60;
            Position = position;
            Degrees = Convert.ToDouble(degrees);
            Minutes = Convert.ToDouble(minutes);
            Seconds = Convert.ToDouble(seconds);
            return this;
        }

        /// <summary>
        /// convert longitude / latitude from coordinante to a double number
        /// </summary>
        /// <returns></returns>
        public double ToDouble()
        {
            var result = (Degrees) + (Minutes) / 60 + (Seconds) / 3600;
            return Position == PositionOfCoordinates.WEST || Position == PositionOfCoordinates.SOUTH ? -result : result;
        }

        /// <summary>
        /// override ToString function.
        /// </summary>
        /// <returns>the string</returns>
        public override string ToString()
        {
            return Degrees + "º " + Minutes + "' " + Seconds + "'' " + Position.ToString()[0];
        }
    }
}
