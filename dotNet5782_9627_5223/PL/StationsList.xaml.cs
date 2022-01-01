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
    /// Interaction logic for StationsList.xaml
    /// </summary>
    public partial class StationsList : Window
    {
        BLApi.IBL bl;
        Action filter;
        public StationsList(BLApi.IBL bl)
        {
            this.bl = bl;
            InitializeComponent();
            Filter();
            CurrFilter.DataContext = new string[2] { "All BaseStations", "Group By Free ChargeSlots" };
        }

        /// <summary>
        /// the function updates DroneListView with the curreent data.
        /// </summary>
        private void Filter()
        {
            StationsListView.DataContext = (List<BO.BaseStationForList>)bl.GetBaseStationList();
        }

        private void StationListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            new StationView((e.OriginalSource as FrameworkElement).DataContext as BO.BaseStationForList, bl, filter).Show();
        }

        private void Button_ClickAdd(object sender, RoutedEventArgs e)
        {
            new StationView(bl, filter).Show();
        }

        /// <summary>
        /// the function treats the event of pressing the button 'close'. 
        /// </summary>
        /// <param name="sender">the invoking object</param>
        /// <param name="e">the event</param>
        private void Button_ClickClose(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void StatusFilter_SelectionChanged(object sender, RoutedEventArgs e)
        {
            //לחשוב מה לעשות כאן
        }
        
    }
}
