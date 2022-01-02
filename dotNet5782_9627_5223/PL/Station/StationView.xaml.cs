using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PL
{
    /// <summary>
    /// Interaction logic for StationView.xaml
    /// </summary>
    public partial class StationView : Window
    {
        private BLApi.IBL bl;
        Action refreshDroneList;

        public StationView(BLApi.IBL bl, Action filter)
        {
            this.bl = bl;
            InitializeComponent();
            InCharging.DataContext = bl.GetDronesInMe(int.Parse(id.Text));
        }

        public StationView(BO.BaseStationForList station ,BLApi.IBL bl, Action filter)
        {
            this.bl = bl;
            InitializeComponent();
            InCharging.DataContext = bl.GetDronesInMe(2);
        }


        private void DroneListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            new DroneView((e.OriginalSource as FrameworkElement).DataContext as BO.DroneForList, bl, refreshDroneList).Show();
        }

    }
}
