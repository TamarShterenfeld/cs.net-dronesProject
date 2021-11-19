using System;

namespace IBL
{
    namespace BO
    {
        /// <summary>
        /// all the different enums that were used in the project
        /// are collected here - just because they may belong to more than one structure.
        /// </summary>
        public enum WeightCategories
        {
            Light = 1, Average, Heavy
        }

        public enum DroneStatuses
        {
            Available = 1, Maintenance, Shipment
        }
        public enum Priorities
        {
            Standard = 1, Fast, Emergency
        }

        public enum ParcelStatuses
        {
            Production, Associated, PickedUp, Supplied
        }

        public enum ParcelConditions
        {
            Priority, WeightCategory, Location
        }
    }

}
