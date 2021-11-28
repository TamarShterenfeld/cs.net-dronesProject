using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL
{

    namespace BO
    {

        public class ParcelForList
        {

            int parcelId;
            string senderId;
            string targetId;
            int droneId;
            public int DroneId
            {
                get { return droneId; }
                set
                {
                    if (value < 0)
                    {
                        throw new DateTimeException("Id must contain a positive number");
                    }
                    droneId = value;
                }
            }

            public int ParcelId
            {
                get { return parcelId; }
                set
                {
                    if (value < 0)
                    {
                        throw new DateTimeException("Id must contain a positive number");
                    }
                    parcelId = value;
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
                        throw new DateTimeException("Sender ID must include exactly 9 digits");
                    }
                    foreach (char letter in value)
                    {
                        if (!Char.IsDigit(letter))
                        {
                            throw new DateTimeException("Sender ID must include only digits");
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
                        throw new DateTimeException("Target Id must include exactly 9 digits");
                    }
                    foreach (char letter in value)
                    {
                        if (!Char.IsDigit(letter))
                        {
                            throw new DateTimeException("Target Id must include only digits");

                        }
                    }
                    targetId = value;
                }
            }

            public WeightCategories Weight { get; set; }
            public Priorities Priority { set; get; }
            public ParcelStatuses Status { set; get;}

            public ParcelForList() { }

            public ParcelForList(int droneId, int parcelId, string senderId, string targetId, WeightCategories weight, Priorities priority, ParcelStatuses status)
            {
                DroneId = droneId; ParcelId = parcelId; SenderId = senderId; TargetId = targetId; Weight = weight; Priority = priority; Status = status;
            }

            /// <summary>
            /// override ToString function.
            /// </summary>
            /// <returns>description of the ParcelForList object</returns>
            public override string ToString()
            {
                return $"parcelId: {parcelId} \n" +
                       $"senderId: {SenderId} \n" +
                       $"targetId:  {TargetId}\n" +
                       $"droneId: {DroneId} \n" +
                       $"weight: {Weight} \n" +
                       $"priority: {Priority} \n" +
                       $"status: {Status} \n";

            }
        }
    }


}
