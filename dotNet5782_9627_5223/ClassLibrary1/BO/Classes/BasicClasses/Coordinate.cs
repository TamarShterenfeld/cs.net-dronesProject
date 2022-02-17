using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO;

namespace BO
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
        double inputCoorValue;
        public double InputCoorValue
        {
            get
            {
                return inputCoorValue;
            }
            set
            {
                if (value < -90 || value > 90)
                    throw new BLLocationException(value);
                inputCoorValue = value;
            }
        }


        public Directions Direction { get; set; }
        public Locations MyLocation { set; get; }





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
        public Coordinate() {}


        /// override ToString function.
        /// </summary>
        /// <returns>description of the Coordinate object</returns>
        public override string ToString()
        {
            double tempPlace = InputCoorValue;
            if (MyLocation == Locations.Longitude)
            {
                Direction = Directions.EAST;
                if (tempPlace < 0)
                {
                    Direction = Directions.WEST;
                    tempPlace = -tempPlace;
                }
            }
            else if (MyLocation == Locations.Latitude)
            {
                Direction = Directions.NORTH;
                if (tempPlace < 0)
                {
                    Direction = Directions.SOUTH;
                    tempPlace = -tempPlace;
                }
            }

            Degrees = (int)tempPlace;
            Minutes = (int)(60 * (tempPlace - Degrees));
            Seconds = (tempPlace - Degrees) * 3600 - Minutes * 60;
            return $"{Degrees}°{Minutes}′{Seconds:0.0}″{Direction.ToString()[0]}";
        }

    }
    

}

