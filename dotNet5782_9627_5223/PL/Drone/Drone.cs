﻿using BO;
using static PL.PO.POConverter;
using System.ComponentModel;

namespace PL
{
    namespace PO
    {
        public class Drone : INotifyPropertyChanged
        {
            private int id;
            private string model;
            private Location location;
            private WeightCategories weight;
            private DroneStatuses status;

            public Drone(BO.DroneForList drone, BLApi.IBL bl)
            {
                Id = drone.Id;
                Model = drone.Model;
                Status = drone.Status;
                Weight = drone.MaxWeight;
                Battery = drone.Battery;
                ParcelId = drone.ParcelId;
                BO.Drone drone1 = bl.GetBLDrone(Id);
                Location =(PO.Location)POConverter.LocationBOTOPO(drone1.Location);
            }
            
            public double Battery { get; set; }
            public int ParcelId { get; set; }
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
