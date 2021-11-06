﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL
{
    namespace BO
    {
        public partial class DalObject : IDAL.IDal
        {
            public static double[] Electricity()
            {
                double[] elec = new double[] { ElectricityConsumingOfAvailable,
                                               ElectricityConsumingOfLightWeight,
                                               ElectricityConsumingOfAverageWeight,
                                               ElectricityConsumingOfHeavyWeight,
                                               //ChargingRate ;
                };
                return elec;
            }

        }
    } }
