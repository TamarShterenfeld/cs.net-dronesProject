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
using PL;

namespace PL
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Properties
        public BLApi.IBL bl;
        #endregion

        #region Constructor
        public MainWindow()
        {
            bl = BLApi.BLFactory.GetBl();
            InitializeComponent();
        }

        #endregion

        #region Buttons_Events

        /// <summary>
        /// the function treats the event of clicking the button 'all drones'
        /// when it's pressed - it opens a new window named 'DroneList'.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_DroneClick(object sender, RoutedEventArgs e)
        {
            new DroneList(new DroneListViewModel(bl)).Show();
        }

        /// <summary>
        /// the function treats the event of clicking the button 'all stations'
        /// when it's pressed - it opens a new window named 'StationList'.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_StationClick(object sender, RoutedEventArgs e)
        {
            new StationsList(new StationsListViewModel(bl)).Show();
        }

        /// <summary>
        /// the function treats the event of clicking the button 'all stations'
        /// when it's pressed - it opens a new window named 'StationList'.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_ParcelsClick(object sender, RoutedEventArgs e)
        {
            new ParcelsListView(new ParcelsListViewModel(bl)).Show();
        }

        /// <summary>
        /// the function treats the event of clicking the button 'all stations'
        /// when it's pressed - it opens a new window named 'StationList'.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_CustomerClick(object sender, RoutedEventArgs e)
        {
            new CustomersList(new CustomersListViewModel(bl)).Show();
        }

        #endregion
    }
}
