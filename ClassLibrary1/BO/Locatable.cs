﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL.BO
{
    
    internal static class Locatable
    {
        public static double Distance(this ILocatable from, ILocatable to)
        {
            int R = 6371 * 1000; // metres
            double phi1 = from.Location.CoorLongitude.InputCoorValue * Math.PI / 180; // φ, λ in radians
            double phi2 = to.Location.CoorLatitude.InputCoorValue * Math.PI / 180;
            double deltaPhi = (to.Location.CoorLatitude.InputCoorValue - from.Location.CoorLatitude.InputCoorValue) * Math.PI / 180;
            double deltaLambda = (to.Location.CoorLongitude.InputCoorValue - from.Location.CoorLongitude.InputCoorValue) * Math.PI / 180;

            double a = Math.Sin(deltaPhi / 2) * Math.Sin(deltaPhi / 2) +
                        Math.Cos(phi1) * Math.Cos(phi2) *
                        Math.Sin(deltaLambda / 2) * Math.Sin(deltaLambda / 2);
            double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            double d = R * c / 1000; // in kilometres
            return d;
        }
    }
    
}
