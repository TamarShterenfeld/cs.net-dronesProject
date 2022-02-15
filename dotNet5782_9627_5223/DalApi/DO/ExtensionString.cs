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
            string temp1, temp2, temp3;       
            temp1 = stringCoor.Split('º').FirstOrDefault();
            degrees = double.Parse(temp1);
            temp2 = stringCoor.Split("' ").FirstOrDefault();
            minutes = double.Parse(temp2.Substring(temp1.Length + 1));
            temp3 = stringCoor.Split("'' ").FirstOrDefault();
            temp3 = temp3.Substring(temp1.Length + temp2.Length);
            seconds = double.Parse(temp3);
            direction =  (Directions)Enum.Parse(typeof(Directions), stringCoor.Substring(temp1.Length + temp2.Length + temp3.Length + 3));
            Coordinate coordinate = new Coordinate{ Degrees = degrees, Minutes= minutes, Seconds = seconds, Direction = direction, MyLocation= location, };
            return coordinate;
        }
    }
}
