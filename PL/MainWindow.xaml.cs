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
using System.Windows.Navigation;
using System.Windows.Shapes;
using IBL;


namespace PL
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public BLApi.IBL bl ;
        public MainWindow()
        {
            bl = BLApi.BLFactory.GetBl();
            InitializeComponent();
        }

        /// <summary>
        /// the function treats the event of clicking the button 'all drones'
        /// when it's pressed - it opens a new window named 'DroneList'.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            new DroneList(bl).Show();
        }
    }
}
