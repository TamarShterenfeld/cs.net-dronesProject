using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace DalXml
{
    sealed partial class DalXml
    {


        /// <summary>
        ///a static method which increases the static field - 'ParcelId' in each time it is called. 
        /// </summary>
        /// <returns></returns>
        public static int IncreaseParcelIndex()
        {
            return 0;
        }

        public int CaughtChargeSlots(int baseStationId)
        {
            return 0;
        }

        public double[] ElectricityConsuming()
        {
            return new double[1];
        }

        public int GetLastParcelId()
        {
            return 0;
        }
    }
}
