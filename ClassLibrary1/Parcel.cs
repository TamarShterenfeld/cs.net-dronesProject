using IDAL.DO;
using System;
using System.Collections.Generic;
using System.Text;
using static IDAL.DO.OverloadException;

namespace IBL
{
    namespace BO
    {
        /// <summary>
        /// the struct Parcel contains the following details: id, senderId, targetId, droneId, weight,
        /// priority, producton, association, picing up, arrival.
        /// </summary>
        public class Parcel
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
                        throw new OverloadException("Id must contain a positive number");
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
                        throw new OverloadException("Sender ID must include exactly 9 digits");
                    }
                    foreach (char letter in value)
                    {
                        if (!Char.IsDigit(letter))
                        {
                            throw new OverloadException("Sender ID must include only digits");
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
                        throw new OverloadException("Target Id must include exactly 9 digits");
                    }
                    foreach (char letter in value)
                    {
                        if (!Char.IsDigit(letter))
                        {
                            throw new OverloadException("Target Id must include only digits");

                        }
                    }
                    targetId = value;
                }
            }

            public WeightCategories Weight { get; set; }
            public Priorities Priority;
            public int DroneId
            {
                get { return droneId; }
                set
                {
                    //-1 - is a sign for a not initalized droneId
                    if (value < -1)
                    {
                        throw new OverloadException("Id must hold a positive value");
                    }
                    droneId = value;
                }

            }

            public DateTime Production { get; set; }
            public DateTime Association { get; set; }
            public DateTime PickingUp { get; set; }
            public DateTime Supplied { get; set; }

            /// <summary>
            /// a constructor with parameters.
            /// </summary>
            /// <param name="id">modify id</param>
            /// <param name="senderId">modify senderId</param>
            /// <param name="targetId">modify targetId</param>
            /// <param name="weight">modify weight</param>
            /// <param name="priority">modify priority</param>
            /// <param name="droneId">modify droneId</param>
            public Parcel(int id, string senderId, string targetId, WeightCategories weight, Priorities priority, int droneId = -1)
            {
                this.id = id; this.senderId = senderId; this.targetId = targetId; this.droneId = droneId; Weight = weight; Priority = priority;
                //a default value in the creation of the object.
                Production = Association = PickingUp = Supplied = new DateTime(01 / 01 / 0001);
            }

            public Parcel() { }

            /// <summary>
            /// override ToString function.
            /// </summary>
            /// <returns></returns>
            public override string ToString()
            {
                return $"id: {Id} \n" +
                             $"senderId: {SenderId} \n" +
                             $"targetId: {TargetId}\n" +
                             $"droneId:  {DroneId}\n" +
                             $"weight:  {Weight}\n" +
                             $"priority:  {Priority}\n" +
                             $"production:  {Production}\n" +
                             $"association:  {Association}\n" +
                             $"pickingUp:  {PickingUp}\n" +
                             $"arrival: {Supplied}\n";
            }
        }
    }
}
