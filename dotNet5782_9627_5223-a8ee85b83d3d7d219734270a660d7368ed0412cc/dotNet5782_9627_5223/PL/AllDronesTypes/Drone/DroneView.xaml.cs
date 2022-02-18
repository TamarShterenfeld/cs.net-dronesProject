using System.Windows;

namespace PL
{
    /// <summary>
    /// Interaction logic for Drone.xaml
    /// </summary>
    public partial class DroneView : Window
    {
        /// <summary>
        /// Interaction logic for StationView.xaml
        /// </summary>
        public DroneView(DroneViewModel droneViewModel)
        {
            InitializeComponent();
            DataContext = droneViewModel;
        }
    }
}

