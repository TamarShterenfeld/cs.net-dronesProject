using BO;
using System.ComponentModel;

namespace PL
{
    namespace PO
    {
        public class Drone : INotifyPropertyChanged
        {
            PO.Coordinate CoordinateDoToBo(BO.Coordinate coor)
            {
                return new PO.Coordinate() { InputCoorValue = coor.InputCoorValue, Degrees = coor.Degrees, Direction = (PO.Directions)coor.Direction, MyLocation = (PO.Locations)coor.MyLocation, Minutes = coor.Minutes, Seconds = coor.Seconds };
            }

            private int id;
            private string model;
            private WeightCategories weight;
            private DroneStatuses status;

            public Drone(BO.DroneForList drone)
            {
                Id = drone.Id;
                Model = drone.Model;
                Status = drone.Status;
                Weight = drone.MaxWeight;
                Battery = drone.Battery;
                ParcelId = drone.ParcelId;
                Location = new(CoordinateDoToBo(drone.Location.CoorLongitude), CoordinateDoToBo(drone.Location.CoorLatitude));
            }
            
            public double Battery { get; set; }
            public int ParcelId { get; set; }
            public Location Location { get; set; }
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
