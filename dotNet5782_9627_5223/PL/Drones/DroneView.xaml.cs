using BO;
using IBL;
using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Text.RegularExpressions;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Globalization;
using System.ComponentModel;

namespace PL
{
    /// <summary>
    /// Interaction logic for Drone.xaml
    /// </summary>
    public partial class DroneView : Window, INotifyPropertyChanged
    {
        private BLApi.IBL bl;
        Action refreshDroneList;
        private object chargeDurationTime;

        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// a constructor that gets the object bl + the delegate action.
        /// </summary>
        /// <param name="bl">the request object to the BL logic level</param>
        /// <param name="refreshDroneList">a delegate</param>
        public DroneView(BLApi.IBL bl, Action refreshDroneList)
        {
            InitializeComponent();
            this.bl = bl;
            this.refreshDroneList = refreshDroneList;
            /*id.DataContext = model.DataContext = weight.DataContext = station.DataContext = "True";*/
            button3.DataContext = button4.DataContext = "Collapsed";
            //status.DataContext = typeof(DroneStatuses).GetEnumValues();
            weight.DataContext = typeof(BO.WeightCategories).GetEnumValues();
            List<string> stationsId = new List<string>();
            foreach (var item in bl.GetBOBaseStationsList())
            {
                stationsId.Add(item.Id.ToString());
            }
            station.DataContext = stationsId;
        }

        /// <summary>
        /// another constructor with parameters
        /// </summary>
        /// <param name="droneForList"></param>
        /// <param name="bl">the request object to the BL level</param>
        /// <param name="refreshDroneList">a delegate</param>
        public DroneView(DroneForList droneForList, BLApi.IBL bl, Action refreshDroneList)
            : this(bl, refreshDroneList)
        {

            this.DataContext = new DroneViewModel(droneForList, bl);

            station.DataContext = "False";
            if (droneForList != null)
            {
                BO.Drone drone = bl.GetBLDrone(droneForList.Id);
                //id.Text = drone.Id.ToString();
                //model.Text = drone.Model;
                weight.IsEnabled = false;
                weight.SelectedItem = drone.MaxWeight;
                battery.Text = drone.Battery.ToString() + "%";
                //status.SelectedItem = drone.Status;
                delivery.Text = drone.Parcel.Id.ToString();
                longitude.Text = drone.Location.CoorLongitude.ToString();
                latitude.Text = drone.Location.CoorLatitude.ToString();
                station.Visibility = Visibility.Collapsed;
                Lstation.Visibility = Visibility.Collapsed;
                string[] parcelOptions = { "Associate Parcel", "Pick Up Parcel", "Supply Parcel" };
                string[] chargeDroneOptions = { "Charging", "Stop Charging" };
                button3.DataContext = chargeDroneOptions;
                button4.DataContext = parcelOptions;
                button3.Visibility = button4.Visibility = Visibility.Visible;
                button2.Content = "Update";
            }
        }

        private void delivery_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}

