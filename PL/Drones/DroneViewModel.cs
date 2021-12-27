using PL.PO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PL
{
   public class DroneViewModel
    {
        public DroneViewModel(BO.DroneForList drone)
        {
            Drone = new Drone(drone);
            DroneStatusesList = typeof(BO.DroneStatuses).GetEnumValues();
        }
        public Drone Drone { get; set; }
        public Array DroneStatusesList { get; set; }
        public ICommand MyProperty { get; set; }
    }
}
