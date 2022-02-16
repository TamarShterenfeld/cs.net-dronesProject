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
        public class DroneForList : INotifyPropertyChanged
        {
            #region PrivateFields
            int id;
            int parcelId;
            double battery;
            #endregion

            #region Properties
            public int Id
            {
                get => id;
                set
                {
                    id = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Id)));
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
            public string Model { get; set; }

            public PO.POConverter.WeightCategories MaxWeight { set; get; }

            public double Battery
            {
                get => battery;
                set
                {
                    battery = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Battery)));
                }

            }
            public PO.POConverter.DroneStatuses Status { set; get; }

            public PO.Location Location { get; set; }

            public event PropertyChangedEventHandler PropertyChanged;

            #endregion

            #region Constructors
            /// <summary>
            /// default constructor
            /// </summary>
            public DroneForList() { }

            /// <summary>
            /// a constructor with parameters
            /// </summary>
            /// <param name="id">DroneForList's id</param>
            /// <param name="parcelId">DroneForList's parcelId</param>
            /// <param name="model">DroneForList's model</param>
            /// <param name="weight">DroneForList's weight</param>
            /// <param name="battery">DroneForList's battery</param>
            /// <param name="status">DroneForList's status</param>
            /// <param name="location">DroneForList's location</param>
            public DroneForList(int id, int parcelId, string model, PO.POConverter.WeightCategories weight, double battery, PO.POConverter.DroneStatuses status, PO.Location location)
            {
                Id = id; ParcelId = parcelId; Model = model; MaxWeight = weight; Battery = battery; Status = status; Location = location;
            }

            #endregion

            #region ToString
            /// <summary>
            /// override ToString function.
            /// </summary>
            /// <returns>description of DroneForList object</returns>
            public override string ToString()
            {
                return $"id: {Id} \n" +
                        $"parcel id: {ParcelId} \n" +
                        $"model: {Model} \n" +
                        $"weight category: {MaxWeight} \n" +
                        $"battery: {Battery} " + "%" + "\n" +
                        $"status: {Status} \n" +
                        $"locaion: {Location}";
            }

            #endregion

          
        }
    }
}
