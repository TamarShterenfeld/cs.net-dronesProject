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
        Latitude , Longitude
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
    }
}



