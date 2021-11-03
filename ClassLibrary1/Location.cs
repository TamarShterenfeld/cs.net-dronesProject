using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL
{
    namespace BO
    {
        public class Location
        {
            private Coordinate coorLong;
            private Coordinate coorLat;
            public Coordinate Long
            {
                get
                {
                    return coorLong;
                }
                set
                {
                    coorLong = value;
                }
            }

            public Coordinate Lat
            {
                get
                {
                    return coorLat;
                }
                set
                {
                    coorLat = value;
                }
            }

            public override string ToString()
            {
                return $"longitude: {coorLong}\n" +
                       $"latitude:  {coorLat}\n";
            }
        }
    }
}
