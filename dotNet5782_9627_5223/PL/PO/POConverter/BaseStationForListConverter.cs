using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL.PO
{
    public static partial class POConverter
    {
        //-----------------------StationForList Converting----------------------
        public static PO.BaseStationForList StationForListBOToPO(BO.BaseStationForList station)
        {
            return new(station);
        }
        public static IEnumerable<PO.BaseStationForList> ListOfStationForListBOToPO(IEnumerable<BO.BaseStationForList> stations)
        {
            return stations.Select(item=> StationForListBOToPO(item));
        }

    }
}
