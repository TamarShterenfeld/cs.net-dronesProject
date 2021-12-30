using PL.PO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace PL
{
    public class DroneViewModel
    {
        public DroneViewModel(BO.DroneForList drone, BLApi.IBL bl)
        {
            Drone = new Drone(drone);
            DroneStatusesList = typeof(BO.DroneStatuses).GetEnumValues();
            DroneWeightsList = typeof(BO.WeightCategories).GetEnumValues();
            IsAdd = false;
            IsEdit = true;
            StationsId = new List<string>();
            foreach (var item in bl.GetBOBaseStationsList())
            {
                StationsId.Add(item.Id.ToString());
            }
            Cancel = new(Button_ClickCancel, null);
        }
        public Drone Drone { get; set; }
        public Array DroneStatusesList { get; set; }
        public Array DroneWeightsList { get; set; }
        public List<string> StationsId { get; set; }
        public RelayCommand Cancel { get; set; }


        public bool IsAdd { get; set; }
        public bool IsEdit { get; set; }

        /// <summary>
        /// the function treats the event of clicking on the button 'Cancel'.
        /// </summary>
        /// <param name="sender">the invoking object</param>
        /// <param name="e">the event</param>
        private void Button_ClickCancel(object sender)
        {
            (sender as Window).Close();
        }
    }
}
