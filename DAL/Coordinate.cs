using IDAL.DO;
using System;
using System.Collections.Generic;
using System.Text;
using static IDAL.DO.IDAL;

namespace DAL
{
    public enum Directions
    {
        NORTH, EAST, WEST, SOUTH
    }
    public enum Locations
    {
        LATITUDE, LONGITUDE
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
        

        public Directions Direction { get; set; }
        public Locations Location { set; get; }

        public Coordinate() { }
        public Coordinate(double value, Locations location = default)
        {
            CastDoubleToCoordinante(value, location);
        }

        //a constructor with parameters.
        public Coordinate(double degrees, double minutes, double seconds, Directions direction, Locations location)
        {
            Degrees = degrees;
            Minutes = minutes;
            Seconds = seconds;
            Direction = direction;
            Location = location;
        }

        /// <summary>
        /// convert double num of position to a longitude or latitude format
        /// </summary>
        /// <param name="value"></param>
        /// <param name="position"></param>
        /// <returns>this</returns>
        public Coordinate CastDoubleToCoordinante(double value,Locations location)
        {

            if (value < 0 && location == Locations.LONGITUDE)
                Direction = Directions.SOUTH;

            if(value>0 && location == Locations.LONGITUDE)
            {
                Direction = Directions.NORTH;
            }
            if (value < 0 && location == Locations.LATITUDE)
            {
                Direction = Directions.WEST;
            }
            if(value > 0 && location == Locations.LATITUDE)
            {
                Direction = Directions.EAST;
            }
                    
            
            
            
            //the absolute num of the decimal converted num.
            var decimalNum = Math.Abs(Convert.ToDecimal(value));
          
            var degrees = Decimal.Truncate(decimalNum);
            decimalNum = (decimalNum - degrees) * 60;

            var minutes = Decimal.Truncate(decimalNum);
            var seconds = (decimalNum - minutes) * 60;
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
            return Direction == Directions.WEST || Direction == Directions.SOUTH ? -result : result;
        }

        /// <summary>
        /// override ToString function.
        /// </summary>
        /// <returns>the string</returns>
        public override string ToString()
        {
            return Degrees + "º " + Minutes + "' " + Seconds + "'' " + Direction.ToString()[0];
        }
    }
}
