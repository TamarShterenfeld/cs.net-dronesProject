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
using BO;

namespace PL
{
    /// <summary>
    /// Interaction logic for DroneList.xaml
    /// </summary>
    public partial class DroneList : Window
    {
        BLApi.IBL bl;

        Action currFilter;
        /// <summary>
        /// a constructor
        /// </summary>
        /// <param name="bl">bl - the request object to the BL logic level</param>
        public DroneList(BLApi.IBL bl)
        {
            this.bl = bl;
            string[] categories = new string[2] {"status", "weight"};
            InitializeComponent();
            ChooseCategory.DataContext = categories;
            StatusFilter.DataContext = typeof(DroneStatuses).GetEnumValues();
            WeightFilter .DataContext = typeof(WeightCategories).GetEnumValues();
            StatusFilter.Visibility = Visibility.Collapsed;
            WeightFilter.Visibility = Visibility.Collapsed;
        }

        /// <summary>
        /// this function treats the event of choosing a category (from a ComboBox) 
        /// for filtering by it the drone's list. 
        /// </summary>
        /// <param name="sender">the invoking object</param>
        /// <param name="e">the event of choosing the category</param>
        private void ChooseCategory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (ChooseCategory.SelectedItem)
            {
                case "status":
                    {
                        WeightFilter.Visibility = Visibility.Collapsed;
                        StatusFilter.Visibility = Visibility.Visible;
                        break;
                    }
                case "weight":
                    {
                        StatusFilter.Visibility = Visibility.Collapsed;
                        WeightFilter.Visibility = Visibility.Visible;
                        break;
                    }
            }
        }

        /// <summary>
        /// the function updates DroneListView with the curreent data.
        /// </summary>
        private void Filter()
        {
            DroneListView.ItemsSource = (List<DroneForList>)bl.GetDronesForList();
        }

        /// <summary>
        /// the function invokes GetStatusFilteredDroneForList() with the selected status.
        /// </summary>
        private void FilterStatus()
        {
            Filter();
            DroneStatuses status = (DroneStatuses)StatusFilter.SelectedItem;
            DroneListView.ItemsSource = (List<DroneForList>)bl.GetStatusFilteredDroneForList(status);
            
        }

        /// <summary>
        /// the function invokes GetWeightFilteredDroneForList() with the selected weight.
        /// </summary>
        private void FilterWeight()
        {
                Filter();
                WeightCategories weight = (WeightCategories)WeightFilter.SelectedItem;
                DroneListView.ItemsSource = (List<DroneForList>)bl.GetWeightFilteredDroneForList(weight);
        }

        /// <summary>
        /// the function updates the data when the status filter is changed.
        /// it also updates the delegate currFilter to point at the current list.
        /// </summary>
        /// <param name="sender">the invoking object</param>
        /// <param name="e">the event</param>
        private void StatusFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            currFilter = FilterStatus;
            FilterStatus();
        }

        /// <summary>
        /// the function updates the data when the status filter is changed.
        ///it also updates the delegate currFilter to point at the current list.
        /// </summary>
        /// <param name="sender">the invoking object</param>
        /// <param name="e">the event</param>
        private void WeightFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            currFilter = FilterWeight;
            FilterWeight();
        }

        /// <summary>
        /// the function opens a new window for adding a drone.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_ClickAdd(object sender, RoutedEventArgs e)
        {
            new SingleDrone(bl, currFilter).Show();
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

        /// <summary>
        /// the function treats the event of pressing twice on a drone.
        /// it opens a new window with more actions about the chosen drone.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DroneListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            new SingleDrone((e.OriginalSource as FrameworkElement).DataContext as DroneForList, bl, currFilter).Show();
        }
    }
}
