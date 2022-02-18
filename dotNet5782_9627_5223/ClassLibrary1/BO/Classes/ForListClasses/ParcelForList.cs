using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    /// <summary>
    /// the class ParcelForList contains all the ParcelForList's needed details.
    /// </summary>
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
                    throw new BLIntIdException(value);
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
                    throw new BLIntIdException(value);
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
                    throw new BLStringIdException(value);
                }
                foreach (char letter in value)
                {
                    if (!Char.IsDigit(letter))
                    {
                        throw new BLStringIdException(value);
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
                    throw new BLStringIdException(value);
                }
                foreach (char letter in value)
                {
                    if (!Char.IsDigit(letter))
                    {
                        throw new BLStringIdException(value);

                    }
                }
                targetId = value;
            }
        }

        public WeightCategories Weight { get; set; }
        public Priorities Priority { set; get; }
        public ParcelStatuses Status { set; get; }

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
                    $"status: {Status}";

        }
    }
}
