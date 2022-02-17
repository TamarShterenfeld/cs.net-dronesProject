using System;
using System.Collections.Generic;
using System.Text;

namespace DO
{
    /// <summary>
    /// the struct Parcel contains the following details: id, senderId, targetId, droneId, weight,
    /// priority, producton, association, picing up, arrival.
    /// actually, these are all the basic details for creating a baseStation.
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
                    throw new IntIdException(value);
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
                    throw new StringIdException(value);
                }
                foreach (char letter in value)
                {
                    if (!Char.IsDigit(letter))
                    {
                        throw new StringIdException(value);
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
                    throw new StringIdException(value);
                }
                foreach (char letter in value)
                {
                    if (!Char.IsDigit(letter))
                    {
                        throw new StringIdException(value);
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
                if (value < 0)
                {
                    throw new IntIdException(value);
                }
                droneId = value;
            }

        }

        public DateTime? ProductionDate { get; set; }
        public DateTime? AssociationDate { get; set; }
        public DateTime? PickUpDate { get; set; }
        public DateTime? SupplyDate { get; set; }
        public bool IsDeleted { get; set; }

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

