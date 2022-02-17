using static PL.PO.POConverter;
using System.ComponentModel;
using System;

namespace PL
{
    namespace PO
    {
        public class Drone : INotifyPropertyChanged
        {
            readonly static Random rand = new();
            private int id;
            private string model;
            private Location location = new();
            private WeightCategories weight;
            private DroneStatuses status = DroneStatuses.Available;
            private double battery = rand.NextDouble() * 20 + 20;

            public Drone(PL.PO.DroneForList drone, BLApi.IBL bl)
            {
                Id = drone.Id;
                Model = drone.Model;
                Status = drone.Status;
                Weight = drone.MaxWeight;
                Battery = drone.Battery;
                parcel = drone.ParcelId == 0? null : ParcelInPassingBOTOPO(bl.GetParcelInPassing(drone.ParcelId));
                BO.Drone drone1 = bl.GetBLDrone(Id);
                Location = LocationBOTOPO(drone1.Location);
            }

            /// <summary>
            /// default constructor
            /// </summary>
            public Drone() { }

            public double Battery { get => battery; set { battery = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Battery))); } } 
            private ParcelInPassing parcel;
            public ParcelInPassing Parcel { set { parcel = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Parcel))); } get { return parcel; } }
            public Location Location
            {
                get => location;
                set
                {
                    location = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Location)));
                }
            }
            public string Model
            {
                get => model;
                set
                {
                    model = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Model)));
                }
            }
            public int Id
            {
                get => id;
                set
                {
                    id = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Id)));
                }
            }

            public WeightCategories Weight
            {
                get => weight;
                set
                {
                    weight = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Weight)));
                }
            }
            public DroneStatuses Status
            {
                get => status;
                set
                {
                    status = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Status)));
                }
            }
            public event PropertyChangedEventHandler PropertyChanged;
        }
    }
}