using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalApi.DO
{
    public static class ExtensionString
    {
        public static Coordinate Parse(this string stringCoor,Locations location)
        {
            double degrees, minutes, seconds;
            Directions direction;
            string temp1;         
            temp1 = stringCoor.Split('º').FirstOrDefault();
            degrees = double.Parse(temp1);
            stringCoor = stringCoor.Substring(stringCoor[temp1.Length]);
            temp1 = stringCoor.Split("' ").FirstOrDefault();
            minutes = double.Parse(temp1);
            stringCoor = stringCoor.Substring(stringCoor[temp1.Length]);
            temp1 = stringCoor.Split("'' ").FirstOrDefault();
            seconds = double.Parse(temp1);
            stringCoor = stringCoor.Substring(stringCoor[temp1.Length]);
            direction =  (Directions)Enum.Parse(typeof(Directions), stringCoor);
            Coordinate coordinate = new Coordinate{ Degrees = degrees, Minutes= minutes, Seconds = seconds, Direction = direction, MyLocation= location, };
            return coordinate;
        }
    }
}
