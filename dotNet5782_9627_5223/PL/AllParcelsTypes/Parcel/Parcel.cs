using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        public class Parcel : INotifyPropertyChanged
        {
            #region PrivateFields
            int parcelId;
            string senderId;
            string targetId;
            int droneId;
            POConverter.WeightCategories weight;
            POConverter.Priorities priorities;
            POConverter.ParcelStatuses statuses;

            public event PropertyChangedEventHandler PropertyChanged;
            #endregion

            #region Properties
            public int DroneId
            {
                get { return droneId; }
                set
                {
                    droneId = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(DroneId)));
                }
            }

            public int ParcelId
            {
                get { return parcelId; }
                set
                {
                    parcelId = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ParcelId)));
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
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SenderId)));
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
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(TargetId)));
                }
            }
            public POConverter.WeightCategories Weight
            {
                get=> weight;
                set
                {
                    weight = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Weight)));
                }
            }

            public POConverter.Priorities Priority 
            {
                set
                {
                    priorities = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Priority)));
                }
                get => priorities;
            }

            public POConverter.ParcelStatuses Status 
            {
                set
                {
                    statuses = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Status)));
                }
                get => statuses;
            }

            #endregion

            #region Constructors
            public Parcel(BLApi.IBL bl, ParcelForList parcelForList)
            {
                ParcelId = parcelForList.ParcelId;
                SenderId = parcelForList.SenderId;
                TargetId = parcelForList.TargetId;
                DroneId = parcelForList.DroneId;
                Weight = (POConverter.WeightCategories)Enum.Parse(typeof(POConverter.WeightCategories), parcelForList.Weight.ToString());
                Priority = (POConverter.Priorities)Enum.Parse(typeof(POConverter.Priorities), parcelForList.Priority.ToString());
                Status = (POConverter.ParcelStatuses)Enum.Parse(typeof(POConverter.ParcelStatuses), parcelForList.Status.ToString());
            }
              /// <summary>
            /// default constructor
            /// </summary>
            public Parcel() { }

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
            public Parcel(int droneId, int parcelId, string senderId, string targetId, BO.WeightCategories weight, BO.Priorities priority, BO.ParcelStatuses status)
            {
                DroneId = droneId; ParcelId = parcelId; SenderId = senderId; TargetId = targetId; Weight = (POConverter.WeightCategories)(int)weight; Priority = (POConverter.Priorities)(int)priority; Status = (POConverter.ParcelStatuses)(int)status;
            }


            #endregion
   
            #region ToString
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
            #endregion
        }
    }

}