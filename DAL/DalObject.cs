using System;
using System.Collections.Generic;
using System.Text;
using static IDAL.DO.IDAL;

namespace DalObject
{
   
    public class DataSource
    {
        //const literals.
        const int DRONESAMOUNT = 10;
        const int BASESTATIONSAMOUNT = 5;
        const int CUSTOMERSAMOUNT = 100;
        const int PARCELAMOUNT = 1000;

        //internal arrs of different entities.
        internal static Drone[] DronesArr = new Drone[DRONESAMOUNT];
        internal static BaseStation[] BaseStationsArr = new BaseStation[BASESTATIONSAMOUNT];
        internal static Customer[] CustomersArr = new Customer[CUSTOMERSAMOUNT];
        internal static Parcel[] ParcelsArr = new Parcel[PARCELAMOUNT];

    }

    public class DalObject
    {

    }
}
