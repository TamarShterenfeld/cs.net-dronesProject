using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class UserStage
    {
        /// <summary>
        /// an entity for reports from the simulator
        /// </summary>
        public double Distance { get; set; }
        public bool PickedUp { get; set; }
        public DroneStatuses Status { get; set; }
        public bool InWayToMaintenance { get; set; }
        public UserStage(double distance, bool picked, DroneStatuses status , bool inWayToMaintenance)
        {
            Distance = distance;
            PickedUp = picked;
            Status = status;
            InWayToMaintenance = inWayToMaintenance;
        }
        public UserStage() { }
    }
}
