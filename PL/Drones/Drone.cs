using BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL
{
    namespace PO
    {
        public class Drone
        {
            public Drone(BO.DroneForList drone)
            {
                Model = drone.Model;
                Status = drone.Status;
            }
            public string Model { get; set; }
            public DroneStatuses Status { get; set; }
        }
    }
}
