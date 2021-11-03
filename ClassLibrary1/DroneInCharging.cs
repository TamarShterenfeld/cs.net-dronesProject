﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDAL.DO;
using static IDAL.DO.OverloadException;
using static IBL.BO.DalObject;

namespace IBL
{
    namespace BO
    {
        public class DroneInCharging
        {
            int id;
            public int Id
            {
                set
                {
                    if (value < 0)
                    {
                        throw new OverloadException("Id must contain a positive number");
                    }
                    id = value;
                }
                get { return id; }
            }
            public double Battery { get; set; }

            /// <summary>
            /// constructor
            /// </summary>
            /// <param name="id"> DroneInPaecel's id </param>
            /// <param name="battery"> DroneInPaecel's battery </param>
            public DroneInCharging(int id, double battery)
            {
                this.id = id;
                Id = id; Battery = battery;
            }

            /// <summary>
            /// override ToString function.
            /// </summary>
            /// <returns>description of DroneInCharging objectreturns>
            public override string ToString()
            {
                return $"id: { Id } \n" +
                       $"name: { Battery } \n";
            }

        }

    }
}
