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
using IBL;
using IBL.BO;

namespace PL
{
    /// <summary>
    /// Interaction logic for DroneList.xaml
    /// </summary>
    public partial class DroneList : Window
    {
        IBL.IBL bl;
        public DroneList(IBL.IBL bl)
        {
            this.bl = bl;
            string[] categories = new string[2] {"status", "weight"};
            InitializeComponent();
            DroneListView.DataContext = bl.GetDronesForList();
            FilterComboBox.DataContext = categories;
        }

        private void StatusSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(FilterComboBox.SelectedItem == null)
            {
                DroneListView.ItemsSource = (List<DroneForList>)bl.GetDronesForList();
            }
            else
            {
                DroneStatuses status = (DroneStatuses)FilterComboBox.SelectedItem;
                DroneListView.ItemsSource = (List<DroneForList>)bl.GetFilteredDroneForList(status);
            }
            
        }
    }
}
