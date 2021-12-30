using BO;
using System.ComponentModel;

namespace PL
{
    namespace PO
    {
        public class Drone : INotifyPropertyChanged
        {
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
                Location = drone.Location;
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
            public int Id { get; set; }
            public double Battery { get; set; }
            public int ParcelId { get; set; }
            public Location Location { get; set; }
            public WeightCategories Weight { set; get; }
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
