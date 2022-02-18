using System;
using System.Collections.Generic;
using System.ComponentModel;
using static PL.PO.POConverter;
using System.Text;
using System.Threading.Tasks;

namespace PL.PO
{
    /// <summary>
    /// the class ParcelForList contains all the ParcelForList's needed details.
    /// </summary>
    public class ParcelForList : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        int parcelId;
        string senderId;
        string targetId;
        int droneId;
        public int DroneId
        {
            get => droneId;
            set
            {
                droneId = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(DroneId)));
            }
        }

        public int ParcelId
        {
            get => parcelId;
            set
            {
                parcelId = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ParcelId)));
            }
        }
        public string SenderId
        {
            get => senderId;
            set
            {
                senderId = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SenderId)));
            }
        }
        public string TargetId
        {
            get => targetId;
            set
            {
                targetId = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(TargetId)));
            }
        }

        WeightCategories weight;
        Priorities priority;
        ParcelStatuses status;

        public WeightCategories Weight { get => weight; set { weight = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Weight))); } }
        public Priorities Priority { get => priority; set { priority = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Priority))); } }
        public ParcelStatuses Status { get => status; set { status = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Status))); } }

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
        public ParcelForList(BO.ParcelForList parcel)
        {
            DroneId = parcel.DroneId; ParcelId = parcel.ParcelId; SenderId = parcel.SenderId; TargetId = parcel.TargetId; Weight = (WeightCategories)parcel.Weight; Priority = (Priorities)parcel.Priority; Status = (PO.POConverter.ParcelStatuses)parcel.Status;
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