using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DalXml
{
    static class Convertors
    {
        static bool ConvertStringToBool(string s)
        {
            return (s == "true" ? true : false);
        }

        public static XElement CoordinateToXElement(Coordinate coor, Locations l)
        {
            XElement inputCoorValue = new XElement("inputCoorValue", coor.InputCoorValue.ToString());
            XElement degrees = new XElement("degrees", coor.Degrees.ToString());
            XElement minutes = new XElement("minutes", coor.Minutes.ToString());
            XElement seconds = new XElement("seconds", coor.Seconds.ToString());
            XElement myLocation = new XElement("location", ((object)coor.MyLocation).ToString());
            XElement direction = new XElement("direction", ((object)coor.Direction).ToString());
            return new XElement(((object)l).ToString(), inputCoorValue, degrees, minutes, seconds, myLocation, direction);
        }

        public static Coordinate ExlementToCoordinate(XElement xmlCoordinate, Locations l)
        {
            string location = xmlCoordinate.Element(((object)l).ToString()).Element("location").Value;
            string direction = xmlCoordinate.Element(((object)l).ToString()).Element("direction").Value;
            return new Coordinate
            {
                InputCoorValue = double.Parse(xmlCoordinate.Element(((object)l).ToString()).Element("inputCoorValue").Value),
                Degrees = double.Parse(xmlCoordinate.Element(((object)l).ToString()).Element("degrees").Value),
                Minutes = double.Parse(xmlCoordinate.Element(((object)l).ToString()).Element("minutes").Value),
                Seconds = double.Parse(xmlCoordinate.Element(((object)l).ToString()).Element("seconds").Value),
                MyLocation = (Locations)Enum.Parse(typeof(Locations), location),
                Direction = (Directions)Enum.Parse(typeof(Directions), direction),
            };
        }
    }
}
