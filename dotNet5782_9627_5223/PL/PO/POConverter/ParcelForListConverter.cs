using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL.PO
{
    public static partial class POConverter
    {
        //-----------------------ParcelForList Converting----------------------

        /// <summary>
        /// convert BO parclForList to PO
        /// </summary>
        /// <param name="parcel">BO parclForList</param>
        /// <returns>PO parclForList</returns>
        public static PO.ParcelForList ParcelForListBOToPO(BO.ParcelForList parcel)
        {
            return new(parcel);
        }

        /// <summary>
        /// convert list of BO parclForList to PO
        /// </summary>
        /// <param name="parcels">BO parclForList IEnumerable</param>
        /// <returns>PO parclForList IEnumerable</returns>
        public static IEnumerable<PO.ParcelForList> ListOfParcelForListBOToPO(IEnumerable<BO.ParcelForList> parcels)
        {
            return parcels.Select(item=> ParcelForListBOToPO(item));
        }

    }
}
