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

        /// <summary>
        /// the class Config contains the indexes of the first free place in the arrays
        /// in addition, ...?
        /// </summary>
        internal class Config
        {
            internal static int indexOfDrone = 0;
            internal static int indexOfBaseStation = 0;
            internal static int indexOfCustomer = 0;
            internal static int indexOfParcel = 0;


        }

    }

    public class DalObject
    {

    }
}
