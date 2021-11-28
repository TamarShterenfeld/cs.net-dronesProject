using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL.BO
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
    public class Coordinate
    {
        public double Degrees { get; set; }
        public double Minutes { get; set; }
        public double Seconds { get; set; }
        public double InputCoorValue { get; set; }

        public Directions Direction { get; set; }
        public Locations MyLocation { set; get; }


        /// <summary>
        /// converts a double value of position to a Coordinate object.
        /// it contains a progress of calaulatios based on the location parameter value.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="position"></param>
        /// <returns>a coordinate object which calculated based on the double value parameter.</returns>
        public Coordinate CastDoubleToCoordinante(double value, Locations location)
        {
            if (value < -180 || value > 180)
            {
                throw new LocationException(value);
                //---print in the catch function---Console.WriteLine("Coordinante value must be a positive number and in range of - 180º to 180º.");
            }
            if (value < 0 && location == Locations.Longitude)
                Direction = Directions.SOUTH;

            if (value > 0 && location == Locations.Longitude)
            {
                Direction = Directions.NORTH;
            }
            if (value < 0 && location == Locations.Latitude)
            {
                Direction = Directions.WEST;
            }
            if (value > 0 && location == Locations.Latitude)
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
        public Coordinate(){ }

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
        /// <returns>description of the Coordinate object</returns>
        public override string ToString()
        {
            return Degrees + "º " + Minutes + "' " + Seconds + "'' " + Direction.ToString()[0];
        }
    }
}
