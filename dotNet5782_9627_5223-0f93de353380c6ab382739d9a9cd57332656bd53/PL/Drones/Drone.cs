using BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace PL
{
    namespace PO
    {
        public class Drone : INotifyPropertyChanged
        {
            public Drone(BO.DroneForList drone)
            {
                Id = drone.Id;
                Model = drone.Model;
                Status = drone.Status;
                Weight = drone.MaxWeight;

            }
            public string Model { get; set; }
            public int Id { get; set; }
            public WeightCategories Weight { set; get; }
            public DroneStatuses Status { get; set; }

            public event PropertyChangedEventHandler PropertyChanged;
        }
    }
}
