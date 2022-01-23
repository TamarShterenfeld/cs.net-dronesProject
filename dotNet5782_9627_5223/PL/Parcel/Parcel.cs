using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL
{
    namespace PO
    {
        /// <summary>
        /// the class ParcelForList contains all the ParcelForList's needed details.
        /// </summary>
        public class ParcelForList
        {

            public ParcelForList(BLApi.IBL bl, ParcelForList parcelForList)
            {
                ParcelId = parcelForList.ParcelId;
                SenderId = parcelForList.SenderId;
                TargetId = parcelForList.TargetId;
                DroneId = parcelForList.DroneId;
                Weight = parcelForList.Weight;
                Priority = parcelForList.Priority;
                Status = parcelForList.Status;
            }

            int parcelId;
            string senderId;
            string targetId;
            int droneId;
            public int DroneId
            {
                get { return droneId; }
                set
                {

                    droneId = value;
                }
            }

            public int ParcelId
            {
                get { return parcelId; }
                set
                {
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
                    targetId = value;
                }
            }

            public BO.WeightCategories Weight { get; set; }
            public BO.Priorities Priority { set; get; }
            public BO.ParcelStatuses Status { set; get; }

            /// <summary>
            /// default constructor
            /// </summary>
            public ParcelForList() { }

            /// <summary>
            /// a constructor with parameters.
            /// </summary>
            /// <param name="droneId"></param>
            /// <param name="parcelId"></param>
            /// <param name="senderId"></param>
            /// <param name="targetId"></param>
            /// <param name="weight"></param>
            /// <param name="priority"></param>
            /// <param name="status"></param>
            public ParcelForList(int droneId, int parcelId, string senderId, string targetId, BO.WeightCategories weight, BO.Priorities priority, BO.ParcelStatuses status)
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
                        $"status: {Status}";

            }
        }
    }

}