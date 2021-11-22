using IDal.DO;
using System;
using System.Collections.Generic;
using System.Text;
using static IDal.DO.OverloadException;

namespace IDal
{
    namespace DO
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
            public Priorities Priority { get; set; }

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

            public DateTime ProductionDate { get; set; }
            public DateTime AssociationDate { get; set; }
            public DateTime PickUpDate { get; set; }
            public DateTime SupplyDate { get; set; }

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
                             $"production:  {ProductionDate}\n" +
                             $"association:  {AssociationDate}\n" +
                             $"pickingUp:  {PickUpDate}\n" +
                             $"arrival: {SupplyDate}\n";
            }
        }
    }
}
