using System;
using System.Collections.Generic;
using System.Text;
using static IDAL.DO.IDAL;

namespace IDAL
{
    public interface IDAL
    {
        static int IncreaseParcelIndex();
       static  IEnumerable<Parcel> GettingNotAssociatedParcels(); 


    }
}
