using IDal.DO;
using System;
using System.Collections.Generic;
using System.Text;

namespace IDal
{
    namespace DO
    {
        public enum Directions
        {
            NORTH, EAST, WEST, SOUTH
        }
        public enum Locations
        {
            Latitude, Longitude
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

            public double RegularCoor { get; set; }


            public Directions Direction { get; set; }
            public Locations Location { set; get; }

            public Coordinate() { }
            public Coordinate(double value, Locations location = default)
            {
                if (value < -180 || value > 180)
                {
                    throw new OverloadException("Coordinante value must be a positive number and in range of - 180º to 180º.");
                }

                RegularCoor = value;
                CastDoubleToCoordinante(value, location);
            }

            //a constructor with parameters.
            public Coordinate(double degrees, double minutes, double seconds, Directions direction, Locations location)
            {

                if (degrees < -180 || degrees > 180)
                {
                    throw new OverloadException("Coordinante value must be a positive number and in range of - 180º to 180º.");
                }
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
            public Coordinate CastDoubleToCoordinante(double value, Locations location)
            {
                if (value < -180 || value > 180)
                {
                    throw new OverloadException("Coordinante value must be a positive number and in range of - 180º to 180º.");
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


}
