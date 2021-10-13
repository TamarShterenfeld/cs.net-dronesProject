using IDAL.DO;
using System;
using System.Collections.Generic;
using System.Text;

namespace IDAL
{
    namespace DO
    {
        public partial class IDAL
        {
            /// <summary>
            /// the struct Parcel contains all the needed details for a parcel.
            /// </summary>
            public struct Parcel
            {
                int id;
                int senderId;
                int targetId;
                int droneId;

                public int Id
                {
                    get { return id; }
                    set
                    {
                        if (value < 0)
                        {
                            throw new FormatException("Id must contain a positive number");
                        }
                        id = value;
                    }
                }

                public int SenderId
                {
                    get { return id; }
                    set
                    {
                        if (value < 0)
                        {
                            throw new FormatException("Id must contain a positive number");
                        }
                        senderId = value;
                    }
                }

                public int TargetId
                {
                    get { return id; }
                    set
                    {
                        if (value < 0)
                        {
                            throw new FormatException("Id must contain a positive number");
                        }
                        targetId = value;
                    }
                }

                public WeightCategories Weight { get; set; }
                public Priorities Priority;
                public int DroneId { 
                    get { return droneId; } 
                    set
                    {
                        if(value < 0)
                        {
                            throw new FormatException("Id must hold a positive value");
                        }
                    }
                }

                public DateTime Production { get; set; }
                public DateTime Association{ get; set; }
                public DateTime PickingUp { get; set; }
                public DateTime Arrival { get; set; }
            }
        }

    }
}
