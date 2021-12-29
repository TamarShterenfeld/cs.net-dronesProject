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
            DroneWeightsList = typeof(BO.WeightCategories).GetEnumValues();
            IsAdd = false;
            IsEdit = true;
            //StationsId = new List<string>();
            //foreach (var item in bl.GetBOBaseStationsList())
            //{
            //    StationsId.Add(item.Id.ToString());
            //}
        }
        public Drone Drone { get; set; }
        public Array DroneStatusesList { get; set; }
        public Array DroneWeightsList { get; set; }

        public List<string> StationsId { get; set; }
        public ICommand MyProperty { get; set; }
        public bool IsAdd { get; set; }
        public bool IsEdit { get; set; }
    }
}
