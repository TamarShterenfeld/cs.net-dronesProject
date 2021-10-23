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
                string senderId;
                string targetId;
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
                public string SenderId
                {
                    get
                    {
                        return senderId;
                    }
                    set
                    {
                        if (value.Length != 9)
                        {
                            throw new FormatException("Sender ID must include exactly 9 digits");
                        }
                        foreach (char letter in value)
                        {
                            if (!Char.IsDigit(letter))
                            {
                                throw new FormatException("Sender ID must include only digits");
                            }
                        }
                       senderId = value;
                    }
                }
                public string TargetId
                {
                    get
                    {
                        return targetId;
                    }
                    set
                    {
                        if (value.Length != 9)
                        {
                            throw new FormatException("Target Id must include exactly 9 digits");
                        }
                        foreach (char letter in value)
                        {
                            if (!Char.IsDigit(letter))
                            {
                                throw new FormatException("Target Id must include only digits");

                            }
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
                        //-1 - is a sign for a not initalized droneId
                        if(value < -1)
                        {
                            throw new FormatException("Id must hold a positive value");
                        }
                        droneId = value;
                    }
                   
                }

                public DateTime Production { get; set; }
                public DateTime Association{ get; set; }
                public DateTime PickingUp { get; set; }
                public DateTime Arrival { get; set; }

                public Parcel (int id, string senderId, string targetId, WeightCategories weight, Priorities priority, int droneId = -1)
                {
                    this.id = id; this.senderId = senderId; this.targetId = targetId; this.droneId = droneId; Weight = weight; Priority = priority;
                    Production = Association = PickingUp = Arrival = new DateTime(01/01/0001);
                }
                

                public override string ToString()
                {
                    return "This is a parcel object, its id: "+ Id+", sender id: "+ SenderId+", target id: "+ TargetId;
                }
            }
        }

    }
}
